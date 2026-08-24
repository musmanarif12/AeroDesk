import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Passengers() {
  const [passengers, setPassengers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchPassengers = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Passengers`)
      .then((res) => {
        setPassengers(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Passengers API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchPassengers();
  }, [fetchPassengers]);

  return (
    <DataTable
      title="Passengers"
      subtitle="Registered passenger traveler profiles, contact info, and passport IDs"
      icon="👥"
      data={passengers}
      loading={loading}
      error={error}
      onRefresh={fetchPassengers}
    />
  );
}

export default Passengers;