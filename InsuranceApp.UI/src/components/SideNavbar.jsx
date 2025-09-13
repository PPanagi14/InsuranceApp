import * as React from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Toolbar,
  Typography,
  Box,
} from "@mui/material";

import DashboardIcon from "@mui/icons-material/Dashboard";
import PeopleIcon from "@mui/icons-material/People";
import ShieldIcon from "@mui/icons-material/Shield";
import PaymentsIcon from "@mui/icons-material/Payments";
import { useNavigate } from "react-router-dom";



const drawerWidth = 240;

export default function SideNavbar() {
  const navigator = useNavigate();
  
  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        "& .MuiDrawer-paper": {
          width: drawerWidth,
          boxSizing: "border-box",
        },
      }}
    >
      <Toolbar>
        <Typography variant="h6" noWrap>
          InsuranceApp
        </Typography>
      </Toolbar>
      <Box sx={{ overflow: "auto" }}>
        <List>
          {[
            { text: "Dashboard", icon: <DashboardIcon /> },
            { text: "Clients", icon: <PeopleIcon /> },
            { text: "Policies", icon: <ShieldIcon /> },
            { text: "Payments", icon: <PaymentsIcon /> },
          ].map((item) => (
            <ListItem key={item.text} disablePadding>
              <ListItemButton onClick={() => {navigator(`/${item.text.toLowerCase()}`)}}>
                <ListItemIcon>{item.icon}</ListItemIcon>
                <ListItemText primary={item.text} />
              </ListItemButton>
            </ListItem>
          ))}
        </List>
      </Box>
    </Drawer>
  );
}
