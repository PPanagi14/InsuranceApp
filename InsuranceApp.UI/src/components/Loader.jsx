import { useAuth } from "../context/AuthContext";
import { CircularProgress, Backdrop } from "@mui/material";

export default function Loader() {
  const { loading } = useAuth();
  return (
    <Backdrop open={loading} sx={{ color: "#fff", zIndex: 9999 }}>
      <CircularProgress color="inherit" />
    </Backdrop>
  );
}
