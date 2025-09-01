import { useEffect, useState } from "react";
import { DataGrid } from "@mui/x-data-grid";
import { useApi } from "../api";
import {
  Typography,
  Box,
  Paper,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  MenuItem,
  IconButton,
} from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";

export default function Clients() {
  const { request } = useApi();
  const [clients, setClients] = useState([]);
  const [loading, setLoading] = useState(true);

  // Dialog state
  const [open, setOpen] = useState(false);
  const [editingClient, setEditingClient] = useState(null);
  const [form, setForm] = useState({
    type: "Person",
    firstName: "",
    lastName: "",
    companyName: "",
    email: "",
    phone: "",
    city: "",
  });

  // DataGrid columns
  const columns = [
    { field: "id", headerName: "ID", width: 200 },
    { field: "type", headerName: "Type", width: 120 },
    { field: "firstName", headerName: "First Name", width: 150 },
    { field: "lastName", headerName: "Last Name", width: 150 },
    { field: "companyName", headerName: "Company", width: 200 },
    { field: "email", headerName: "Email", width: 220 },
    { field: "phoneMobile", headerName: "Phone", width: 160 },
    { field: "city", headerName: "City", width: 150 },
    {
      field: "actions",
      headerName: "Actions",
      width: 120,
      renderCell: (params) => (
        <>
          <IconButton
            color="primary"
            onClick={() => handleEdit(params.row)}
          >
            <Edit />
          </IconButton>
          <IconButton
            color="error"
            onClick={() => handleDelete(params.row.id)}
          >
            <Delete />
          </IconButton>
        </>
      ),
    },
  ];

  // Load clients
  useEffect(() => {
    loadClients();
  }, []);

  const loadClients = async () => {
    try {
      setLoading(true);
      const data = await request("/api/clients");
      setClients(data);
    } catch (err) {
      console.error("Failed to load clients:", err);
    } finally {
      setLoading(false);
    }
  };

  // Handle form change
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  // Submit (add or update)
  const handleSubmit = async () => {
    try {
      if (editingClient) {
        await request(`/api/clients/${editingClient.id}`, {
          method: "PUT",
          body: JSON.stringify(form),
        });
      } else {
        await request("/api/clients", {
          method: "POST",
          body: JSON.stringify(form),
        });
      }
      setOpen(false);
      setEditingClient(null);
      resetForm();
      loadClients();
    } catch (err) {
      alert("Failed to save client",err);
    }
  };

  // Edit client
  const handleEdit = (client) => {
    setEditingClient(client);
    setForm({
      type: client.type,
      firstName: client.firstName || "",
      lastName: client.lastName || "",
      companyName: client.companyName || "",
      email: client.email,
      phone: client.phoneMobile,
      city: client.city || "",
    });
    setOpen(true);
  };

  // Delete client
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this client?")) return;
    try {
      await request(`/api/clients/${id}`, { method: "DELETE" });
      loadClients();
    } catch {
      alert("Failed to delete client");
    }
  };

  const resetForm = () => {
    setForm({
      type: "Person",
      firstName: "",
      lastName: "",
      companyName: "",
      email: "",
      phone: "",
      city: "",
    });
  };

  return (
    <>
    <Box p={3}>
      <Typography variant="h4" gutterBottom>
        Clients
      </Typography>

      <Button
        variant="contained"
        onClick={() => {
          resetForm();
          setEditingClient(null);
          setOpen(true);
        }}
        sx={{ mb: 2 }}
      >
        + Add Client
      </Button>

      <Paper sx={{ height: 500, width: "100%" }}>
        <DataGrid
          rows={clients}
          columns={columns}
          loading={loading}
          pageSize={10}
          rowsPerPageOptions={[5, 10, 20]}
          disableRowSelectionOnClick
          getRowId={(row) => row.id}
        />
      </Paper>

      {/* Add/Edit Client Dialog */}
      <Dialog open={open} onClose={() => setOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle>{editingClient ? "Edit Client" : "Add Client"}</DialogTitle>
        <DialogContent>
          {console.log(form)}
          <TextField
            select
            fullWidth
            margin="normal"
            label="Client Type"
            name="type"
            value={form.type}
            onChange={handleChange}
          >
            <MenuItem value="Person">Person</MenuItem>
            <MenuItem value="Company">Company</MenuItem>
          </TextField>

          {form.type === "Person" ? (
            <>
              <TextField
                fullWidth
                margin="normal"
                label="First Name"
                name="firstName"
                value={form.firstName}
                onChange={handleChange}
              />
              <TextField
                fullWidth
                margin="normal"
                label="Last Name"
                name="lastName"
                value={form.lastName}
                onChange={handleChange}
              />
            </>
          ) : (
            <TextField
              fullWidth
              margin="normal"
              label="Company Name"
              name="companyName"
              value={form.companyName}
              onChange={handleChange}
            />
          )}

          <TextField
            fullWidth
            margin="normal"
            label="Email"
            name="email"
            value={form.email}
            onChange={handleChange}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Phone"
            name="phone"
            value={form.phone}
            onChange={handleChange}
          />
          <TextField
            fullWidth
            margin="normal"
            label="City"
            name="city"
            value={form.city}
            onChange={handleChange}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpen(false)}>Cancel</Button>
          <Button variant="contained" onClick={handleSubmit}>
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
    </>
  );
}
