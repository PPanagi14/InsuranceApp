import {
  Card,
  CardContent,
  Typography,
  Box,
  Chip,
  Divider,
  Button,
  Stack,
} from "@mui/material";
import { Event, Person, WarningAmber } from "@mui/icons-material";
import { formatDateTime, isExpiringSoon } from "../utils/date";
import { useNavigate } from "react-router-dom";



export function PolicyCard({ policy, client }) {
  const expiringSoon = isExpiringSoon(policy.endDate);
    const navigate = useNavigate();


  return (
    <Card sx={{ borderRadius: 3, boxShadow: 2 }}>
      <CardContent>
        {/* Header row */}
        <Box display="flex" justifyContent="space-between" alignItems="center" mb={1}>
          <Typography variant="subtitle1" fontWeight="bold">
            {policy.policyNumber}
          </Typography>
          <Stack direction="row" spacing={1}>
            {expiringSoon && (
              <Chip
                icon={<WarningAmber />}
                label="Expiring Soon"
                color="warning"
                size="small"
              />
            )}
            <Chip
              label={policy.status}
              color={policy.status === "Active" ? "success" : "default"}
              size="small"
            />
          </Stack>
        </Box>

        <Typography variant="body2" color="text.secondary" gutterBottom>
          {policy.policyType} Insurance
        </Typography>

        {/* Client info */}
        <Box display="flex" alignItems="center" gap={1} mb={0.5}>
          <Person fontSize="small" color="action" />
          <Typography variant="body2">
            {client?.firstName} {client?.lastName}
          </Typography>
        </Box>

        {/* Dates with time */}
        <Box display="flex" alignItems="center" gap={1}>
          <Event fontSize="small" color="action" />
          <Typography variant="body2">
            {formatDateTime(policy.startDate)}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            – {formatDateTime(policy.endDate)}
          </Typography>
        </Box>

        <Divider sx={{ my: 1 }} />

        {/* Premium */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="success.main" fontWeight="bold">
            €{policy.premiumAmount.toLocaleString()}
          </Typography>
          {policy.commission && (
            <Typography variant="body2" color="text.secondary">
              Commission: €{policy.commission}
            </Typography>
          )}
        </Box>

        {/* Actions */}
        <Box display="flex" justifyContent="flex-end" gap={1}>
          <Button
            size="small"
            variant="outlined"
            onClick={() => navigate(`/policies/${policy.id}`)}
          >
            View Details
          </Button>
          <Button size="small" variant="contained">
            Renew
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
}
