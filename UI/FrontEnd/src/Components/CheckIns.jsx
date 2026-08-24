import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function CheckIns() {
  const [checkIns, setCheckIns] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchCheckIns = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/CheckIns`)
      .then((res) => {
        setCheckIns(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to CheckIns API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchCheckIns();
  }, [fetchCheckIns]);

  return (
    <DataTable
      title="Check-Ins"
      subtitle="Live airport check-in records, desk allocations, and passenger boarding states"
      icon="🛎️"
      data={checkIns}
      loading={loading}
      error={error}
      onRefresh={fetchCheckIns}
    />
  );
}

export default CheckIns;