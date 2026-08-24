import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function BoardingPasses() {
  const [boardingPasses, setBoardingPasses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchBoardingPasses = useCallback(() => {
    setLoading(true);
    setError(null);
    axios
      .get(`${API_BASE}/api/BoardingPasses`)
      .then((res) => {
        setBoardingPasses(res.data || []);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.response?.data?.message || err.message || "Failed to connect to Boarding Passes API");
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    fetchBoardingPasses();
  }, [fetchBoardingPasses]);

  return (
    <DataTable
      title="Boarding Passes"
      subtitle="Issued boarding passes, seat assignments, gate numbers, and QR validation"
      icon="🎫"
      data={boardingPasses}
      loading={loading}
      error={error}
      onRefresh={fetchBoardingPasses}
    />
  );
}

export default BoardingPasses;