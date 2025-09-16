import { TextField, MenuItem, Box, Button } from "@mui/material";
import { Formik, Form } from "formik";
import * as Yup from "yup";

// Validation schema
const ClientSchema = Yup.object().shape({
  type: Yup.string().required("Client type is required"),

  // Person rules
  firstName: Yup.string().when("type", {
    is: "Person",
    then: (schema) => schema.required("First name is required"),
  }),
  lastName: Yup.string().when("type", {
    is: "Person",
    then: (schema) => schema.required("Last name is required"),
  }),
  dateOfBirth: Yup.date().when("type", {
    is: "Person",
    then: (schema) =>
      schema
        .max(new Date(), "Date of birth must be in the past")
        .required("Date of birth is required"),
  }),

  // Company rules
  companyName: Yup.string().when("type", {
    is: "Company",
    then: (schema) => schema.required("Company name is required"),
  }),
  vatNumber: Yup.string().when("type", {
    is: "Company",
    then: (schema) => schema.required("VAT number is required"),
  }),

  // Common rules
  email: Yup.string().email("Invalid email").required("Email is required"),
  phoneMobile: Yup.string()
    .required("Phone is required")
    .matches(/^\+?\d{7,15}$/, "Invalid phone number format"),
  street: Yup.string().nullable(),
  city: Yup.string().required("City is required"),
  postalCode: Yup.string().nullable(),
  country: Yup.string().nullable(),
  notes: Yup.string().max(1000, "Notes cannot exceed 1000 characters"),
});

export default function ClientForm({ initialValues, onSubmit, editingClient }) {
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={ClientSchema}
      onSubmit={onSubmit}
      enableReinitialize
    >
      {({ values, errors, touched, handleChange }) => (
        <Form>
          {/* Type */}
          <TextField
            select
            fullWidth
            margin="normal"
            label="Client Type"
            name="type"
            value={values.type}
            onChange={handleChange}
            error={touched.type && Boolean(errors.type)}
            helperText={touched.type && errors.type}
          >
            <MenuItem value="Person">Person</MenuItem>
            <MenuItem value="Company">Company</MenuItem>
          </TextField>

          {/* Conditional fields */}
          {values.type === "Person" ? (
            <>
              <TextField
                fullWidth
                margin="normal"
                label="First Name"
                name="firstName"
                value={values.firstName}
                onChange={handleChange}
                error={touched.firstName && Boolean(errors.firstName)}
                helperText={touched.firstName && errors.firstName}
              />
              <TextField
                fullWidth
                margin="normal"
                label="Last Name"
                name="lastName"
                value={values.lastName}
                onChange={handleChange}
                error={touched.lastName && Boolean(errors.lastName)}
                helperText={touched.lastName && errors.lastName}
              />
              <TextField
                fullWidth
                margin="normal"
                type="date"
                label="Date of Birth"
                name="dateOfBirth"
                value={values.dateOfBirth}
                onChange={handleChange}
                InputLabelProps={{ shrink: true }}
                error={touched.dateOfBirth && Boolean(errors.dateOfBirth)}
                helperText={touched.dateOfBirth && errors.dateOfBirth}
              />
            </>
          ) : (
            <>
              <TextField
                fullWidth
                margin="normal"
                label="Company Name"
                name="companyName"
                value={values.companyName}
                onChange={handleChange}
                error={touched.companyName && Boolean(errors.companyName)}
                helperText={touched.companyName && errors.companyName}
              />
              <TextField
                fullWidth
                margin="normal"
                label="VAT Number"
                name="vatNumber"
                value={values.vatNumber}
                onChange={handleChange}
                error={touched.vatNumber && Boolean(errors.vatNumber)}
                helperText={touched.vatNumber && errors.vatNumber}
              />
            </>
          )}

          {/* Contact */}
          <TextField
            fullWidth
            margin="normal"
            label="Email"
            name="email"
            type="email"
            value={values.email}
            onChange={handleChange}
            error={touched.email && Boolean(errors.email)}
            helperText={touched.email && errors.email}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Phone"
            name="phoneMobile"
            value={values.phoneMobile}
            onChange={handleChange}
            error={touched.phoneMobile && Boolean(errors.phoneMobile)}
            helperText={touched.phoneMobile && errors.phoneMobile}
          />

          {/* Address */}
          <TextField
            fullWidth
            margin="normal"
            label="Street"
            name="street"
            value={values.street}
            onChange={handleChange}
            error={touched.street && Boolean(errors.street)}
            helperText={touched.street && errors.street}
          />
          <TextField
            fullWidth
            margin="normal"
            label="City"
            name="city"
            value={values.city}
            onChange={handleChange}
            error={touched.city && Boolean(errors.city)}
            helperText={touched.city && errors.city}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Postal Code"
            name="postalCode"
            value={values.postalCode}
            onChange={handleChange}
            error={touched.postalCode && Boolean(errors.postalCode)}
            helperText={touched.postalCode && errors.postalCode}
          />
          <TextField
            fullWidth
            margin="normal"
            label="Country"
            name="country"
            value={values.country}
            onChange={handleChange}
            error={touched.country && Boolean(errors.country)}
            helperText={touched.country && errors.country}
          />

          {/* Notes */}
          <TextField
            fullWidth
            margin="normal"
            label="Notes"
            name="notes"
            multiline
            rows={3}
            value={values.notes}
            onChange={handleChange}
            error={touched.notes && Boolean(errors.notes)}
            helperText={touched.notes && errors.notes}
          />

          {/* Actions */}
          <Box mt={2} display="flex" justifyContent="flex-end" gap={2}>
            <Button type="submit" variant="contained">
              {editingClient ? "Update Client" : "Create Client"}
            </Button>
          </Box>
        </Form>
      )}
    </Formik>
  );
}
