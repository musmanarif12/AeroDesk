import { useEffect, useState, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Baggage() {
  const [baggage, setBaggage] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchBaggage = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/Baggages`)
      .then((res) => {
        setBaggage(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Baggages API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchBaggage();
  }, [fetchBaggage]);

  return (
    <DataTable
      title="Baggage"
      subtitle="Luggage tracking, weight metrics, tag barcodes, and claim status"
      icon="🧳"
      data={baggage}
      loading={loading}
      error={error}
      onRefresh={fetchBaggage}
    />
  );
}

export default Baggage;