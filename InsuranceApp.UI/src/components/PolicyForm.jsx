import { TextField, MenuItem, Box, Button } from "@mui/material";
import { Formik, Form } from "formik";
import * as Yup from "yup";

const PolicySchema = Yup.object().shape({
  policyNumber: Yup.string().required("Policy number is required"),
  insurer: Yup.string().required("Insurer is required"),
  policyType: Yup.string().required("Policy type is required"),
  startDate: Yup.date()
    .required("Start date is required")
    .typeError("Invalid start date"),
  endDate: Yup.date()
    .required("End date is required")
    .typeError("Invalid end date")
    .min(Yup.ref("startDate"), "End date must be after start date"),
  premiumAmount: Yup.number()
    .required("Premium amount is required")
    .positive("Premium must be positive"),
  currency: Yup.string().required("Currency is required"),
  status: Yup.string().required("Status is required"),
  clientId: Yup.string().required("Client is required"),

  // 🔹 New fields
  coverageAmount: Yup.number()
    .nullable()
    .min(0, "Coverage amount cannot be negative"),
  paymentFrequency: Yup.string()
    .oneOf(["Monthly", "Quarterly", "SemiAnnual", "Annual"])
    .required("Payment frequency is required"),
  paymentMethod: Yup.string()
    .nullable()
    .oneOf(["Card", "Bank", "Cash"], "Invalid payment method"),
  brokerCommission: Yup.number()
    .nullable()
    .min(0, "Commission cannot be negative"),
  renewalDate: Yup.date()
    .nullable()
    .typeError("Invalid renewal date"),
});

