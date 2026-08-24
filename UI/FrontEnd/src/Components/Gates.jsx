import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Gates() {
  const [gates, setGates] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchGates = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Gates`)
      .then((res) => {
        setGates(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Gates API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchGates();
  }, [fetchGates]);

  return (
    <DataTable
      title="Gates"
      subtitle="Airport terminal gate management, availability, and scheduled flight assignments"
      icon="🚪"
      data={gates}
      loading={loading}
      error={error}
      onRefresh={fetchGates}
    />
  );
}

export default Gates;