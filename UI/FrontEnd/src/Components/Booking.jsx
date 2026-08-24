import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Bookings() {
  const [bookings, setBookings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchBookings = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Bookings`)
      .then((res) => {
        setBookings(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Bookings API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchBookings();
  }, [fetchBookings]);

  return (
    <DataTable
      title="Bookings"
      subtitle="Passenger reservations, ticket fares, booking reference codes, and status"
      icon="📑"
      data={bookings}
      loading={loading}
      error={error}
      onRefresh={fetchBookings}
    />
  );
}

export default Bookings;