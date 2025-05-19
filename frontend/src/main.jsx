import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route, Navigate } from "react-router";
import Home from "./pages/Home.jsx";
import Layout from "./components/Layout.jsx";
import AddGroup from "./pages/AddGroup.jsx";
import GroupDetails from "./pages/GroupDetails.jsx";
import AddTransaction from "./pages/AddTransaction.jsx";
import React from "react";
import 'bootstrap/dist/css/bootstrap.min.css';
import "./index.css";

const root = document.getElementById("root");

ReactDOM.createRoot(root).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/home" replace />} />
        <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/add-group" element={<AddGroup />} />
          <Route
            path="/group/:groupId"
            element={<GroupDetails />} 
          />
          <Route
            path="/group/:groupId/add-transaction"
            element={<AddTransaction />}
          />
        </Route>
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
