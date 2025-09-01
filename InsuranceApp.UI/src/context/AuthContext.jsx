import { createContext, useState, useContext, useEffect } from "react";
import { jwtDecode } from "jwt-decode";

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [auth, setAuth] = useState({
    user: null,
    accessToken: null,
    refreshToken: null,
  });
  const [loading, setLoading] = useState(false);

  // Load from localStorage on startup
  useEffect(() => {
    const stored = localStorage.getItem("auth");
    if (stored) {
      setAuth(JSON.parse(stored));
    }
  }, []);

  const login = (user, accessToken, refreshToken) => {
  // decode roles from token
  const decoded = jwtDecode(accessToken);
  const roles = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] || [];

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
    setAuth({ user: null, accessToken: null, refreshToken: null });
    localStorage.removeItem("auth");
  };

  return (
    <AuthContext.Provider
      value={{ ...auth, login, updateTokens, logout, loading, setLoading }}
    >
      {children}
    </AuthContext.Provider>
  );
}

// eslint-disable-next-line react-refresh/only-export-components
export function useAuth() {
  return useContext(AuthContext);
}
