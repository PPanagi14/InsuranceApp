import * as React from "react";
import { Box, Container, Toolbar, Typography } from "@mui/material";
import { Outlet } from "react-router-dom";
import TopNavbar from "./Navbar";



export default function Layout() {

  return (
    <Box sx={{ minHeight: "100vh", bgcolor: "background.default" }}>
      <TopNavbar />
      {/* Push content below fixed AppBar */}
      <Toolbar />

      <Container maxWidth="lg" sx={{ py: 3 }}>

        <Outlet />
      </Container>
    </Box>
  );
}
