import { useEffect, useState } from "react";
import { useApi } from "../api";
import {
  Typography,
  Box,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  MenuItem,
  Grid,
  InputAdornment,
  ToggleButtonGroup,
  ToggleButton,
} from "@mui/material";
import { Search, FilterList } from "@mui/icons-material";
import { ClientCard } from "../components/ClientCard";
import { useNavigate } from "react-router-dom";


export default function Clients() {
  const navigate = useNavigate();

  const { request } = useApi();
  const [clients, setClients] = useState([]);
  const [loading, setLoading] = useState(true);


  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState("All");

  // Load clients
  useEffect(() => {
    loadClients();
  }, []);

  const loadClients = async () => {
    try {
      setLoading(true);
      const data = await request("/api/clients/GetAllClientsWithDetails");
      setClients(data);
    } catch (err) {
      console.error("Failed to load clients:", err);
    } finally {
      setLoading(false);
    }
  };



  // Derived list after search & filter
  const filteredClients = clients.filter((c) => {
    const matchesSearch =
      `${c.firstName} ${c.lastName} ${c.companyName} ${c.email}`
        .toLowerCase()
        .includes(search.toLowerCase());

    const matchesFilter =
      statusFilter === "All" || c.status === statusFilter;

    return matchesSearch && matchesFilter;
  });

 if(loading) {
    return <Typography variant="h6" p={3}>Loading clients...</Typography>;
  }

  return (
    <Box p={3}>
      <Typography variant="h4" gutterBottom>
        Clients
      </Typography>

      {/* Toolbar */}
      <Box display="flex" alignItems="center" justifyContent="space-between" mb={3} gap={2} flexWrap="wrap">
        {/* Search bar */}
        <TextField
          placeholder="Search clients..."
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

        {/* Filter buttons */}
        <ToggleButtonGroup
          value={statusFilter}
          exclusive
          onChange={(e, val) => val && setStatusFilter(val)}
          size="small"
        >
          <ToggleButton value="All">
            <FilterList fontSize="small" /> All ({clients.length})
          </ToggleButton>
          <ToggleButton value="Active">
            Active ({clients.filter((c) => c.status === "Active").length})
          </ToggleButton>
          <ToggleButton value="Inactive">
            Inactive ({clients.filter((c) => c.status === "Inactive").length})
          </ToggleButton>
        </ToggleButtonGroup>

        {/* Add client button */}
        <Button variant="contained" onClick={() => navigate("/clients/new")}>
          + New Client
        </Button>
      </Box>

      {/* Grid of cards */}
      <Grid container spacing={3}>
        {filteredClients.map((client) => (
          <Grid size={{xs:12, sm:8, md:6, lg:4}} key={client.id}>
            <ClientCard client={client} />
          </Grid>
        ))}
      </Grid>

      
    </Box>
  );
}
