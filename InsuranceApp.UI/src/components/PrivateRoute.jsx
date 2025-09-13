import { Navigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function PrivateRoute({ children }) {
  const { accessToken, loading } = useAuth();

  // While restoring auth state, show nothing or a loader
  if (loading) {
    return <div>Loading...</div>; // or <Spinner /> component
  }

  // After loading, check token
  if (!accessToken) {
    return <Navigate to="/login" replace />;
  }

  return children;
}
