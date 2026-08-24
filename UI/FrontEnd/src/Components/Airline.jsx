import { useEffect, useState, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Airline() {
  const [airline, setAirline] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchAirlines = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Airlines`)
      .then((res) => {
        setAirline(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Airlines API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchAirlines();
  }, [fetchAirlines]);

  return (
    <DataTable
      title="Airlines"
      subtitle="Partner airline carriers, IATA/ICAO codes, and fleet networks"
      icon="🏢"
      data={airline}
      loading={loading}
      error={error}
      onRefresh={fetchAirlines}
    />
  );
}

export default Airline;