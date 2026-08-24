import { useEffect, useState, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Airport() {
  const [airport, setAirport] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchAirports = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Airports`)
      .then((res) => {
        setAirport(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Airports API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchAirports();
  }, [fetchAirports]);

  return (
    <DataTable
      title="Airports"
      subtitle="International and domestic airport hubs, runways, and location coordinates"
      icon="🛫"
      data={airport}
      loading={loading}
      error={error}
      onRefresh={fetchAirports}
    />
  );
}

export default Airport;