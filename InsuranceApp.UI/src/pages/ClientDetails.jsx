import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  Box,
  Typography,
  Button,
  Chip,
  Grid,
  Paper,
  Tabs,
  Tab,
  Divider,
} from "@mui/material";
import { ArrowBack } from "@mui/icons-material";
import { useApi } from "../api";
import { formatDate } from "../utils/date";

export default function ClientDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { request } = useApi();

  const [client, setClient] = useState(null);
  const [tab, setTab] = useState(0);

  useEffect(() => {
    loadClient();
  }, [id]);

  const loadClient = async () => {
    try {
      const data = await request(`/api/clients/${id}`);
      setClient(data);
    } catch (err) {
      console.error("Failed to load client:", err);
    }
  };

  if (!client) return <Typography>Loading...</Typography>;

  return (
    <Box p={3}>
      {/* Back button */}
      <Button
        startIcon={<ArrowBack />}
        onClick={() => navigate("/clients")}
        sx={{ mb: 2 }}
      >
        Back to Clients
      </Button>

      {/* Header */}
      <Box
        display="flex"
        justifyContent="space-between"
        alignItems="center"
        mb={2}
      >
        <Box>
          <Typography variant="h5" fontWeight="bold">
            {client.type === "Person"
              ? `${client.firstName} ${client.lastName}`
              : client.companyName}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Client since {client.startDate ? formatDate(client.startDate) : "-"}
          </Typography>
        </Box>
        <Box display="flex" gap={2} alignItems="center">
          <Chip
            label={client.status || "Active"}
            color={client.status === "Active" ? "success" : "default"}
          />
           <Button
              variant="contained"
              onClick={() => navigate(`/clients/${client.id}/edit`)} // 🔥 redirect to form page
            >
              Edit Client
            </Button>
        </Box>
      </Box>

      {/* Summary cards */}
      <Grid container spacing={2} mb={3}>
        {/* Contact Information */}
        <Grid  size={{xs:12, md:4}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="subtitle1" fontWeight="bold" gutterBottom>
              Contact Information
            </Typography>
            <Typography variant="body2">{client.email}</Typography>
            <Typography variant="body2">{client.phoneMobile}</Typography>
            <Typography variant="body2">{client.address}</Typography>
          </Paper>
        </Grid>

        {/* Personal Details */}
        <Grid size={{xs:12, md:4}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="subtitle1" fontWeight="bold" gutterBottom>
              Personal Details
            </Typography>
            {client.dateOfBirth && (
              <Typography variant="body2">
                Date of Birth: {formatDate(client.dateOfBirth)}
              </Typography>
            )}
            {client.occupation && (
              <Typography variant="body2">
                Occupation: {client.occupation}
              </Typography>
            )}
            <Typography variant="body2">Client ID: #{client.id}</Typography>
          </Paper>
        </Grid>

        {/* Portfolio Summary */}
        <Grid size={{xs:12, md:4}}>
          <Paper sx={{ p: 2, borderRadius: 2 }}>
            <Typography variant="subtitle1" fontWeight="bold" gutterBottom>
              Portfolio Summary
            </Typography>
            <Typography variant="body2">
              Active Policies: {client.policiesCount ?? 0}
            </Typography>
            <Typography variant="body2">
              Total Premium:{" "}
              <span style={{ color: "green", fontWeight: "bold" }}>
                €{client.totalPremium?.toLocaleString() ?? 0}
              </span>
            </Typography>
            <Typography variant="body2">
              Total Claims: {client.totalClaims ?? 0}
            </Typography>
          </Paper>
        </Grid>
      </Grid>

      {/* Tabs */}
      <Paper sx={{ borderRadius: 2 }}>
        <Tabs
          value={tab}
          onChange={(e, newValue) => setTab(newValue)}
          sx={{ borderBottom: 1, borderColor: "divider" }}
        >
          <Tab label="Policies" />
          <Tab label="Claims History" />
          <Tab label="Documents" />
        </Tabs>

        {/* Tab panels */}
        <Box p={2}>
          {tab === 0 && (
            <>
              <Typography variant="subtitle1" gutterBottom>
                Policies
              </Typography>
              <Divider sx={{ mb: 2 }} />
              {client.policies && client.policies.length > 0 ? (
                client.policies.map((p) => (
                  <Paper
                    key={p.id}
                    sx={{ p: 2, borderRadius: 2, mb: 2 }}
                  >
                    <Typography variant="subtitle2" fontWeight="bold">
                      {p.policyType} Insurance
                    </Typography>
                    <Typography variant="body2">
                      Policy #{p.policyNumber}
                    </Typography>
                    <Typography variant="body2" color="success.main">
                      Premium: €{p.premiumAmount}/year
                    </Typography>
                    <Typography variant="body2">
                      {formatDate(p.startDate)} – {formatDate(p.endDate)}
                    </Typography>
                  </Paper>
                ))
              ) : (
                <Typography variant="body2">No policies found.</Typography>
              )}
            </>
          )}

          {tab === 1 && (
            <Typography variant="body2">Claims history coming soon...</Typography>
          )}

          {tab === 2 && (
            <Typography variant="body2">Documents coming soon...</Typography>
          )}
        </Box>
      </Paper>
    </Box>
  );
}
