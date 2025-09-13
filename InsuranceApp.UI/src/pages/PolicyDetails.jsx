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
import { ArrowBack, Person, AttachMoney, Event } from "@mui/icons-material";
import { useApi } from "../api";
import { formatDate } from "../utils/date";

export default function PolicyDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const { request } = useApi();

  const [policy, setPolicy] = useState(null);
  const [tab, setTab] = useState(0);

  useEffect(() => {
    loadPolicy();
  }, [id]);

  const loadPolicy = async () => {
    try {
      const data = await request(`/api/policies/${id}`);
      setPolicy(data);
    } catch (err) {
      console.error("Failed to load policy:", err);
    }
  };

  if (!policy) return <Typography>Loading...</Typography>;

  return (
    <Box p={3}>
      {/* Back button */}
      <Button
        startIcon={<ArrowBack />}
        onClick={() => navigate("/policies")}
        sx={{ mb: 2 }}
      >
        Back to Policies
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
            {policy.policyType} Insurance
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Policy #{policy.policyNumber} • {policy.clientName}
          </Typography>
        </Box>
        <Box display="flex" gap={2} alignItems="center">
          <Chip
            label={policy.status}
            color={policy.status === "Active" ? "success" : "default"}
          />
          <Button
                variant="contained"
                onClick={() => navigate(`/policies/${policy.id}/edit`)} // 🔥 redirect to form page
            >
                Edit Policy
            </Button>
        </Box>
      </Box>

      {/* Summary cards */}
      <Grid container spacing={2} mb={3}>
        {/* Client */}
        <Grid size={{xs:12, md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2, textAlign: "center" }}>
            <Typography
              variant="subtitle2"
              color="text.secondary"
              gutterBottom
              display="flex"
              alignItems="center"
              justifyContent="center"
              gap={1}
            >
              <Person fontSize="small" /> Client
            </Typography>
            <Typography variant="subtitle1" fontWeight="bold">
              {policy.clientName}
            </Typography>
            <Button
              size="small"
              variant="outlined"
              sx={{ mt: 1 }}
              onClick={() => navigate(`/clients/${policy.clientId}`)}
            >
              View Client
            </Button>
          </Paper>
        </Grid>

        {/* Premium */}
        <Grid size={{xs:12, md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2, textAlign: "center" }}>
            <Typography
              variant="subtitle2"
              color="text.secondary"
              gutterBottom
              display="flex"
              alignItems="center"
              justifyContent="center"
              gap={1}
            >
              <AttachMoney fontSize="small" /> Premium
            </Typography>
            <Typography
              variant="h5"
              fontWeight="bold"
              color="success.main"
            >
              €{policy.premiumAmount?.toLocaleString()}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              €{policy.monthlyPremium}/month
            </Typography>
          </Paper>
        </Grid>

        {/* Coverage */}
        <Grid size={{xs:12, md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2, textAlign: "center" }}>
            <Typography
              variant="subtitle2"
              color="text.secondary"
              gutterBottom
              display="flex"
              alignItems="center"
              justifyContent="center"
              gap={1}
            >
              🛡 Coverage
            </Typography>
            <Typography variant="h5" fontWeight="bold">
              €{policy.coverageAmount?.toLocaleString()}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Total Liability
            </Typography>
          </Paper>
        </Grid>

        {/* Renewal */}
        <Grid size={{xs:12, md:3}}>
          <Paper sx={{ p: 2, borderRadius: 2, textAlign: "center" }}>
            <Typography
              variant="subtitle2"
              color="text.secondary"
              gutterBottom
              display="flex"
              alignItems="center"
              justifyContent="center"
              gap={1}
            >
              <Event fontSize="small" /> Renewal
            </Typography>
            <Typography variant="h6" fontWeight="bold">
              {formatDate(policy.endDate)}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Next Payment: {formatDate(policy.nextPaymentDate)}
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
          <Tab label="Coverage Details" />
          <Tab label="Vehicle Info" />
          <Tab label="Payment History" />
          <Tab label="Claims" />
          <Tab label="Documents" />
        </Tabs>

        {/* Tab panels */}
        <Box p={2}>
          {tab === 0 && (
            <>
              <Typography variant="h6" gutterBottom>
                Coverage Breakdown
              </Typography>
              <Grid container spacing={2}>
                <Grid size={{xs:12, md:6}}>
                  <Paper sx={{ p: 2, borderRadius: 2 }}>
                    <Typography variant="body2">
                      Liability Coverage: €{policy.liabilityCoverage}
                    </Typography>
                    <Typography variant="body2">
                      Collision: €{policy.collisionCoverage}
                    </Typography>
                    <Typography variant="body2">
                      Comprehensive: €{policy.comprehensiveCoverage}
                    </Typography>
                    <Typography variant="body2">
                      Uninsured Motorist: €{policy.uninsuredCoverage}
                    </Typography>
                  </Paper>
                </Grid>
                <Grid size={{xs:12, md:6}}>
                  <Paper sx={{ p: 2, borderRadius: 2 }}>
                    <Typography variant="h6" gutterBottom>
                      Policy Dates
                    </Typography>
                    <Typography variant="body2">
                      Start Date: {formatDate(policy.startDate)}
                    </Typography>
                    <Typography variant="body2">
                      End Date: {formatDate(policy.endDate)}
                    </Typography>
                    <Typography variant="body2">
                      Renewal Date: {formatDate(policy.endDate)}
                    </Typography>
                    <Typography variant="body2">
                      Payment Method: {policy.paymentMethod || "N/A"}
                    </Typography>
                  </Paper>
                </Grid>
              </Grid>
            </>
          )}

          {tab === 1 && <Typography>Vehicle Info coming soon...</Typography>}
          {tab === 2 && <Typography>Payment History coming soon...</Typography>}
          {tab === 3 && <Typography>Claims coming soon...</Typography>}
          {tab === 4 && <Typography>Documents coming soon...</Typography>}
        </Box>
      </Paper>
    </Box>
  );
}
