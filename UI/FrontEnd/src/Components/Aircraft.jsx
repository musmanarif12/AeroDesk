import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Aircraft() {
  const [aircrafts, setAircrafts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchAircrafts = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Aircrafts`)
      .then((res) => {
        setAircrafts(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Aircrafts API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchAircrafts();
  }, [fetchAircrafts]);

  return (
    <DataTable
      title="Aircrafts"
      subtitle="Fleet inventory, models, seat capacities, and operational status"
      icon="🛩️"
      data={aircrafts}
      loading={loading}
      error={error}
      onRefresh={fetchAircrafts}
    />
  );
}

export default Aircraft;