import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Flight() {
  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchFlights = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Flights`)
      .then((res) => {
        setFlights(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Flights API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchFlights();
  }, [fetchFlights]);

  return (
    <DataTable
      title="Flights"
      subtitle="Overview of active, scheduled, and departed flight schedules"
      icon="✈️"
      data={flights}
      loading={loading}
      error={error}
      onRefresh={fetchFlights}
    />
  );
}

export default Flight;