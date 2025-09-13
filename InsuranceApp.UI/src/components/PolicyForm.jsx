// src/components/PolicyForm.jsx
import { TextField, MenuItem, Box, Button } from "@mui/material";
import { Formik, Form } from "formik";
import * as Yup from "yup";

const PolicySchema = Yup.object().shape({
  policyNumber: Yup.string().required("Policy number is required"),
  insurer: Yup.string().required("Insurer is required"),
  policyType: Yup.string().required("Policy type is required"),
  startDate: Yup.string().required("Start date is required"),
  endDate: Yup.string().required("End date is required"),
  premiumAmount: Yup.number().required("Premium amount is required"),
  currency: Yup.string().required("Currency is required"),
  status: Yup.string().required("Status is required"),
  clientId: Yup.string().required("Client is required"),
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
            <MenuItem value="Health">Health</MenuItem>
            <MenuItem value="Life">Life</MenuItem>
            <MenuItem value="Property">Property</MenuItem>
          </TextField>
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
