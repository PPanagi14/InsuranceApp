import {
  Card,
  CardContent,
  Typography,
  Box,
  Chip,
  Divider,
  Button,
  IconButton,
} from "@mui/material";
import { Email, Phone, LocationOn } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

export function ClientCard({ client }) {
  const navigate = useNavigate();
  console.log("ClientCard client:", client);
  return (
    <Card sx={{ borderRadius: 3, boxShadow: 3 }}>
      <CardContent>
        {/* Top row: Name + Status */}
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Typography variant="h6" fontWeight="bold">
            {client.firstName} {client.lastName}
          </Typography>
          <Chip
            label="Active"
            color="success" 
            size="small"
          />
          {/* <Chip
            label={client.status}
            color={client.status === "Active" ? "success" : "default"}
            size="small"
          /> */}
        </Box>

        {/* Subheader */}
        <Typography variant="body2" color="text.secondary" gutterBottom>
          Client since {new Date(client.startDate).toLocaleDateString()}
        </Typography>

        {/* Contact info */}
        <Box display="flex" alignItems="center" gap={1} mb={0.5}>
          <Email fontSize="small" color="action" />
          <Typography variant="body2">{client.email}</Typography>
        </Box>
        <Box display="flex" alignItems="center" gap={1} mb={0.5}>
          <Phone fontSize="small" color="action" />
          <Typography variant="body2">{client.phoneMobile}</Typography>
        </Box>
        <Box display="flex" alignItems="center" gap={1} mb={1}>
          <LocationOn fontSize="small" color="action" />
          <Typography variant="body2">{client.address}</Typography>
        </Box>

        <Divider sx={{ my: 1 }} />

        {/* Summary */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Policies:
          </Typography>
          <Typography variant="body2" fontWeight="bold">
            {client.policiesCount}
          </Typography>
        </Box>
        <Box display="flex" justifyContent="space-between" mb={2}>
          <Typography variant="body2" color="text.secondary">
            Total Premium:
          </Typography>
          <Typography variant="body2" fontWeight="bold" color="success.main">
            {/* ${client.totalPremium?.toLocaleString()} */}
          </Typography>
        </Box>

        <Divider sx={{ mb: 1 }} />

        {/* Actions */}
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Button
            variant="outlined"
            size="small"
            onClick={() => navigate(`/clients/${client.id}`)}
          >
            View Details
          </Button>
          <Box>
            <IconButton color="primary" size="small">
              <Phone />
            </IconButton>
            <IconButton color="primary" size="small">
              <Email />
            </IconButton>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
}
