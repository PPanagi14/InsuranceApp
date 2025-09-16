import { useEffect, useState } from "react";
import { useApi } from "../api";
import {
  Typography,
  Box,
  Button,
  Grid,
  TextField,
  InputAdornment,
  ToggleButton,
  ToggleButtonGroup,
  Paper,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  MenuItem,
} from "@mui/material";
import { Search } from "@mui/icons-material";
import { PolicyCard } from "../components/PolicyCard";
import { isExpiringSoon } from "../utils/date";
import { useNavigate } from "react-router-dom";
import PolicyForm from "./PolicyFormPage";

export default function Policies() {
  const { request } = useApi();
  const navigate = useNavigate();

  const [policies, setPolicies] = useState([]);
  const [clients, setClients] = useState([]);
  const [loading, setLoading] = useState(true);

  const [search, setSearch] = useState("");
  const [filter, setFilter] = useState("All");

  useEffect(() => {
    loadPolicies();
    loadClients();
  }, []);

  const loadPolicies = async () => {
    try {
      setLoading(true);
      const data = await request("/api/policies/GetAllPoliciesWithDetails");
      setPolicies(data);
    } catch (err) {
      console.error("Failed to load policies:", err);
    } finally {
      setLoading(false);
    }
  };

  const loadClients = async () => {
    try {
      const data = await request("/api/clients");
      setClients(data);
    } catch (err) {
      console.error("Failed to load clients:", err);
    }
  };


  // Summary values
  const totalPolicies = policies.length;
  const activeCount = policies.filter((p) => p.status === "Active").length;
  const expiringSoonCount = policies.filter((p) => isExpiringSoon(p.endDate))
    .length;
  const totalPremium = policies.reduce(
    (sum, p) => sum + Number(p.premiumAmount || 0),
    0
  );

  // Filtered list
  const filteredPolicies = policies.filter((p) => {
    const matchesSearch = `${p.policyNumber} ${p.insurer} ${p.policyType}`
      .toLowerCase()
      .includes(search.toLowerCase());
    const matchesFilter =
      filter === "All" ||
      p.status === filter ||
      (filter === "Expiring" && isExpiringSoon(p.endDate));
    return matchesSearch && matchesFilter;
  });

  if (loading) {
    return (
      <Typography variant="h6" p={3}>
        Loading policies...
      </Typography>
    );
  }

  return (
    <Box p={3}>
      <Typography variant="h4" gutterBottom>
        Policies
      </Typography>
      <Typography variant="body2" color="text.secondary" gutterBottom>
        Track and manage all insurance policies
      </Typography>

      {/* Summary cards */}
      <Grid container spacing={2} mb={3}>
        <Grid  size={{xs:12 ,sm:6 ,md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="body2" color="text.secondary">
              Total Policies
            </Typography>
            <Typography variant="h6">{totalPolicies}</Typography>
          </Paper>
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="body2" color="text.secondary">
              Active Policies
            </Typography>
            <Typography variant="h6" color="success.main">
              {activeCount}
            </Typography>
          </Paper>
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <Paper
            sx={{
              p: 2,
              borderRadius: 2,
              bgcolor: expiringSoonCount > 0 ? "warning.light" : "background.paper",
              border:
                expiringSoonCount > 0 ? "1px solid" : "1px solid transparent",
              borderColor: expiringSoonCount > 0 ? "warning.main" : "transparent",
              boxShadow: expiringSoonCount > 0 ? 4 : 1,
            }}
          >
            <Typography variant="body2" color="text.secondary">
              Expiring Soon
            </Typography>
            <Typography
              variant="h6"
              color={expiringSoonCount > 0 ? "warning.dark" : "text.primary"}
              fontWeight={expiringSoonCount > 0 ? "bold" : "normal"}
            >
              {expiringSoonCount}
            </Typography>
          </Paper>
        </Grid>
        <Grid size={{xs:12 ,sm:6 ,md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="body2" color="text.secondary">
              Total Premium
            </Typography>
            <Typography variant="h6">€{totalPremium}</Typography>
          </Paper>
        </Grid>
      </Grid>

      {/* Toolbar */}
      <Box
        display="flex"
        alignItems="center"
        justifyContent="space-between"
        mb={3}
        gap={2}
        flexWrap="wrap"
      >
        <TextField
          placeholder="Search policies..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          size="small"
          sx={{ flex: 1, minWidth: 250 }}
          InputProps={{
            startAdornment: (
              <InputAdornment position="start">
                <Search />
              </InputAdornment>
            ),
          }}
        />

        <ToggleButtonGroup
          value={filter}
          exclusive
          onChange={(e, val) => val && setFilter(val)}
          size="small"
        >
          <ToggleButton value="All">All</ToggleButton>
          <ToggleButton value="Active">Active</ToggleButton>
          <ToggleButton value="Pending">Pending</ToggleButton>
          <ToggleButton value="Expiring">Expiring Soon</ToggleButton>
        </ToggleButtonGroup>

        <Button variant="contained" onClick={() => navigate("/policies/new")}>
          + New Policy
        </Button>

      </Box>

      {/* Policies list */}
      
      <Grid container spacing={2}>
        {filteredPolicies.map((policy) => (
          <Grid size={{xs:12 }} key={policy.id}>
            <PolicyCard
              policy={policy}
              client={clients.find((c) => c.id === policy.clientId)}
              onView={() => navigate(`/policies/${policy.id}`)}
              onRenew={() => console.log("Renew", policy.id)}
            />
          </Grid>
        ))}
      </Grid>

      
    </Box>
  );
}
