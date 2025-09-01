import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export default function Navbar() {
  const { logout, user, roles } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <AppBar position="static">
      <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
        <Box>
          <Button component={Link} to="/dashboard" color="inherit">
            Dashboard
          </Button>
          <Button component={Link} to="/clients" color="inherit">
            Clients
          </Button>
          <Button component={Link} to="/policies" color="inherit">
            Policies
          </Button>

          {/* ✅ Only show if Admin */}
          {roles?.includes("Admin") && (
            <Button component={Link} to="/users" color="inherit">
              Manage Users
            </Button>
          )}
        </Box>

        <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
          {user && <Typography variant="body1">👤 {user}</Typography>}
          <Button color="inherit" onClick={handleLogout}>
            Logout
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
