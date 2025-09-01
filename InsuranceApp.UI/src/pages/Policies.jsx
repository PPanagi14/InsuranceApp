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
  IconButton,
  MenuItem,
} from "@mui/material";
import { Edit, Delete } from "@mui/icons-material";

export default function Policies() {
  const { request } = useApi();
  const [policies, setPolicies] = useState([]);
  const [loading, setLoading] = useState(true);

  // Dialog state
  const [open, setOpen] = useState(false);
  const [editingPolicy, setEditingPolicy] = useState(null);
  const [form, setForm] = useState({
    policyNumber: "",
    premiumAmount: "",
    startDate: "",
    endDate: "",
    status: "Active",
    clientId: "", // important to link to a client
  });

  // DataGrid columns
  const columns = [
    { field: "policyNumber", headerName: "Policy #", width: 150 },
    { field: "premiumAmount", headerName: "Premium (€)", width: 150 },
    { field: "startDate", headerName: "Start Date", width: 150 },
    { field: "endDate", headerName: "End Date", width: 150 },
    { field: "status", headerName: "Status", width: 120 },
    { field: "clientId", headerName: "Client ID", width: 220 },
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

  // Load policies
  useEffect(() => {
    loadPolicies();
  }, []);

  const loadPolicies = async () => {
    try {
      setLoading(true);
      const data = await request("/api/policies"); // make sure backend has this GET
      setPolicies(data);
    } catch (err) {
      console.error("Failed to load policies:", err);
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
      if (editingPolicy) {
        await request(`/api/policies/${editingPolicy.id}`, {
          method: "PUT",
          body: JSON.stringify(form),
        });
      } else {
        await request("/api/policies", {
          method: "POST",
          body: JSON.stringify(form),
        });
      }
      setOpen(false);
      setEditingPolicy(null);
      resetForm();
      loadPolicies();
    } catch (err) {
      alert("Failed to save policy", err);
    }
  };

  // Edit
  const handleEdit = (policy) => {
    setEditingPolicy(policy);
    setForm({
      policyNumber: policy.policyNumber,
      premiumAmount: policy.premiumAmount,
      startDate: policy.startDate,
      endDate: policy.endDate,
      status: policy.status,
      clientId: policy.clientId,
    });
    setOpen(true);
  };

  // Delete
  const handleDelete = async (id) => {
    if (!window.confirm("Are you sure you want to delete this policy?")) return;
    try {
      await request(`/api/policies/${id}`, { method: "DELETE" });
      loadPolicies();
    } catch {
      alert("Failed to delete policy");
    }
  };

  const resetForm = () => {
    setForm({
      policyNumber: "",
      premiumAmount: "",
      startDate: "",
      endDate: "",
      status: "Active",
      clientId: "",
    });
  };

  return (
    <Box p={3}>
      <Typography variant="h4" gutterBottom>
        Policies
      </Typography>

      <Button
        variant="contained"
        onClick={() => {
          resetForm();
          setEditingPolicy(null);
          setOpen(true);
        }}
        sx={{ mb: 2 }}
      >
        + Add Policy
      </Button>

      <Paper sx={{ height: 500, width: "100%" }}>
        <DataGrid
          rows={policies}
          columns={columns}
          loading={loading}
          pageSize={10}
          rowsPerPageOptions={[5, 10, 20]}
          disableRowSelectionOnClick
          getRowId={(row) => row.id}
        />
      </Paper>

      {/* Add/Edit Policy Dialog */}
      <Dialog open={open} onClose={() => setOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle>{editingPolicy ? "Edit Policy" : "Add Policy"}</DialogTitle>
        <DialogContent>
          <TextField
            fullWidth
            margin="normal"
            label="Policy Number"
            name="policyNumber"
            value={form.policyNumber}
            onChange={handleChange}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Premium Amount (€)"
            name="premiumAmount"
            value={form.premiumAmount}
            onChange={handleChange}
          />
          <TextField
            type="date"
            fullWidth
            margin="normal"
            label="Start Date"
            name="startDate"
            value={form.startDate}
            onChange={handleChange}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            type="date"
            fullWidth
            margin="normal"
            label="End Date"
            name="endDate"
            value={form.endDate}
            onChange={handleChange}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            select
            fullWidth
            margin="normal"
            label="Status"
            name="status"
            value={form.status}
            onChange={handleChange}
          >
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Expired">Expired</MenuItem>
            <MenuItem value="Cancelled">Cancelled</MenuItem>
          </TextField>
          <TextField
            fullWidth
            margin="normal"
            label="Client ID"
            name="clientId"
            value={form.clientId}
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
  );
}
