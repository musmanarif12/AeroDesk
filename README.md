Airport Operations Management System (AOMS)

A production-ready Enterprise Airport Operations Management System built with .NET 9, implementing Clean Architecture,CQRS (Command Query Responsibility Segments),MediatR, and Entity Framework Core.

This solution provides a centralized backend infrastructure for handling complex airport operations—including flight scheduling, passenger bookings, check-in workflows, baggage tracking, automated email notifications, and an extensible local document management system. API testing and interactive documentation are configured using **Scalar API Reference**.

Tech Stack

* Framework: ASP.NET Core Web API (.NET 9)
* Architecture: Clean Architecture + CQRS Pattern
* Design Patterns: Mediator (MediatR), Repository, Strategy Pattern (Storage Provider)
* Database & ORM: SQL Server, Entity Framework Core 9 (Code-First)
* Authentication & Authorization: JWT (JSON Web Tokens), Role-Based Access Control (RBAC)
* Data Processing & Validation: LINQ, AutoMapper, FluentValidation
* Background Tasks & Notifications: `IHostedService` / `BackgroundService`, SMTP (Mailtrap)
* Document Storage: Local Machine File Storage Provider (`IFileStorageService`)
* API Documentation & Testing: Scalar API Reference / OpenAPI

Key Features & Modules

1. Authentication & User Management

* Secure JWT-based authentication flow (Login, Password Reset).
* Role-Based Access Control enforcing granular security policies across 5 distinct system roles.
* Full User Management CRUD (Activation, Deactivation, Role Assignment).

2. Flight & Airline Management

* Manage Airlines, Fleet Aircraft, and Gate assignments.
* Schedule and update real-time flight statuses (Scheduled, Boarding, Departed, Delayed, Cancelled).
* Optimized LINQ queries for flight occupancy and delay tracking.

3. Flight Departure Email Notification System (NEW)

* Automated Background Service: `FlightNotificationBackgroundService` polls the database every 10 minutes.
* Proactive Passenger Alerts: Automatically scans for upcoming flight departures scheduled within the next 5 hours.
* Email Service Abstraction: Integrated `IEmailService` with `SmtpEmailService` utilizing Mailtrap for delivery.
* Idempotency: Implemented `NotificationSent` flag on the `Flight` entity with EF Core migrations to eliminate duplicate emails.

4. Booking, Check-In & Boarding Workflow

* Complete lifecycle management for passenger reservations.
* Check-in module with automated seat assignment and luggage registration.
* Automated Boarding Pass generation linked directly to check-in records.
* Digital boarding pass verification and boarding gate operations.

5. Baggage Tracking

* Register baggage against passenger records and bookings.
* Real-time baggage status updates across transition points.
* Visual tagging support via attached scan documents.

6. Local Document Storage & Management Module

* Modular file management system allowing uploaded documents to attach polymorphically to core domain entities (`Passenger`, `Booking`, `CheckIn`, `Airline`, `Aircraft`).
* Local Machine Provider: Files are stored directly on the host server's local file directory via an `IFileStorageService` implementation.
* Security & Validation: File extension whitelist (`.pdf`, `.jpg`, `.jpeg`, `.png`), size limit enforcement (up to 5 MB), and soft-delete capabilities preserving audit trails.

7. Analytical Reporting & Dashboard

* Real-time metrics for daily operations (Active Flights, Flights Today, Revenue Summary).
* Advanced LINQ reports for tracking delayed flights, capacity utilization, passenger history, missing passenger documents, and total storage usage by user.

System Roles & Access Matrix

| Role | Core Responsibilities |

| Administrator | Full system access, manage users, airlines, airports, system-wide documents, and operational reports. |
| Airline Manager | Aircraft management, flight scheduling, gate assignments, status updates, and logo/document uploads. |
| Check-in Officer | Passenger verification, seat assignment, baggage registration, boarding pass generation, and passport document uploads. |
| Boarding Officer | Boarding pass validation, passenger boarding processing, and gate closure execution. |
| Passenger | Personal booking management, flight status lookup, boarding pass download, baggage tracking, and document uploads. |

Architecture Overview

The solution adheres to Clean Architecture principles, ensuring strict separation of concerns, maintainability, and testability:

               ┌────────────────────────┐
               │    Presentation API    │
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


* Domain Layer: Core entities (`Flight`, `Passenger`, `Booking`, `Document`, etc.), enums, and domain logic.
  Application Layer: CQRS commands/queries, MediatR handlers, DTOs, FluentValidation rules, AutoMapper profiles, and service interfaces (`IEmailService`, `IFileStorageService`).
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
* `Document` (Polymorphic relationship via `EntityType` and `EntityId`)

API Reference

Authentication

POST /api/auth/login
POST /api/auth/change-password


Flights

GET    /api/flights
POST   /api/flights
GET    /api/flights/{id}
PUT    /api/flights/{id}
PATCH  /api/flights/{id}/status


Check-In & Boarding

POST /api/checkin
POST /api/checkin/generate-boarding-pass
POST /api/boarding/scan

File Upload & Document Management (Local Disk)

POST   /api/files/upload?entityType={type}&entityId={id}
GET    /api/files/{id}/download
GET    /api/files?entityType={type}&entityId={id}
PUT    /api/files/{id}
DELETE /api/files/{id}

Configuration & Setup

Prerequisites

* .NET 9.0 SDK
* SQL Server
* Mailtrap (or standard SMTP server)

AppSettings Configuration

Update your `appsettings.json` file with your database connection string, JWT parameters, SMTP credentials, and local storage directory configuration:

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

Run the following commands to apply database migrations and initialize schema objects:

```bash
dotnet ef database update --project Infrastructure --startup-project Presentation

```
Running the Application & Testing with Scalar

Run the Web API project:

```bash
dotnet run --project Presentation

```

Once running, access the interactive Scalar API Reference interface for testing endpoints at:
`https://localhost:{port}/scalar/v1`
