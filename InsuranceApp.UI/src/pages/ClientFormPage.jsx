import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Typography, Paper, Button, Snackbar, Alert } from "@mui/material";
import { useApi } from "../api";
import ClientForm from "../components/ClientForm";
import { useSnackbar } from "../hooks/useSnackbar";

export default function ClientFormPage() {
  const { id } = useParams();
  const { request } = useApi();
  const navigate = useNavigate();
  const { snackbar, showSnackbar, handleClose } = useSnackbar();

  const [initialValues, setInitialValues] = useState({
    type: "Person",
    firstName: "",
    lastName: "",
    companyName: "",
    email: "",
    phone: "",
    city: "",
  });
  const [editingClient, setEditingClient] = useState(null);

  useEffect(() => {
    if (id) loadClient(id);
  }, [id]);

  const loadClient = async (clientId) => {
    try {
      const data = await request(`/api/clients/${clientId}`);
      setInitialValues({
        type: data.type,
        firstName: data.firstName || "",
        lastName: data.lastName || "",
        companyName: data.companyName || "",
        email: data.email || "",
        phone: data.phoneMobile || "",
        city: data.city || "",
      });
      setEditingClient(data);
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to load client", "error");
    }
  };

  const handleSubmit = async (values) => {
    try {
      if (editingClient) {
        await request(`/api/clients/${editingClient.id}`, {
          method: "PUT",
          body: JSON.stringify(values),
        });
        showSnackbar("Client updated successfully");
      } else {
        await request("/api/clients", {
          method: "POST",
          body: JSON.stringify(values),
        });
        showSnackbar("Client created successfully");
      }
      navigate("/clients");
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to save client", "error");
    }
  };

  return (
    <Box p={3}>
      <Paper sx={{ p: 3, borderRadius: 2 }}>
        <Typography variant="h5" gutterBottom>
          {editingClient ? "Edit Client" : "New Client"}
        </Typography>

        <ClientForm
          initialValues={initialValues}
          onSubmit={handleSubmit}
          editingClient={editingClient}
        />

        <Box mt={2}>
          <Button variant="outlined" onClick={() => navigate("/clients")}>
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
