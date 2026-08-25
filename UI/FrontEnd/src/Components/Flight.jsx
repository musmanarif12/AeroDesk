import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import DataTable from "./DataTable";

const API_BASE = "https://localhost:7010";

const STATUS_OPTIONS = [
  "Scheduled",
  "OnTime",
  "Delayed",
  "Boarding",
  "InFlight",
  "Landed",
  "Cancelled",
];

// ─── Flight Form Modal (Create / Edit) ───────────────────────────────────────
function FlightModal({ mode, flight, onClose, onSuccess }) {
  const isEdit = mode === "edit";

  const [form, setForm] = useState({
    flightNumber: "",
    departureTime: "",
    arrivalTime: "",
    status: "",
    departureAirportId: "",
    arrivalAirportId: "",
    gateId: "",
    airlineId: "",
    aircraftId: "",
  });

  const [saving, setSaving] = useState(false);
  const [formError, setFormError] = useState(null);

  // Pre-fill form when editing
  useEffect(() => {
    if (isEdit && flight) {
      const fmtDT = (dt) => (dt ? dt.substring(0, 16) : "");
      setForm({
        flightNumber: flight.flightNumber || "",
        departureTime: fmtDT(flight.departureTime),
        arrivalTime: fmtDT(flight.arrivalTime),
        status: flight.status || "",
        departureAirportId: flight.departureAirportId ?? "",
        arrivalAirportId: flight.arrivalAirportId ?? "",
        gateId: flight.gateId ?? "",
        airlineId: flight.airlineId ?? "",
        aircraftId: flight.aircraftId ?? "",
      });
    }
  }, [isEdit, flight]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSaving(true);
    setFormError(null);

    const payload = {
      ...(isEdit && { id: flight.id }),   // PUT request ke liye id zaruri hai
      flightNumber: form.flightNumber,
      departureTime: form.departureTime
        ? new Date(form.departureTime).toISOString()
        : null,
      arrivalTime: form.arrivalTime
        ? new Date(form.arrivalTime).toISOString()
        : null,
      status: form.status,
      departureAirportId: Number(form.departureAirportId) || 0,
      arrivalAirportId: Number(form.arrivalAirportId) || 0,
      gateId: Number(form.gateId) || 0,
      airlineId: Number(form.airlineId) || 0,
      aircraftId: Number(form.aircraftId) || 0,
    };

    try {
      if (isEdit) {
        await axios.put(`${API_BASE}/api/Flights/${flight.id}`, payload);
      } else {
        await axios.post(`${API_BASE}/api/Flights`, payload);
      }
      onSuccess();
      onClose();
    } catch (err) {
      setFormError(
        err.response?.data?.message ||
        err.response?.data?.title ||
        err.message ||
        "Operation failed. Please try again."
      );
    } finally {
      setSaving(false);
    }
  };

  return (
    <div
      className="modal-backdrop"
      onClick={(e) => e.target === e.currentTarget && onClose()}
    >
      <div className="modal-box">
        {/* Modal Header */}
        <div className="modal-header">
          <div className="modal-title-group">
            <span className="modal-icon">{isEdit ? "✏️" : "✈️"}</span>
            <div>
              <h2 className="modal-title">
                {isEdit ? "Edit Flight" : "Create New Flight"}
              </h2>
              <p className="modal-subtitle">
                {isEdit
                  ? `Updating flight ${flight?.flightNumber}`
                  : "Add a new flight to the system"}
              </p>
            </div>
          </div>
          <button className="modal-close-btn" onClick={onClose} type="button">
            ✕
          </button>
        </div>

        {/* Error Banner */}
        {formError && (
          <div className="modal-error-banner">
            <span>⚠️</span>
            <span>{formError}</span>
          </div>
        )}

        {/* Form */}
        <form onSubmit={handleSubmit} className="modal-form">
          <div className="form-grid-2">
            <div className="form-group">
              <label className="form-label">Flight Number *</label>
              <input
                className="form-input"
                name="flightNumber"
                value={form.flightNumber}
                onChange={handleChange}
                placeholder="e.g. PK-301"
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Status *</label>
              <select
                className="form-input form-select"
                name="status"
                value={form.status}
                onChange={handleChange}
                required
              >
                <option value="">— Select Status —</option>
                {STATUS_OPTIONS.map((s) => (
                  <option key={s} value={s}>
                    {s}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label className="form-label">Departure Time *</label>
              <input
                className="form-input"
                type="datetime-local"
                name="departureTime"
                value={form.departureTime}
                onChange={handleChange}
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Arrival Time *</label>
              <input
                className="form-input"
                type="datetime-local"
                name="arrivalTime"
                value={form.arrivalTime}
                onChange={handleChange}
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Departure Airport ID *</label>
              <input
                className="form-input"
                type="number"
                name="departureAirportId"
                value={form.departureAirportId}
                onChange={handleChange}
                placeholder="e.g. 1"
                min="1"
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Arrival Airport ID *</label>
              <input
                className="form-input"
                type="number"
                name="arrivalAirportId"
                value={form.arrivalAirportId}
                onChange={handleChange}
                placeholder="e.g. 2"
                min="1"
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Gate ID *</label>
              <input
                className="form-input"
                type="number"
                name="gateId"
                value={form.gateId}
                onChange={handleChange}
                placeholder="e.g. 1"
                min="1"
                required
              />
            </div>

            <div className="form-group">
              <label className="form-label">Airline ID *</label>
              <input
                className="form-input"
                type="number"
                name="airlineId"
                value={form.airlineId}
                onChange={handleChange}
                placeholder="e.g. 1"
                min="1"
                required
              />
            </div>

            <div className="form-group form-group-full">
              <label className="form-label">Aircraft ID *</label>
              <input
                className="form-input"
                type="number"
                name="aircraftId"
                value={form.aircraftId}
                onChange={handleChange}
                placeholder="e.g. 1"
                min="1"
                required
              />
            </div>
          </div>

          {/* Footer Actions */}
          <div className="modal-footer">
            <button
              type="button"
              className="btn-cancel"
              onClick={onClose}
              disabled={saving}
            >
              Cancel
            </button>
            <button type="submit" className="btn-save" disabled={saving}>
              {saving ? (
                <>
                  <span className="btn-spinner"></span>
                  {isEdit ? "Saving..." : "Creating..."}
                </>
              ) : isEdit ? (
                "💾 Save Changes"
              ) : (
                "✈️ Create Flight"
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

// ─── Delete Confirmation Modal ────────────────────────────────────────────────
function DeleteModal({ flight, onClose, onConfirm, deleting }) {
  return (
    <div
      className="modal-backdrop"
      onClick={(e) => e.target === e.currentTarget && onClose()}
    >
      <div className="modal-box modal-box-sm">
        <div className="modal-header">
          <div className="modal-title-group">
            <span className="modal-icon modal-icon-danger">🗑️</span>
            <div>
              <h2 className="modal-title">Delete Flight</h2>
              <p className="modal-subtitle">This action cannot be undone</p>
            </div>
          </div>
          <button className="modal-close-btn" onClick={onClose} type="button">
            ✕
          </button>
        </div>

        <div className="delete-confirm-body">
          <div className="delete-warning-icon">⚠️</div>
          <p className="delete-confirm-text">
            Are you sure you want to delete flight{" "}
            <strong className="delete-highlight">
              {flight?.flightNumber}
            </strong>
            ? All associated data will be permanently removed from the database.
          </p>
        </div>

        <div className="modal-footer">
          <button className="btn-cancel" onClick={onClose} disabled={deleting}>
            Keep Flight
          </button>
          <button
            className="btn-delete"
            onClick={onConfirm}
            disabled={deleting}
          >
            {deleting ? (
              <>
                <span className="btn-spinner btn-spinner-white"></span>
                Deleting...
              </>
            ) : (
              "🗑️ Yes, Delete"
            )}
          </button>
        </div>
      </div>
    </div>
  );
}

// ─── Main Flight Component ─────────────────────────────────────────────────────
function Flight() {
  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Server-side pagination & search states
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(8);
  const [totalCount, setTotalCount] = useState(0);
  const [searchTerm, setSearchTerm] = useState("");

  // Modal states
  const [modalMode, setModalMode] = useState(null); // "create" | "edit" | "delete"
  const [selectedFlight, setSelectedFlight] = useState(null);
  const [deleting, setDeleting] = useState(false);

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

  const handlePageChange = (newPage) => setPageNumber(newPage);

  const handleSearchChange = (term) => {
    setSearchTerm(term);
    setPageNumber(1);
  };

  // CRUD Handlers
  const handleCreate = () => {
    setSelectedFlight(null);
    setModalMode("create");
  };

  const handleEdit = (row) => {
    setSelectedFlight(row);
    setModalMode("edit");
  };

  const handleDeleteClick = (row) => {
    setSelectedFlight(row);
    setModalMode("delete");
  };

  const handleDeleteConfirm = async () => {
    if (!selectedFlight) return;
    setDeleting(true);
    try {
      await axios.delete(`${API_BASE}/api/Flights/${selectedFlight.id}`);
      setModalMode(null);
      setSelectedFlight(null);
      const newTotal = totalCount - 1;
      const maxPage = Math.max(1, Math.ceil(newTotal / pageSize));
      const targetPage = Math.min(pageNumber, maxPage);
      setPageNumber(targetPage);
      fetchFlights(targetPage, searchTerm);
    } catch (err) {
      alert(
        err.response?.data?.message ||
        err.message ||
        "Failed to delete flight."
      );
    } finally {
      setDeleting(false);
    }
  };

  const closeModal = () => {
    setModalMode(null);
    setSelectedFlight(null);
  };

  return (
    <>
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
        onAdd={handleCreate}
        onEdit={handleEdit}
        onDelete={handleDeleteClick}
      />

      {/* Create / Edit Modal */}
      {(modalMode === "create" || modalMode === "edit") && (
        <FlightModal
          mode={modalMode}
          flight={selectedFlight}
          onClose={closeModal}
          onSuccess={() => fetchFlights(pageNumber, searchTerm)}
        />
      )}

      {/* Delete Confirmation Modal */}
      {modalMode === "delete" && (
        <DeleteModal
          flight={selectedFlight}
          onClose={closeModal}
          onConfirm={handleDeleteConfirm}
          deleting={deleting}
        />
      )}
    </>
  );
}

export default Flight;