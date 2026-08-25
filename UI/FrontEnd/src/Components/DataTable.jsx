import React, { useState, useMemo } from "react";

// Format header keys into human-readable titles
function formatHeader(key) {
  if (!key) return "";
  return key
    .replace(/([A-Z])/g, " $1")
    .replace(/[_-]/g, " ")
    .trim()
    .replace(/\b\w/g, (c) => c.toUpperCase());
}

// Format cell values with smart badge colors
function renderCellValue(key, val) {
  if (val === null || val === undefined || val === "") {
    return <span className="cell-subtle">—</span>;
  }

  const strVal = String(val);
  const lowerVal = strVal.toLowerCase();

  // Status badges
  if (
    lowerVal === "active" ||
    lowerVal === "ontime" ||
    lowerVal === "on time" ||
    lowerVal === "landed" ||
    lowerVal === "confirmed" ||
    lowerVal === "available" ||
    lowerVal === "completed"
  ) {
    return <span className="status-badge status-active">{strVal}</span>;
  }

  if (
    lowerVal === "delayed" ||
    lowerVal === "cancelled" ||
    lowerVal === "inactive" ||
    lowerVal === "failed"
  ) {
    return <span className="status-badge status-delayed">{strVal}</span>;
  }

  if (
    lowerVal === "scheduled" ||
    lowerVal === "boarding" ||
    lowerVal === "in-flight" ||
    lowerVal === "inflight" ||
    lowerVal === "pending"
  ) {
    return <span className="status-badge status-scheduled">{strVal}</span>;
  }

  if (
    lowerVal === "maintenance" ||
    lowerVal === "standby" ||
    lowerVal === "reserved" ||
    lowerVal === "checked in" ||
    lowerVal === "checkedin"
  ) {
    return <span className="status-badge status-maintenance">{strVal}</span>;
  }

  // Boolean values
  if (typeof val === "boolean") {
    return val ? (
      <span className="status-badge status-active">Yes</span>
    ) : (
      <span className="status-badge status-inactive">No</span>
    );
  }

  // Key with ID or Code
  if (key.toLowerCase().includes("id") || key.toLowerCase().includes("code")) {
    return <span className="cell-bold font-mono">{strVal}</span>;
  }

  return <span>{strVal}</span>;
}

