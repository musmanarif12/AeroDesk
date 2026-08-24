AeroDesk

A production-ready Enterprise Airport Operations Management System built with a React Frontend Dashboard and a .NET 9 Backend, implementing Clean Architecture, CQRS (Command Query Responsibility Segregation), MediatR, and Entity Framework Core.

This full-stack solution provides a centralized airport operations platform—enabling real-time management of flights, passenger bookings, check-in workflows, baggage tracking, automated email notifications, local document management, and an interactive operations dashboard. API testing and interactive documentation are configured using Scalar API Reference.

Tech Stack

* Frontend: React.js, modern CSS/UI design, REST API integration
* Backend Framework: ASP.NET Core Web API (.NET 9)
* Architecture: Clean Architecture + CQRS Pattern
* Design Patterns: Mediator (MediatR), Repository, Strategy Pattern (Storage Provider)
* Database & ORM: SQL Server, Entity Framework Core 9 (Code-First)
* Authentication & Authorization: JWT (JSON Web Tokens), Role-Based Access Control (RBAC)
* Data Processing & Validation: LINQ, AutoMapper, FluentValidation
* Background Tasks & Notifications: `IHostedService` / `BackgroundService`, SMTP (Mailtrap)
* Document Storage: Local Machine File Storage Provider (`IFileStorageService`)
* API Documentation & Testing: Scalar API Reference / OpenAPI

Key Features & Modules

1. Interactive React Operations Dashboard 

* Dynamic UI integrated directly with .NET 9 backend endpoints.
* Real-time operational data grid rendering active modules including Flights, Aircrafts, Airlines, Airports, Baggage, Boarding Passes, Bookings, Check-Ins, Gates, and Passengers.
* Real-time system gateway status, module counters, paginated tables, and instant refresh capabilities.

2. Authentication & User Management

* Secure JWT-based authentication flow (Login, Password Reset).
* Role-Based Access Control enforcing granular security policies across 5 distinct system roles.
* Full User Management CRUD (Activation, Deactivation, Role Assignment).

3. Flight & Airline Management

* Manage Airlines, Fleet Aircraft, and Gate assignments.
* Schedule and update real-time flight statuses (Scheduled, Boarding, Departed, Delayed, Cancelled).
* Optimized LINQ queries for flight occupancy and delay tracking.

4. Flight Departure Email Notification System

* Automated Background Service: `FlightNotificationBackgroundService` polls the database every 10 minutes.
* Proactive Passenger Alerts: Automatically scans for upcoming flight departures scheduled within the next 5 hours.
* Email Service Abstraction: Integrated `IEmailService` with `SmtpEmailService` utilizing Mailtrap for delivery.
* Idempotency: Implemented `NotificationSent` flag on the `Flight` entity with EF Core migrations to eliminate duplicate emails.

5. Booking, Check-In & Boarding Workflow

* Complete lifecycle management for passenger reservations.
* Check-in module with automated seat assignment and luggage registration.
* Automated Boarding Pass generation linked directly to check-in records.
* Digital boarding pass verification and boarding gate operations.

6. Baggage Tracking

* Register baggage against passenger records and bookings.
* Real-time baggage status updates across transition points.
* Visual tagging support via attached scan documents.

7. Local Document Storage & Management Module

* Modular file management system allowing uploaded documents to attach polymorphically to core domain entities (`Passenger`, `Booking`, `CheckIn`, `Airline`, `Aircraft`).
* Local Machine Provider: Files are stored directly on the host server's local file directory via an `IFileStorageService` implementation.
* Security & Validation: File extension whitelist (`.pdf`, `.jpg`, `.jpeg`, `.png`), size limit enforcement (up to 5 MB), and soft-delete capabilities preserving audit trails.

System Roles & Access Matrix

| Role | Core Responsibilities |

| Administrator | Full system access, manage users, airlines, airports, system-wide documents, and operational reports. |
| Airline Manager | Aircraft management, flight scheduling, gate assignments, status updates, and logo/document uploads. |
| Check-in Officer | Passenger verification, seat assignment, baggage registration, boarding pass generation, and passport document uploads. |
| Boarding Officer | Boarding pass validation, passenger boarding processing, and gate closure execution. |
| Passenger | Personal booking management, flight status lookup, boarding pass download, baggage tracking, and document uploads. |

