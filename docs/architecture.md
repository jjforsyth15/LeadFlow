# Architecture

LeadFlow is a modular ASP.NET Core Web API built as a single deployable application.

The project uses a layered structure to separate HTTP handling, business logic, and database access.

## Request Flow

```text
Client
  ↓
Controller
  ↓
Service
  ↓
Entity Framework Core
  ↓
PostgreSQL
```

## Controllers

Controllers define API routes, receive requests, call services, and return HTTP responses.

They should remain small and avoid containing business rules or complex database queries.

## Services

Services contain the application's business logic.

Examples include:

* Lead validation
* Lead scoring
* Lead assignment
* Lead status transitions
* Campaign analytics

Services may access `ApplicationDbContext` directly for database operations.

## Persistence

Entity Framework Core manages communication with PostgreSQL through `ApplicationDbContext`.

It is responsible for:

* Querying and saving data
* Managing entity relationships
* Tracking changes
* Applying database migrations

Repositories will only be introduced if database queries become complex or reused across multiple services.

## DTOs and Entities

Entities represent data stored in the database.

DTOs define the request and response models exposed by the API.

Database entities are not returned directly from controllers.

## Authentication and Authorization

ASP.NET Core Identity manages users and passwords.

JWT bearer tokens authenticate API requests, while roles restrict access to protected operations.

Initial roles include:

* Admin
* Agent
* Analyst

The API also checks resource ownership where necessary. For example, an Agent may only update leads assigned to them.

## Error Handling

Centralized exception-handling middleware provides consistent error responses and prevents internal exception details from being exposed.

## Initial Project Structure

```text
LeadFlow/
├── src/
│   └── LeadFlow.Api/
│       ├── Controllers/
│       ├── Data/
│       ├── DTOs/
│       ├── Entities/
│       ├── Middleware/
│       ├── Services/
│       └── Program.cs
├── tests/
│   └── LeadFlow.Tests/
├── docs/
│   └── architecture.md
├── docker-compose.yml
├── README.md
└── LeadFlow.sln
```

This structure keeps the application simple while separating the main responsibilities clearly enough for future development.
