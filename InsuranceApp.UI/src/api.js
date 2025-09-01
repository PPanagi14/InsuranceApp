import { useAuth } from "./context/AuthContext";

export function useApi() {
const { accessToken, refreshToken, updateTokens, logout, setLoading } = useAuth();


  const request = async (url, options = {}, retry = true) => {
  setLoading(true);
    const headers = {
      ...(options.headers || {}),
      "Content-Type": "application/json",
      ...(accessToken ? { Authorization: `Bearer ${accessToken}` } : {}),
    };

    try {
      const response = await fetch(`http://localhost:5073${url}`, {
        ...options,
        headers,
      });

      if (response.status === 401 && retry && refreshToken) {
        const refreshed = await refreshAccessToken(refreshToken);
        if (refreshed) {
          updateTokens(refreshed.accessToken, refreshed.refreshToken);
          return await request(url, options, false); // retry once
        } else {
          logout();
          window.location.href = "/login"; // redirect
          throw new Error("Session expired. Please log in again.");
        }
      }

      return await response.json();
    } catch (err) {
      console.error("API error:", err);
      throw err;
    }finally {
    setLoading(false);
  }
  };

  const refreshAccessToken = async (refreshToken) => {
    try {
      const res = await fetch("http://localhost:5073/api/auth/refresh", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ refreshToken }), // ✅ matches RefreshTokenCommand
      });

      if (!res.ok) return null;
      return await res.json(); // ✅ matches AuthResultDto
    } catch {
      return null;
    }
  };

  return { request };
}
