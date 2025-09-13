import { createContext, useState, useContext, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [auth, setAuth] = useState({
    user: null,
    accessToken: null,
    refreshToken: null,
    roles: [],
  });
  const [loading, setLoading] = useState(true); // start as true

  // Load from localStorage on startup
  useEffect(() => {
    try {
      const stored = localStorage.getItem("auth");
      if (stored) {
        setAuth(JSON.parse(stored));
      }
    } catch (err) {
      console.error("Error reading auth from localStorage", err);
      localStorage.removeItem("auth");
    }
    setLoading(false); 
  }, []);

  const login = (user, accessToken, refreshToken) => {
    let roles = [];
    try {
      const decoded = jwtDecode(accessToken);
      const claim =
        decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      if (Array.isArray(claim)) roles = claim;
      else if (typeof claim === "string") roles = [claim];
    } catch {
      roles = [];
    }

    const data = { user, accessToken, refreshToken, roles };
    setAuth(data);
    localStorage.setItem("auth", JSON.stringify(data));
  };

  const updateTokens = (accessToken, refreshToken) => {
    const data = { ...auth, accessToken, refreshToken };
    setAuth(data);
    localStorage.setItem("auth", JSON.stringify(data));
  };

  const logout = () => {
    setAuth({ user: null, accessToken: null, refreshToken: null, roles: [] });
    localStorage.removeItem("auth");
  };

  return (
    <AuthContext.Provider
      value={{ ...auth, login, updateTokens, logout, loading }}
    >
      {children}
    </AuthContext.Provider>
  );
}

// eslint-disable-next-line react-refresh/only-export-components
export function useAuth() {
  return useContext(AuthContext);
}
