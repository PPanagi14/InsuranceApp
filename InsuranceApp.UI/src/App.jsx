import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import PrivateRoute from "./components/PrivateRoute";

import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Clients from "./pages/Clients";
import Policies from "./pages/Policies";
import Layout from "./components/Layout"; // with Navbar
import ClientDetails from "./pages/ClientDetails";
import PolicyDetails from "./pages/PolicyDetails";
import PolicyFormPage from "./pages/PolicyFormPage";
import ClientFormPage from "./pages/ClientFormPage";


function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Public */}
        <Route path="/login" element={<Login />} />

        {/* Protected with layout (Navbar) */}
        <Route
          element={
            <PrivateRoute>
              <Layout />
            </PrivateRoute>
          }
        >
          
          <Route path="/dashboard" element={<Dashboard />} />
           
           {/* Clients */}
          <Route path="/clients" element={<Clients />} />
          <Route path="/clients/new" element={<ClientFormPage />} />
          <Route path="/clients/:id/edit" element={<ClientFormPage />} />
          <Route path="/clients/:id" element={<ClientDetails />} />

          {/* Policies */}
          <Route path="/policies" element={<Policies />} />
          <Route path="/policies/new" element={<PolicyFormPage />} />
          <Route path="/policies/:id/edit" element={<PolicyFormPage />} />
          <Route path="/policies/:id" element={<PolicyDetails />} />
          </Route>

        {/* Default redirect */}
        <Route path="*" element={<Navigate to="/dashboard" replace />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
