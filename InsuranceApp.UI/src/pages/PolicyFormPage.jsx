import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Typography, Paper, Button, Snackbar, Alert } from "@mui/material";
import { useApi } from "../api";
import PolicyForm from "../components/PolicyForm";
import { useSnackbar } from "../hooks/useSnackbar";

export default function PolicyFormPage() {
  const { id } = useParams();
  const { request } = useApi();
  const navigate = useNavigate();
  const { snackbar, showSnackbar, handleClose } = useSnackbar();

  const [clients, setClients] = useState([]);
  const [initialValues, setInitialValues] = useState({
    clientId: "",
    insurer: "",
    policyType: "Auto",
    policyNumber: "",
    startDate: "",
    endDate: "",
    premiumAmount: "",
    currency: "EUR",
    status: "Active",

    // new fields
    coverageAmount: "",
    paymentFrequency: "Annual",
    paymentMethod: "",
    brokerCommission: "",
    renewalDate: "",
  });
  const [editingPolicy, setEditingPolicy] = useState(null);

  useEffect(() => {
    loadClients();
    if (id) loadPolicy(id);
  }, [id]);

  const loadClients = async () => {
    try {
      const data = await request("/api/clients");
      setClients(data);
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to load clients", "error");
    }
  };

  const formatDateForInput = (dateStr) => {
    if (!dateStr) return "";
    return new Date(dateStr).toISOString().slice(0, 16);
  };

  const loadPolicy = async (policyId) => {
    try {
      const data = await request(`/api/policies/${policyId}`);
      setInitialValues({
        clientId: data.clientId,
        insurer: data.insurer,
        policyType: data.policyType,
        policyNumber: data.policyNumber,
        startDate: formatDateForInput(data.startDate),
        endDate: formatDateForInput(data.endDate),
        premiumAmount: data.premiumAmount,
        currency: data.currency,
        status: data.status,

        // new fields
        coverageAmount: data.coverageAmount || "",
        paymentFrequency: data.paymentFrequency || "Annual",
        paymentMethod: data.paymentMethod || "",
        brokerCommission: data.brokerCommission || "",
        renewalDate: formatDateForInput(data.renewalDate),
      });
      setEditingPolicy(data);
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to load policy", "error");
    }
  };

  const handleSubmit = async (values) => {
    try {
      const payload = {
        ...values,
        startDate: values.startDate ? new Date(values.startDate).toISOString() : null,
        endDate: values.endDate ? new Date(values.endDate).toISOString() : null,
        renewalDate: values.renewalDate ? new Date(values.renewalDate).toISOString() : null,
      };

      if (editingPolicy) {
        await request(`/api/policies/${editingPolicy.id}`, {
          method: "PUT",
          body: JSON.stringify(payload),
        });
        showSnackbar("Policy updated successfully");
      } else {
        await request("/api/policies", {
          method: "POST",
          body: JSON.stringify(payload),
        });
        showSnackbar("Policy created successfully");
      }
      navigate("/policies");
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to save policy", "error");
    }
  };

  return (
    <Box p={3}>
      <Paper sx={{ p: 3, borderRadius: 2 }}>
        <Typography variant="h5" gutterBottom>
          {editingPolicy ? "Edit Policy" : "New Policy"}
        </Typography>

        <PolicyForm
          initialValues={initialValues}
          onSubmit={handleSubmit}
          clients={clients}
          editingPolicy={editingPolicy}
        />

        <Box mt={2}>
          <Button variant="outlined" onClick={() => navigate("/policies")}>
            Cancel
          </Button>
        </Box>
      </Paper>

      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={handleClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert severity={snackbar.severity} onClose={handleClose}>
          {snackbar.message}
        </Alert>
      </Snackbar>
    </Box>
  );
}
