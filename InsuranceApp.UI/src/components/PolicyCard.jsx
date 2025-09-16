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
import { Event, Euro, Business, Repeat, Payment } from "@mui/icons-material";

export function PolicyCard({ policy, client, onView, onRenew }) {
  return (
    <Card sx={{ borderRadius: 3, boxShadow: 3 }}>
      <CardContent>
        {/* Top row: Policy number + Status */}
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Typography variant="h6" fontWeight="bold">
            Policy #{policy.policyNumber}
          </Typography>
          <Chip
            label={policy.status}
            color={
              policy.status === "Active"
                ? "success"
                : policy.status === "Expired"
                ? "error"
                : "default"
            }
            size="small"
          />
        </Box>

        {/* Subheader: Client */}
        <Typography variant="body2" color="text.secondary" gutterBottom>
          {client
            ? client.type === "Person"
              ? `${client.firstName} ${client.lastName}`
              : client.companyName
            : "Unknown Client"}
        </Typography>

        <Divider sx={{ my: 1 }} />

        {/* Policy Info */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Insurer:
          </Typography>
          <Typography variant="body2" fontWeight="bold">
            {policy.insurer}
          </Typography>
        </Box>
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Type:
          </Typography>
          <Typography variant="body2">{policy.policyType}</Typography>
        </Box>

        {/* Dates */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Start:
          </Typography>
          <Typography variant="body2">
            {new Date(policy.startDate).toLocaleDateString()}
          </Typography>
        </Box>
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            End:
          </Typography>
          <Typography variant="body2">
            {new Date(policy.endDate).toLocaleDateString()}
          </Typography>
        </Box>
        {policy.renewalDate && (
          <Box display="flex" justifyContent="space-between" mb={1}>
            <Typography variant="body2" color="text.secondary">
              Renewal:
            </Typography>
            <Typography variant="body2">
              {new Date(policy.renewalDate).toLocaleDateString()}
            </Typography>
          </Box>
        )}

        <Divider sx={{ my: 1 }} />

        {/* Financials */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Premium:
          </Typography>
          <Typography variant="body2" fontWeight="bold" color="success.main">
            {policy.premiumAmount} {policy.currency}
          </Typography>
        </Box>
        {policy.coverageAmount && (
          <Box display="flex" justifyContent="space-between" mb={1}>
            <Typography variant="body2" color="text.secondary">
              Coverage:
            </Typography>
            <Typography variant="body2" fontWeight="bold">
              {policy.coverageAmount} {policy.currency}
            </Typography>
          </Box>
        )}
        {policy.brokerCommission && (
          <Box display="flex" justifyContent="space-between" mb={1}>
            <Typography variant="body2" color="text.secondary">
              Commission:
            </Typography>
            <Typography variant="body2" fontWeight="bold">
              {policy.brokerCommission} {policy.currency}
            </Typography>
          </Box>
        )}

        {/* Payment Info */}
        <Box display="flex" justifyContent="space-between" mb={1}>
          <Typography variant="body2" color="text.secondary">
            Payment Frequency:
          </Typography>
          <Typography variant="body2">{policy.paymentFrequency}</Typography>
        </Box>
        {policy.paymentMethod && (
          <Box display="flex" justifyContent="space-between" mb={1}>
            <Typography variant="body2" color="text.secondary">
              Payment Method:
            </Typography>
            <Typography variant="body2">{policy.paymentMethod}</Typography>
          </Box>
        )}

        <Divider sx={{ my: 1 }} />

        {/* Actions */}
        <Box display="flex" justifyContent="space-between" alignItems="center">
          <Button
            variant="outlined"
            size="small"
            onClick={onView}
          >
            View Details
          </Button>
          <Button
            variant="contained"
            size="small"
            onClick={onRenew}
          >
            Renew
          </Button>
        </Box>
      </CardContent>
    </Card>
  );
}
