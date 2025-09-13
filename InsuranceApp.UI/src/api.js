import { useEffect, useRef, useCallback } from "react";
import { useAuth } from "./context/AuthContext";

// Small helpers
const safeParseJson = async (res) => {
  const text = await res.text();
  if (!text) return null;
  try { return JSON.parse(text); } catch { return text; }
};

const OFFLINE = import.meta?.env?.VITE_OFFLINE_MODE === "true";

const getBaseUrl = () =>
  import.meta?.env?.VITE_API_BASE_URL?.replace(/\/$/, "") ||
  "http://localhost:5073";

// ------- Offline mock API (for local demo without backend) -------
const loadMockDb = () => {
  try {
    const raw = localStorage.getItem("mock-db");
    if (raw) return JSON.parse(raw);
  } catch {}
  return { clients: [], policies: [] };
};
const saveMockDb = (db) => {
  try { localStorage.setItem("mock-db", JSON.stringify(db)); } catch {}
};
let mockDb = loadMockDb();
const uid = () => (crypto?.randomUUID ? crypto.randomUUID() : `id-${Math.random().toString(36).slice(2)}`);

const createDemoJwt = (name, roles = ["Admin"]) => {
  const header = { alg: "none", typ: "JWT" };
  const payload = {
    sub: name,
    name,
    ["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]: roles,
    exp: Math.floor(Date.now() / 1000) + 60 * 60,
  };
  const toUrl = (obj) => btoa(JSON.stringify(obj))
    .replace(/\+/g, "-")
    .replace(/\//g, "_")
    .replace(/=+$/, "");
  return `${toUrl(header)}.${toUrl(payload)}.`;
};

async function offlineHandle(url, options = {}) {
  const { method = "GET", body } = options;
  const path = url.split("?")[0];
  const parse = () => {
    try { return typeof body === "string" ? JSON.parse(body) : body; } catch { return {}; }
  };

  // Auth
  if (path === "/api/auth/login" && method === "POST") {
    return { token: { accessToken: createDemoJwt("Demo Admin"), refreshToken: "demo-refresh" } };
  }
  if (path === "/api/auth/refresh" && method === "POST") {
    return { accessToken: createDemoJwt("Demo Admin") };
  }

  // Clients
  if (path === "/api/clients" && method === "GET") {
    return mockDb.clients;
  }
  if (path === "/api/clients" && method === "POST") {
    const data = parse();
    const item = { id: uid(), phoneMobile: data.phone || "", ...data };
    mockDb.clients.push(item);
    saveMockDb(mockDb);
    return item;
  }
  if (path.startsWith("/api/clients/") && method === "PUT") {
    const id = path.split("/").pop();
    const data = parse();
    mockDb.clients = mockDb.clients.map((c) => (c.id === id ? { ...c, ...data } : c));
    saveMockDb(mockDb);
    return mockDb.clients.find((c) => c.id === id);
  }
  if (path.startsWith("/api/clients/") && method === "DELETE") {
    const id = path.split("/").pop();
    mockDb.clients = mockDb.clients.filter((c) => c.id !== id);
    saveMockDb(mockDb);
    return null;
  }

  // Policies
  if (path === "/api/policies" && method === "GET") {
    return mockDb.policies;
  }
  if (path === "/api/policies" && method === "POST") {
    const data = parse();
    const item = { id: uid(), ...data };
    mockDb.policies.push(item);
    saveMockDb(mockDb);
    return item;
  }
  if (path.startsWith("/api/policies/") && method === "PUT") {
    const id = path.split("/").pop();
    const data = parse();
    mockDb.policies = mockDb.policies.map((p) => (p.id === id ? { ...p, ...data } : p));
    saveMockDb(mockDb);
    return mockDb.policies.find((p) => p.id === id);
  }
  if (path.startsWith("/api/policies/") && method === "DELETE") {
    const id = path.split("/").pop();
    mockDb.policies = mockDb.policies.filter((p) => p.id !== id);
    saveMockDb(mockDb);
    return null;
  }

  // Default
  throw { status: 404, title: "Offline route not implemented", detail: path };
}

export function useApi() {
  const { accessToken, refreshToken, updateTokens, logout, setLoading } = useAuth();

  // Refs so our returned functions keep a STABLE identity while always reading the latest values
  const accessTokenRef = useRef(accessToken);
  const refreshTokenRef = useRef(refreshToken);
  const updateTokensRef = useRef(updateTokens);
  const logoutRef = useRef(logout);
  const setLoadingRef = useRef(setLoading);

  useEffect(() => { accessTokenRef.current = accessToken; }, [accessToken]);
  useEffect(() => { refreshTokenRef.current = refreshToken; }, [refreshToken]);
  useEffect(() => { updateTokensRef.current = updateTokens; }, [updateTokens]);
  useEffect(() => { logoutRef.current = logout; }, [logout]);
  useEffect(() => { setLoadingRef.current = setLoading; }, [setLoading]);

  // Loading counter so multiple overlapping requests don't flicker
  const activeRequestsRef = useRef(0);
  const startLoading = () => {
    activeRequestsRef.current += 1;
    if (activeRequestsRef.current === 1) setLoadingRef.current?.(true);
  };
  const stopLoading = () => {
    activeRequestsRef.current = Math.max(0, activeRequestsRef.current - 1);
    if (activeRequestsRef.current === 0) setLoadingRef.current?.(false);
  };

  // Refresh mutex so parallel 401s trigger ONE refresh
  const isRefreshingRef = useRef(false);
  const refreshPromiseRef = useRef(null);

  const ensureRefreshed = useCallback(async () => {
    if (isRefreshingRef.current && refreshPromiseRef.current) {
      return refreshPromiseRef.current; // wait for ongoing refresh
    }
    const rt = refreshTokenRef.current;
    if (!rt) return null;

    isRefreshingRef.current = true;
    refreshPromiseRef.current = (async () => {
      try {
        const res = await fetch(`${getBaseUrl()}/api/auth/refresh`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ refreshToken: rt }),
        });

        if (!res.ok) return null;

        const data = await res.json(); // { accessToken, refreshToken }
        if (data?.accessToken) {
          updateTokensRef.current?.(data.accessToken, data.refreshToken ?? rt);
        }
        return data;
      } catch {
        return null;
      } finally {
        isRefreshingRef.current = false;
        refreshPromiseRef.current = null;
      }
    })();

    return refreshPromiseRef.current;
  }, []);

  // STABLE request function
  const request = useCallback(async (url, options = {}, retry = true) => {
    startLoading();
    try {
      const base = getBaseUrl();
      const token = accessTokenRef.current;

      // Offline mode short-circuit
      if (OFFLINE) {
        return await offlineHandle(url, options);
      }

      // Only set JSON header if sending a body and header not already given
      const hasBody = options.body != null;
      const headers = {
        ...(options.headers || {}),
        ...(hasBody && !options.headers?.["Content-Type"] ? { "Content-Type": "application/json" } : {}),
        ...(token ? { Authorization: `Bearer ${token}` } : {}),
      };

      const response = await fetch(`${base}${url}`, { ...options, headers });

      // Handle 401 → try refresh once
      if (response.status === 401 && retry && refreshTokenRef.current) {
        const refreshed = await ensureRefreshed();
        if (refreshed?.accessToken) {
          // retry once with new token
          return await request(url, options, false);
        } else {
          // refresh failed → logout
          logoutRef.current?.();
          window.location.href = "/login";
          throw { status: 401, title: "Unauthorized", detail: "Session expired. Please log in again." };
        }
      }

      // No content
      if (response.status === 204) return null;

      // Try to parse (ProblemDetails or data)
      const payload = await safeParseJson(response);

      if (!response.ok) {
        // Normalize error shape
        const problem = {
          status: response.status,
          title: payload?.title || "Request failed",
          detail: payload?.detail || (typeof payload === "string" ? payload : "Unknown error"),
          errors: payload?.errors,
          raw: payload,
        };
        throw problem;
      }

      return payload;
    } catch (err) {
      // Surface clean error
      console.error("API error:", err);
      if (err instanceof TypeError) {
        try {
          return await offlineHandle(url, options);
        } catch (e2) {
          throw { status: 0, title: "Network error", detail: "Failed to reach API" };
        }
      }
      throw err;
    } finally {
      stopLoading();
    }
  }, [ensureRefreshed]);

  return { request };
}