export default function DataTable({
  title,
  subtitle,
  icon,
  data = [],
  loading = false,
  error = null,
  onRefresh,
  // Server-side Pagination & Search Props
  pageNumber = 1,
  pageSize = 8,
  totalCount = 0,
  onPageChange,
  searchTerm = "",
  onSearchChange,
}) {
  const [sortColumn, setSortColumn] = useState(null);
  const [sortDirection, setSortDirection] = useState("asc");

  // Extract columns from backend data
  const columns = useMemo(() => {
    if (!Array.isArray(data) || data.length === 0) return [];
    return Object.keys(data[0]);
  }, [data]);

  // Handle column sorting (Sorting only loaded page rows visually)
  const handleSort = (col) => {
    if (sortColumn === col) {
      setSortDirection(sortDirection === "asc" ? "desc" : "asc");
    } else {
      setSortColumn(col);
      setSortDirection("asc");
    }
  };

  // Sort current page items
  const sortedData = useMemo(() => {
    if (!Array.isArray(data)) return [];
    let items = [...data];

    if (sortColumn) {
      items.sort((a, b) => {
        const valA = a[sortColumn] ?? "";
        const valB = b[sortColumn] ?? "";
        if (!isNaN(Number(valA)) && !isNaN(Number(valB))) {
          return sortDirection === "asc"
            ? Number(valA) - Number(valB)
            : Number(valB) - Number(valA);
        }
        return sortDirection === "asc"
          ? String(valA).localeCompare(String(valB))
          : String(valB).localeCompare(String(valA));
      });
    }

    return items;
  }, [data, sortColumn, sortDirection]);

  // Server-side pagination calculation
  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize));
  const startEntry = totalCount === 0 ? 0 : (pageNumber - 1) * pageSize + 1;
  const endEntry = Math.min(pageNumber * pageSize, totalCount);

  return (
    <div className="card-container">
      {/* Card Header Row */}
      <div className="card-header-row">
        <div className="card-title-group">
          <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
            {icon && <span style={{ fontSize: "22px" }}>{icon}</span>}
            <h2 className="card-title">{title}</h2>
            <span className="record-count-badge">
              {totalCount} {totalCount === 1 ? "Record" : "Records"}
            </span>
          </div>
          <p className="card-subtitle">
            {subtitle || `Live data retrieved from AeroDesk Central Database`}
          </p>
        </div>

        <div className="card-actions-group">
          {/* Server-Side Search Box */}
          <div className="table-search-box">
            <svg
              width="16"
              height="16"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
              style={{ opacity: 0.5 }}
            >
              <circle cx="11" cy="11" r="8"></circle>
              <line x1="21" y1="21" x2="16.65" y2="16.65"></line>
            </svg>
            <input
              type="text"
              placeholder={`Search ${title.toLowerCase()}...`}
              value={searchTerm}
              onChange={(e) => onSearchChange && onSearchChange(e.target.value)}
            />
            {searchTerm && (
              <button
                onClick={() => onSearchChange && onSearchChange("")}
                style={{
                  border: "none",
                  background: "transparent",
                  cursor: "pointer",
                  color: "#9197b3",
                  fontSize: "12px",
                }}
              >
                ✕
              </button>
            )}
          </div>

          {/* Refresh Button */}
          {onRefresh && (
            <button
              className="refresh-btn"
              onClick={onRefresh}
              title="Refresh data"
            >
              <svg
                width="14"
                height="14"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
              >
                <path d="M21.5 2v6h-6M21.34 15.57a10 10 0 1 1-.57-8.38l5.67-5.67" />
              </svg>
              Refresh
            </button>
          )}
        </div>
      </div>

      {/* Loading State */}
      {loading && (
        <div className="state-container">
          <div className="spinner"></div>
          <p style={{ fontWeight: 600, color: "var(--text-muted)" }}>
            Fetching {title} data from server...
          </p>
        </div>
      )}

      {/* Error State */}
      {!loading && error && (
        <div
          className="notice-banner"
          style={{
            background: "#fff5f5",
            borderColor: "#feb2b2",
            color: "#c53030",
          }}
        >
          <div style={{ display: "flex", alignItems: "center", gap: "8px" }}>
            <span style={{ fontSize: "18px" }}>⚠️</span>
            <span>
              <strong>Failed to fetch data:</strong> {String(error)}
            </span>
          </div>
          {onRefresh && (
            <button onClick={onRefresh} style={{ background: "#e53e3e" }}>
              Try Again
            </button>
          )}
        </div>
      )}

      {/* Empty State */}
      {!loading && !error && sortedData.length === 0 && (
        <div className="state-container">
          <div style={{ fontSize: "40px", opacity: 0.7 }}>📭</div>
          <h3
            style={{
              color: "var(--text-dark)",
              fontSize: "16px",
              marginTop: "6px",
            }}
          >
            {searchTerm
              ? `No results matching "${searchTerm}"`
              : `No ${title} found.`}
          </h3>
          <p style={{ color: "var(--text-muted)", fontSize: "13px" }}>
            {searchTerm
              ? "Try checking for typos or searching a different term."
              : "No records are currently available in the database."}
          </p>
        </div>
      )}

      {/* Data Table */}
      {!loading && !error && sortedData.length > 0 && (
        <>
          <div className="table-responsive">
            <table className="modern-table">
              <thead>
                <tr>
                  {columns.map((col) => (
                    <th
                      key={col}
                      onClick={() => handleSort(col)}
                      style={{ cursor: "pointer", userSelect: "none" }}
                      title="Click to sort"
                    >
                      <div
                        style={{
                          display: "flex",
                          alignItems: "center",
                          gap: "6px",
                        }}
                      >
                        <span>{formatHeader(col)}</span>
                        {sortColumn === col && (
                          <span
                            style={{
                              fontSize: "11px",
                              color: "var(--primary)",
                            }}
                          >
                            {sortDirection === "asc" ? "▲" : "▼"}
                          </span>
                        )}
                      </div>
                    </th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {sortedData.map((row, idx) => (
                  <tr key={row.id || row.Id || idx}>
                    {columns.map((col) => (
                      <td key={col}>{renderCellValue(col, row[col])}</td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* Table Footer */}
          <div className="table-footer">
            <div className="footer-info">
              Showing{" "}
              <strong>
                {startEntry} – {endEntry}
              </strong>{" "}
              of <strong>{totalCount}</strong> entries
            </div>

            <div className="pagination-controls">
              <button
                className="page-btn"
                disabled={pageNumber === 1}
                onClick={() => onPageChange && onPageChange(pageNumber - 1)}
              >
                ‹
              </button>

              {Array.from({ length: totalPages }, (_, i) => i + 1).map(
                (pageNum) => {
                  if (
                    pageNum === 1 ||
                    pageNum === totalPages ||
                    Math.abs(pageNum - pageNumber) <= 1
                  ) {
                    return (
                      <button
                        key={pageNum}
                        className={`page-btn ${pageNumber === pageNum ? "active" : ""
                          }`}
                        onClick={() => onPageChange && onPageChange(pageNum)}
                      >
                        {pageNum}
                      </button>
                    );
                  } else if (
                    pageNum === pageNumber - 2 ||
                    pageNum === pageNumber + 2
                  ) {
                    return (
                      <span key={pageNum} className="page-dots">
                        ...
                      </span>
                    );
                  }
                  return null;
                }
              )}

              <button
                className="page-btn"
                disabled={pageNumber === totalPages}
                onClick={() => onPageChange && onPageChange(pageNumber + 1)}
              >
                ›
              </button>
            </div>
          </div>
        </>
      )}
    </div>
  );
}