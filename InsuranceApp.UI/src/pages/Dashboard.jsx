import { Typography, Box, Paper, Grid } from "@mui/material";
import { useAuth } from "../context/AuthContext";
import { useApi } from "../api";
import { useEffect, useState } from "react";

export default function Dashboard() {
  const { user } = useAuth();
  const { request } = useApi();
  const [stats, setStats] = useState({ clients: 0, policies: 0 });

  useEffect(() => {
    async function loadStats() {
      try {
        const clients = await request("/api/clients");
        const policies = await request("/api/policies");
        setStats({ clients: clients.length, policies: policies.length });
      } catch (err) {
        console.error("Failed to load stats", err);
      }
    }
    loadStats();
  }, []);

  return (
    <Box p={3}>
      <Typography variant="h4" gutterBottom>
        Welcome, {user}
      </Typography>

      <Typography variant="body1" gutterBottom>
        Here’s an overview of your insurance management system:
      </Typography>

      <Grid container spacing={2} mt={2}>
        <Grid item xs={12} md={6}>
          <Paper sx={{ p: 3, textAlign: "center" }}>
            <Typography variant="h6">Clients</Typography>
            <Typography variant="h4">{stats.clients}</Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} md={6}>
          <Paper sx={{ p: 3, textAlign: "center" }}>
            <Typography variant="h6">Policies</Typography>
            <Typography variant="h4">{stats.policies}</Typography>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
