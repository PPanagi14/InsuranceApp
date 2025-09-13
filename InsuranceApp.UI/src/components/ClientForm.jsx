// src/components/ClientForm.jsx
import { TextField, MenuItem, Box, Button } from "@mui/material";
import { Formik, Form } from "formik";
import * as Yup from "yup";

// Validation schema
const ClientSchema = Yup.object().shape({
  type: Yup.string().required("Client type is required"),
  firstName: Yup.string().when("type", {
    is: "Person",
    then: (schema) => schema.required("First name is required"),
  }),
  lastName: Yup.string().when("type", {
    is: "Person",
    then: (schema) => schema.required("Last name is required"),
  }),
  companyName: Yup.string().when("type", {
    is: "Company",
    then: (schema) => schema.required("Company name is required"),
  }),
  email: Yup.string().email("Invalid email").required("Email is required"),
  phone: Yup.string().required("Phone is required"),
  city: Yup.string().required("City is required"),
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
            </>
          ) : (
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
          )}

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
            name="phone"
            value={values.phone}
            onChange={handleChange}
            error={touched.phone && Boolean(errors.phone)}
            helperText={touched.phone && errors.phone}
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
