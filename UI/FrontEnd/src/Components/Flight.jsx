import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

function Flight() {
  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Server-side pagination & search states
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(8);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState("");

  const fetchFlights = useCallback(
    (page = pageNumber, search = searchTerm) => {
      setLoading(true);
      setError(null);

      axios
        .get(`${API_BASE}/api/Flights`, {
          params: {
            pageNumber: page,
            pageSize: pageSize,
            searchTerm: search.trim() || undefined,
          },
        })
        .then((res) => {
          setFlights(res.data?.items || []);
          setTotalCount(res.data?.totalCount || 0);
          setLoading(false);
        })
        .catch((err) => {
          setError(
            err.response?.data?.message ||
            err.message ||
            "Failed to connect to Flights API"
          );
          setLoading(false);
        });
    },
    [pageNumber, pageSize, searchTerm]
  );

  // Search input change delay (Debounce 400ms) to reduce API calls
  useEffect(() => {
    const handler = setTimeout(() => {
      fetchFlights(pageNumber, searchTerm);
    }, 400);

    return () => clearTimeout(handler);
  }, [pageNumber, searchTerm, fetchFlights]);

  const handlePageChange = (newPage) => {
    setPageNumber(newPage);
  };

  const handleSearchChange = (term) => {
    setSearchTerm(term);
    setPageNumber(1); // Reset to page 1 on new search
  };

  return (
    <DataTable
      title="Flights"
      subtitle="Overview of active, scheduled, and departed flight schedules"
      icon="✈️"
      data={flights}
      loading={loading}
      error={error}
      onRefresh={() => fetchFlights(pageNumber, searchTerm)}
      pageNumber={pageNumber}
      pageSize={pageSize}
      totalCount={totalCount}
      onPageChange={handlePageChange}
      searchTerm={searchTerm}
      onSearchChange={handleSearchChange}
    />
  );
}

export default Flight;