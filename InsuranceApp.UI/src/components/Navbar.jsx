import * as React from "react";
import {
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Button,
  Box,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Divider,
  Menu,
  MenuItem,
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import NotificationsIcon from "@mui/icons-material/Notifications";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import DashboardIcon from "@mui/icons-material/Dashboard";
import PeopleIcon from "@mui/icons-material/People";
import DescriptionIcon from "@mui/icons-material/Description";
import AttachMoneyIcon from "@mui/icons-material/AttachMoney";
import { NavLink, useLocation } from "react-router-dom";
import { useAuth } from "../context/AuthContext"; // adjust path if needed

const routes = [
  { label: "Dashboard", to: "/dashboard", icon: <DashboardIcon /> },
  { label: "Clients", to: "/clients", icon: <PeopleIcon /> },
  { label: "Policies", to: "/policies", icon: <DescriptionIcon /> },
  { label: "Payments", to: "/payments", icon: <AttachMoneyIcon /> },
];

export default function Navbar() {
  const [open, setOpen] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const location = useLocation();
  const { logout, user } = useAuth(); // user should contain { name, email }

  const isActive = (to) => location.pathname === to;
  const handleMenuOpen = (event) => setAnchorEl(event.currentTarget);
  const handleMenuClose = () => setAnchorEl(null);

  const handleLogout = () => {
    handleMenuClose();
    logout();
    window.location.href = "/login"; // redirect to login after logout
  };

  // Map pathname to page title


  return (
    <>
      <AppBar
        position="fixed"
        elevation={2}
        sx={{
          bgcolor: "primary.main",       // 🔵 use theme primary color
          color: "primary.contrastText", // text becomes white by default
        }}
      >
        <Toolbar sx={{ gap: 2 }}>
          {/* Mobile menu */}
          <IconButton
            edge="start"
            onClick={() => setOpen(true)}
            sx={{ display: { xs: "inline-flex", md: "none" }, color: "inherit" }}
          >
            <MenuIcon />
          </IconButton>

          {/* Brand / Page title */}
         <Typography variant="h6" sx={{ mr: 2, fontWeight: 700 }}>
            InsuranceApp
          </Typography>


          {/* Desktop nav */}
          <Box sx={{ display: { xs: "none", md: "flex" }, gap: 1, flexGrow: 1 }}>
            {routes.map((r) => (
              <Button
                key={r.to}
                component={NavLink}
                to={r.to}
                sx={{
                  textTransform: "none",
                  fontWeight: isActive(r.to) ? 700 : 500,
                  px: 2.5,
                  py: 0.75,
                  borderRadius: "20px", // pill shape
                  transition: "all 0.2s ease-in-out",
                  bgcolor: isActive(r.to) ? "primary.dark" : "transparent",
                  color: isActive(r.to) ? "primary.contrastText" : "primary.contrastText",
                  "&:hover": {
                    bgcolor: isActive(r.to)
                      ? "primary.main"
                      : "rgba(255,255,255,0.15)",
                  },

                }}
              >
                {r.label}
              </Button>
            ))}
          </Box>


          {/* Right actions */}
          <Box sx={{ ml: "auto", display: "flex", alignItems: "center", gap: 1 }}>
            <IconButton color="inherit">
              <NotificationsIcon />
            </IconButton>

            <IconButton onClick={handleMenuOpen} color="inherit">
              {/* You could replace this with <Avatar src={user?.avatar} /> */}
              <AccountCircleIcon />
            </IconButton>

            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={handleMenuClose}
              
            >
              <Box sx={{ px: 2, py: 1.5 }}>
                <Typography variant="subtitle1" fontWeight={600}>
                  {user?.name || "User"}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  {user?.email || ""}
                </Typography>
              </Box>
              <Divider />
              <MenuItem onClick={handleMenuClose}>Profile</MenuItem>
              <MenuItem onClick={handleMenuClose}>Settings</MenuItem>
              <Divider />
              <MenuItem onClick={handleLogout} sx={{ color: "error.main" }}>
                Logout
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </AppBar>


      {/* Mobile drawer */}
      <Drawer
        anchor="left"
        open={open}
        onClose={() => setOpen(false)}
        sx={{ display: { md: "none" } }}
      >
        <Box
          sx={{ width: 260 }}
          role="presentation"
          onClick={() => setOpen(false)}
        >
          <Typography variant="h6" sx={{ p: 2, fontWeight: 600 }}>
            InsuranceApp
          </Typography>
          <Divider />
          <List>
            {routes.map((r) => (
              <ListItem key={r.to} disablePadding>
                <ListItemButton
                  component={NavLink}
                  to={r.to}
                  selected={isActive(r.to)}
                >
                  <ListItemIcon>{r.icon}</ListItemIcon>
                  <ListItemText primary={r.label} />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </Box>
      </Drawer>
    </>
  );
}
