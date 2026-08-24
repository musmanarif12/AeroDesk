import React, { useState } from "react";
import "./App.css";

import Flight from "./Components/Flight";
import Aircraft from "./Components/Aircraft";
import Airline from "./Components/Airline";
import Airport from "./Components/Airport";
import Baggage from "./Components/Baggages";
import BoardingPasses from "./Components/Boarding_passes";
import Bookings from "./Components/Booking";
import CheckIns from "./Components/CheckIns";
import Gates from "./Components/Gates";
import Passengers from "./Components/Passengers";

const NAV_ITEMS = [
  { id: "flights", label: "Flights", icon: "✈️", component: Flight },
  { id: "aircrafts", label: "Aircrafts", icon: "🛩️", component: Aircraft },
  { id: "airlines", label: "Airlines", icon: "🏢", component: Airline },
  { id: "airports", label: "Airports", icon: "🛫", component: Airport },
  { id: "baggage", label: "Baggage", icon: "🧳", component: Baggage },
  { id: "boarding_passes", label: "Boarding Passes", icon: "🎫", component: BoardingPasses },
  { id: "bookings", label: "Bookings", icon: "📑", component: Bookings },
  { id: "checkins", label: "Check-Ins", icon: "🛎️", component: CheckIns },
  { id: "gates", label: "Gates", icon: "🚪", component: Gates },
  { id: "passengers", label: "Passengers", icon: "👥", component: Passengers },
];

function App() {
  const [activeTab, setActiveTab] = useState("flights");
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);

  const activeNavItem = NAV_ITEMS.find((item) => item.id === activeTab) || NAV_ITEMS[0];
  const ActiveComponent = activeNavItem.component;

  return (
    <div className="app-container">
      {/* Sidebar Navigation */}
      <aside className={`sidebar ${mobileMenuOpen ? "open" : ""}`}>
        <div>
          {/* Logo */}
          <div className="sidebar-header">
            <div className="logo-icon">✈️</div>
            <div className="logo-title-group">
              <span className="logo-title">AeroDesk</span>
              <span className="logo-version">Aviation ERP v2.0</span>
            </div>
          </div>

          {/* System Status Pill */}
          <div className="system-status-pill">
            <span className="status-dot-pulse"></span>
            <span>API Online</span>
          </div>

          {/* Navigation Menu */}
          <nav className="nav-menu">
            {NAV_ITEMS.map((item) => {
              const isActive = activeTab === item.id;
              return (
                <button
                  key={item.id}
                  className={`nav-item ${isActive ? "active" : ""}`}
                  onClick={() => {
                    setActiveTab(item.id);
                    setMobileMenuOpen(false);
                  }}
                >
                  <div className="nav-item-left">
                    <span className="nav-icon">{item.icon}</span>
                    <span>{item.label}</span>
                  </div>
                  <svg
                    className="nav-chevron"
                    viewBox="0 0 24 24"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="2.5"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                  >
                    <polyline points="9 18 15 12 9 6"></polyline>
                  </svg>
                </button>
              );
            })}
          </nav>
        </div>

        {/* User Profile Badge */}
        <div className="user-profile">
          <div className="user-info">
            <div className="user-avatar">AD</div>
            <div>
              <div className="user-name">Aero Desk Admin</div>
              <div className="user-role">Flight Controller</div>
            </div>
          </div>
        </div>
      </aside>

      {/* Main Content Area */}
      <main className="main-content">
        {/* Top Header Bar */}
        <header className="top-header">
          <div className="greeting-group">
            <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
              <button
                className="mobile-toggle"
                onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
                aria-label="Toggle navigation"
              >
                ☰
              </button>
              <h1 className="greeting-text">
                <span>{activeNavItem.icon}</span> {activeNavItem.label} Operations
              </h1>
            </div>
            <p className="greeting-sub">
              Manage airport and airline operations in real-time
            </p>
          </div>

          <div className="header-meta">
            <div className="meta-pill">
              <span>📅</span>
              <span>{new Date().toLocaleDateString(undefined, { weekday: "short", month: "short", day: "numeric", year: "numeric" })}</span>
            </div>
            <div className="meta-pill" style={{ color: "#00ac4f", borderColor: "#bbf7d0" }}>
              <span className="status-dot-pulse"></span>
              <span>Port: 7010</span>
            </div>
          </div>
        </header>

        {/* Quick Stats Overview Bar */}
        <section className="stats-grid">
          <div className="stat-card">
            <div className="stat-icon-wrapper purple">✈️</div>
            <div className="stat-details">
              <span className="stat-label">Active Module</span>
              <span className="stat-value" style={{ fontSize: "18px" }}>{activeNavItem.label}</span>
              <span className="stat-subtext">Click sidebar to switch</span>
            </div>
          </div>

          <div className="stat-card">
            <div className="stat-icon-wrapper blue">🗂️</div>
            <div className="stat-details">
              <span className="stat-label">Total Modules</span>
              <span className="stat-value">10</span>
              <span className="stat-subtext">Integrated database models</span>
            </div>
          </div>

          <div className="stat-card">
            <div className="stat-icon-wrapper green">⚡</div>
            <div className="stat-details">
              <span className="stat-label">System Gateway</span>
              <span className="stat-value" style={{ fontSize: "16px", color: "#00ac4f" }}>Connected</span>
              <span className="stat-subtext">localhost:7010</span>
            </div>
          </div>
        </section>

        {/* Active Component Render */}
        <section key={activeTab}>
          <ActiveComponent />
        </section>
      </main>
    </div>
  );
}

export default App;