export default function PolicyForm({ initialValues, onSubmit, clients, editingPolicy }) {
  return (
     <Formik
      initialValues={initialValues}
      validationSchema={PolicySchema}
      onSubmit={onSubmit}
      enableReinitialize
    >
      {({ values, errors, touched, handleChange }) => (
        <Form>
          {/* Basic Info */}
          <TextField
            fullWidth
            margin="normal"
            label="Policy Number"
            name="policyNumber"
            value={values.policyNumber}
            onChange={handleChange}
            error={touched.policyNumber && Boolean(errors.policyNumber)}
            helperText={touched.policyNumber && errors.policyNumber}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Insurer"
            name="insurer"
            value={values.insurer}
            onChange={handleChange}
            error={touched.insurer && Boolean(errors.insurer)}
            helperText={touched.insurer && errors.insurer}
          />
          <TextField
            select
            fullWidth
            margin="normal"
            label="Policy Type"
            name="policyType"
            value={values.policyType}
            onChange={handleChange}
            error={touched.policyType && Boolean(errors.policyType)}
            helperText={touched.policyType && errors.policyType}
          >
            <MenuItem value="Auto">Auto</MenuItem>
            <MenuItem value="Home">Home</MenuItem>
            <MenuItem value="Life">Life</MenuItem>
            <MenuItem value="Health">Health</MenuItem>
            <MenuItem value="Travel">Travel</MenuItem>
            <MenuItem value="Business">Business</MenuItem>
            <MenuItem value="Other">Other</MenuItem>
          </TextField>

          {/* Dates */}
          <TextField
            type="datetime-local"
            fullWidth
            margin="normal"
            label="Start Date"
            name="startDate"
            value={values.startDate}
            onChange={handleChange}
            error={touched.startDate && Boolean(errors.startDate)}
            helperText={touched.startDate && errors.startDate}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            type="datetime-local"
            fullWidth
            margin="normal"
            label="End Date"
            name="endDate"
            value={values.endDate}
            onChange={handleChange}
            error={touched.endDate && Boolean(errors.endDate)}
            helperText={touched.endDate && errors.endDate}
            InputLabelProps={{ shrink: true }}
          />

          {/* Financials */}
          <TextField
            fullWidth
            margin="normal"
            label="Premium Amount"
            name="premiumAmount"
            type="number"
            value={values.premiumAmount}
            onChange={handleChange}
            error={touched.premiumAmount && Boolean(errors.premiumAmount)}
            helperText={touched.premiumAmount && errors.premiumAmount}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Coverage Amount"
            name="coverageAmount"
            type="number"
            value={values.coverageAmount}
            onChange={handleChange}
            error={touched.coverageAmount && Boolean(errors.coverageAmount)}
            helperText={touched.coverageAmount && errors.coverageAmount}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Broker Commission"
            name="brokerCommission"
            type="number"
            value={values.brokerCommission}
            onChange={handleChange}
            error={touched.brokerCommission && Boolean(errors.brokerCommission)}
            helperText={touched.brokerCommission && errors.brokerCommission}
          />
          <TextField
            select
            fullWidth
            margin="normal"
            label="Currency"
            name="currency"
            value={values.currency}
            onChange={handleChange}
            error={touched.currency && Boolean(errors.currency)}
            helperText={touched.currency && errors.currency}
          >
            <MenuItem value="EUR">EUR (€)</MenuItem>
            <MenuItem value="USD">USD ($)</MenuItem>
            <MenuItem value="GBP">GBP (£)</MenuItem>
          </TextField>

          {/* Payment Info */}
          <TextField
            select
            fullWidth
            margin="normal"
            label="Payment Frequency"
            name="paymentFrequency"
            value={values.paymentFrequency}
            onChange={handleChange}
            error={touched.paymentFrequency && Boolean(errors.paymentFrequency)}
            helperText={touched.paymentFrequency && errors.paymentFrequency}
          >
            <MenuItem value="Monthly">Monthly</MenuItem>
            <MenuItem value="Quarterly">Quarterly</MenuItem>
            <MenuItem value="SemiAnnual">Semi-Annual</MenuItem>
            <MenuItem value="Annual">Annual</MenuItem>
          </TextField>
          <TextField
            select
            fullWidth
            margin="normal"
            label="Payment Method"
            name="paymentMethod"
            value={values.paymentMethod}
            onChange={handleChange}
            error={touched.paymentMethod && Boolean(errors.paymentMethod)}
            helperText={touched.paymentMethod && errors.paymentMethod}
          >
            <MenuItem value="Card">Card</MenuItem>
            <MenuItem value="Bank">Bank Transfer</MenuItem>
            <MenuItem value="Cash">Cash</MenuItem>
          </TextField>
          <TextField
            type="datetime-local"
            fullWidth
            margin="normal"
            label="Renewal Date"
            name="renewalDate"
            value={values.renewalDate}
            onChange={handleChange}
            error={touched.renewalDate && Boolean(errors.renewalDate)}
            helperText={touched.renewalDate && errors.renewalDate}
            InputLabelProps={{ shrink: true }}
          />

          {/* Status + Client */}
          <TextField
            select
            fullWidth
            margin="normal"
            label="Status"
            name="status"
            value={values.status}
            onChange={handleChange}
            error={touched.status && Boolean(errors.status)}
            helperText={touched.status && errors.status}
          >
            <MenuItem value="Active">Active</MenuItem>
            <MenuItem value="Pending">Pending</MenuItem>
            <MenuItem value="Expired">Expired</MenuItem>
            <MenuItem value="Cancelled">Cancelled</MenuItem>
          </TextField>
          <TextField
            select
            fullWidth
            margin="normal"
            label="Client"
            name="clientId"
            value={values.clientId}
            onChange={handleChange}
            error={touched.clientId && Boolean(errors.clientId)}
            helperText={touched.clientId && errors.clientId}
          >
            {clients.map((c) => (
              <MenuItem key={c.id} value={c.id}>
                {c.type === "Person"
                  ? `${c.firstName} ${c.lastName}`
                  : c.companyName}
              </MenuItem>
            ))}
          </TextField>

          {/* Actions */}
          <Box mt={2} display="flex" justifyContent="flex-end" gap={2}>
            <Button type="submit" variant="contained">
              {editingPolicy ? "Update Policy" : "Create Policy"}
            </Button>
          </Box>
        </Form>
      )}
    </Formik>
  );
}