Architecture Overview

```text
               ┌────────────────────────┐
               │   React Frontend UI    │
               │   (Client Dashboard)   │
               └───────────┬────────────┘
                           │ HTTP / REST
               ┌───────────▼────────────┐
               │   Presentation API     │
               │   (Scalar Interactive) │
               └───────────┬────────────┘
                           │
               ┌───────────▼────────────┐
               │   Application Layer    │
               │   (CQRS / MediatR)     │
               └───────────┬────────────┘
                           │
       ┌───────────────────┴───────────────────┐
       │                                       │
┌──────▼─────────┐                    ┌────────▼────────┐
│  Domain Layer  │                    │ Infrastructure  │
│  (Entities)    │                    │ (EF Core / SMTP │
│                │                    │ Local Storage)  │
└────────────────┘                    └─────────────────┘

```

* Frontend Layer: React SPA consuming API endpoints to present real-time table views, metrics, and operation controls.
* Domain Layer Core entities (`Flight`, `Passenger`, `Booking`, `Document`, etc.), enums, and domain logic.
* Application Layer: CQRS commands/queries, MediatR handlers, DTOs, FluentValidation rules, AutoMapper profiles, and service interfaces (`IEmailService`, `IFileStorageService`).
* Infrastructure Layer: EF Core `DbContext`, database configurations, migrations, background services, local disk storage service implementation, and SMTP email services.
* Presentation API Layer: Controllers, middlewares (Global Exception Handling, Custom Response Wrappers), Scalar API UI endpoint integration, and Dependency Injection registration.

Database Architecture

The system utilizes an enterprise relational schema consisting of 13 core entities:

* `User`
* `Role`
* `Airline`
* `Airport`
* `Aircraft`
* `Flight`
* `Passenger`
* `Booking`
* `CheckIn`
* `BoardingPass`
* `Baggage`
* `Gate`
* `Document` *(Polymorphic relationship via `EntityType` and `EntityId`)*

API Reference & Endpoints
Authentication

* `POST /api/auth/login`
* `POST /api/auth/change-password`

Flights

* `GET /api/flights`
* `POST /api/flights`
* `GET /api/flights/{id}`
* `PUT /api/flights/{id}`
* `PATCH /api/flights/{id}/status`

Check-In & Boarding

* `POST /api/checkin`
* `POST /api/checkin/generate-boarding-pass`
* `POST /api/boarding/scan`

File Upload & Document Management (Local Disk)

* `POST /api/files/upload?entityType={type}&entityId={id}`
* `GET /api/files/{id}/download`
* `GET /api/files?entityType={type}&entityId={id}`
* `PUT /api/files/{id}`
* `DELETE /api/files/{id}`


Configuration & Setup

Prerequisites

* .NET 9.0 SDK
*  Node.js (v18+ recommended)
*  SQL Server
*  Mailtrap (or standard SMTP server)

AppSettings Configuration (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AOMSDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_SUPER_SECRET_JWT_KEY_HERE",
    "Issuer": "AOMSApi",
    "Audience": "AOMSUsers",
    "DurationInMinutes": 60
  },
  "SmtpSettings": {
    "Host": "sandbox.smtp.mailtrap.io",
    "Port": 2525,
    "Username": "YOUR_MAILTRAP_USERNAME",
    "Password": "YOUR_MAILTRAP_PASSWORD",
    "SenderEmail": "no-reply@aoms.com",
    "SenderName": "Airport Operations System"
  },
  "FileStorage": {
    "LocalDirectoryPath": "C:\\AOMS_Uploads\\Documents",
    "MaxFileSizeInMB": 5,
    "AllowedExtensions": [ ".pdf", ".jpg", ".jpeg", ".png" ]
  }
}

```
Database Migration

```bash
dotnet ef database update --project Infrastructure --startup-project Presentation

```

Running the Application

1. Start Backend API:
```bash
dotnet run --project Presentation

```


Interactive Scalar API documentation will be available at: `https://localhost:{port}/scalar/v1`
2. Start React Frontend:
```bash
cd client
npm install
npm run dev

```
