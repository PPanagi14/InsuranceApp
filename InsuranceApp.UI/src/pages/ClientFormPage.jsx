import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Box, Typography, Paper, Button, Snackbar, Alert } from "@mui/material";
import { useApi } from "../api";
import ClientForm from "../components/ClientForm";
import { useSnackbar } from "../hooks/useSnackbar";

// 🔹 Default values (matches backend Client entity)
const defaultInitialValues = {
  type: "Person",

  // Person
  firstName: "",
  lastName: "",
  dateOfBirth: "",

  // Company
  companyName: "",
  vatNumber: "",

  // Contact
  email: "",
  phoneMobile: "",
  street: "",
  city: "",
  postalCode: "",
  country: "",

  // Notes
  notes: "",
};

export default function ClientFormPage() {
  const { id } = useParams();
  const { request } = useApi();
  const navigate = useNavigate();
  const { snackbar, showSnackbar, handleClose } = useSnackbar();

  const [initialValues, setInitialValues] = useState(defaultInitialValues);
  const [editingClient, setEditingClient] = useState(null);

  // 🔹 Load client if editing
  useEffect(() => {
    if (id) loadClient(id);
  }, [id]);

  const loadClient = async (clientId) => {
    try {
      const data = await request(`/api/clients/${clientId}`);

      // Map API response into Formik shape
      setInitialValues({
        type: data.type,
        firstName: data.firstName || "",
        lastName: data.lastName || "",
        dateOfBirth: data.dateOfBirth
          ? data.dateOfBirth.split("T")[0] // format "yyyy-MM-dd" for date input
          : "",

        companyName: data.companyName || "",
        vatNumber: data.vatNumber || "",

        email: data.email,
        phoneMobile: data.phoneMobile || "",
        street: data.street || "",
        city: data.city || "",
        postalCode: data.postalCode || "",
        country: data.country || "",

        notes: data.notes || "",
      });

      setEditingClient(data);
    // eslint-disable-next-line no-unused-vars
    } catch (err) {
      showSnackbar("Failed to load client", "error");
    }
  };

  // 🔹 Handle submit (create or update)
  const handleSubmit = async (values) => {
    try {
      const payload = {
        ...values,
        dateOfBirth: values.dateOfBirth
          ? new Date(values.dateOfBirth).toISOString()
          : null,
      };

      if (editingClient) {
        await request(`/api/clients/${editingClient.id}`, {
          method: "PUT",
          body: JSON.stringify(payload),
        });
        showSnackbar("Client updated successfully");
      } else {
        await request("/api/clients", {
          method: "POST",
          body: JSON.stringify(payload),
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
