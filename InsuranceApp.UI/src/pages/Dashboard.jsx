import {
  Typography,
  Box,
  Grid,
  Button,
  CircularProgress,
  Alert,
  Paper,
  List,
  ListItem,
  ListItemText,
  Stack,
} from "@mui/material";
import { useAuth } from "../context/AuthContext";
import { useApi } from "../api";
import { useEffect, useMemo, useState } from "react";
import { StatCard } from "../components/StatCard";
import { useNavigate } from "react-router-dom";
import PeopleIcon from "@mui/icons-material/People";
import DescriptionIcon from "@mui/icons-material/Description";
import WarningAmberIcon from "@mui/icons-material/WarningAmber";
import EuroIcon from "@mui/icons-material/Euro";

function daysBetween(a, b) {
  const ms = b.setHours(0, 0, 0, 0) - a.setHours(0, 0, 0, 0);
  return Math.ceil(ms / (1000 * 60 * 60 * 24));
}

export default function Dashboard() {
  const { user } = useAuth();
  const { request } = useApi();
  const navigate = useNavigate();

  const [clients, setClients] = useState([]);
  const [policies, setPolicies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    let alive = true;
    (async () => {
      try {
        setLoading(true);
        const [c, p] = await Promise.all([
          request("/api/clients"),
          request("/api/policies"),
        ]);
        if (!alive) return;
        setClients(Array.isArray(c) ? c : []);
        setPolicies(Array.isArray(p) ? p : []);
      } catch (err) {
        console.error("Failed to load dashboard data", err);
        setError("Failed to load dashboard data.");
      } finally {
        setLoading(false);
      }
    })();
    return () => {
      alive = false;
    };
  }, [request]);

  const now = useMemo(() => new Date(), []);

  const stats = useMemo(() => {
    const totalClients = clients.length;
    const totalPolicies = policies.length;
    const activePolicies = policies.filter((p) => String(p.status) === "Active").length;

    const expiringSoon = policies
      .filter((p) => String(p.status) === "Active" && p.endDate)
      .filter((p) => {
        const end = new Date(p.endDate);
        const d = daysBetween(new Date(now), end);
        return d >= 0 && d <= 30;
      });

    const clientsWithPolicies = new Set(policies.map((p) => p.clientId));
    const clientsWithoutPolicies = clients.filter((c) => !clientsWithPolicies.has(c.id));

    const totalPremiumActive = policies
      .filter((p) => String(p.status) === "Active")
      .reduce((sum, p) => sum + (Number(p.premiumAmount) || 0), 0);

    return {
      totalClients,
      totalPolicies,
      activePolicies,
      expiringSoon,
      clientsWithoutPolicies,
      totalPremiumActive,
    };
  }, [clients, policies, now]);

  if (loading) {
    return (
      <Box p={3}>
        <Stack direction="row" alignItems="center" gap={2}>
          <CircularProgress size={24} />
          <Typography>Loading dashboard…</Typography>
        </Stack>
      </Box>
    );
  }

  return (
    <Box p={3}>
      {/* Welcome Header */}
      <Typography variant="h4" gutterBottom fontWeight={600}>
        Welcome{user ? `, ${user}` : ""} 👋
      </Typography>
      <Typography variant="body1" color="text.secondary" gutterBottom>
        Here’s a quick snapshot of your brokerage performance.
      </Typography>

      {error && (
        <Box my={2}>
          <Alert severity="error">{error}</Alert>
        </Box>
      )}

      {/* Stat Cards */}
      <Grid container spacing={2} mt={1}>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <StatCard
            title=" Total Clients"
            value={stats.totalClients}
            icon={<PeopleIcon color="primary" />}
          />
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <StatCard
            title="Policies"
            value={stats.totalPolicies}
            subtitle={`${stats.activePolicies} active`}
            icon={<DescriptionIcon color="success" />}
          />
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <StatCard
            title="Expiring ≤ 30 days"
            value={stats.expiringSoon.length}
            icon={<WarningAmberIcon color="warning" />}
          />
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <StatCard
            title="Active Premium"
            value={`${(stats.totalPremiumActive || 0).toFixed(2)} EUR`}
            icon={<EuroIcon color="info" />}
          />
        </Grid>
      </Grid>

      {/* Detail Sections */}
      <Grid container spacing={2} mt={2}>
        {/* Expiring Policies */}
        <Grid size={{xs:12 ,md:6}}>
          <Paper sx={{ p: 2, borderRadius: 2, boxShadow: 2 }}>
            <Typography variant="h6" fontWeight={600} gutterBottom>
              Policies expiring soon
            </Typography>
            {stats.expiringSoon.length === 0 ? (
              <Typography color="text.secondary">No upcoming expirations.</Typography>
            ) : (
              <List dense>
                {stats.expiringSoon
                  .slice()
                  .sort((a, b) => new Date(a.endDate) - new Date(b.endDate))
                  .slice(0, 8)
                  .map((p) => {
                    const d = daysBetween(new Date(now), new Date(p.endDate));
                    const client = clients.find((c) => c.id === p.clientId);
                    const clientName =
                      client?.type === "Person"
                        ? `${client?.firstName ?? ""} ${client?.lastName ?? ""}`.trim()
                        : client?.companyName || "—";
                    return (
                      <ListItem
                        key={p.id}
                        secondaryAction={
                          <Typography
                            variant="body2"
                            color={d <= 7 ? "error.main" : "text.secondary"}
                          >
                            {d} days
                          </Typography>
                        }
                      >
                        <ListItemText
                          primary={`${p.insurer} • ${p.policyNumber}`}
                          secondary={`${clientName} — ends ${new Date(p.endDate).toLocaleDateString()}`}
                        />
                      </ListItem>
                    );
                  })}
              </List>
            )}
          </Paper>
        </Grid>

        {/* Quick Actions + Clients without policies */}
        <Grid size={{xs:12 ,md:6}}>
          <Paper sx={{ p: 2, borderRadius: 2, boxShadow: 2, mb: 2 }}>
            <Typography variant="h6" fontWeight={600} gutterBottom>
              Quick actions
            </Typography>
            <Stack direction="column" gap={1}>
              <Button variant="contained" onClick={() => navigate("/clients")}>
                Add Client
              </Button>
              <Button variant="outlined" onClick={() => navigate("/policies")}>
                Add Policy
              </Button>
              <Button
                variant="text"
                onClick={() => navigate("/policies?filter=expiring30")}
              >
                View expiring policies
              </Button>
            </Stack>
          </Paper>

          <Paper sx={{ p: 2, borderRadius: 2, boxShadow: 2 }}>
            <Typography variant="h6" fontWeight={600} gutterBottom>
              Clients without policies
            </Typography>
            {stats.clientsWithoutPolicies.length === 0 ? (
              <Typography color="text.secondary">
                All clients have at least one policy.
              </Typography>
            ) : (
              <List dense>
                {stats.clientsWithoutPolicies.slice(0, 6).map((c) => (
                  <ListItem key={c.id}>
                    <ListItemText
                      primary={
                        c.type === "Person"
                          ? `${c.firstName ?? ""} ${c.lastName ?? ""}`.trim() || c.email
                          : c.companyName || c.email
                      }
                      secondary={c.city || c.email}
                    />
                  </ListItem>
                ))}
              </List>
            )}
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
