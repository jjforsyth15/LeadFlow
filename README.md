# LeadFlow

LeadFlow is a backend-focused customer lead processing platform built with C# and ASP.NET Core. It models a performance marketing workflow in which customer leads are received from different campaigns and sources, validated, processed, scored, assigned to agents, and tracked throughout their lifecycle.

The project is designed around a RESTful API and a relational data model, with an emphasis on clean backend architecture, maintainability, security, and clear separation of responsibilities.

## Core Functionality

LeadFlow is designed to support:

* User registration and authentication
* Role-based authorization for administrators, agents, and analysts
* Campaign creation and management
* Lead submission from multiple sources
* Lead validation and processing
* Lead scoring and prioritization
* Lead assignment to agents
* Lead lifecycle and status tracking
* Campaign and lead performance analytics
* Documented REST API endpoints

## Technology Stack

### C# and ASP.NET Core

C# is the primary programming language, with ASP.NET Core Web API providing the backend framework.

ASP.NET Core provides the foundation for routing, dependency injection, middleware, authentication, authorization, configuration, logging, and REST API development.

### PostgreSQL

PostgreSQL is used as the relational database.

LeadFlow contains strongly related data such as users, campaigns, leads, assignments, and status history. A relational database provides foreign keys, constraints, transactions, indexing, and structured querying to maintain data integrity across these relationships.

### Entity Framework Core

Entity Framework Core serves as the object-relational mapper between the C# application and PostgreSQL.

EF Core provides:

* Entity-to-table mapping
* LINQ-based database queries
* Relationship management
* Change tracking
* Database migrations
* Asynchronous database operations

### REST API

LeadFlow exposes its functionality through RESTful HTTP endpoints.

Resources are organized around concepts such as:

```text
/api/auth
/api/users
/api/campaigns
/api/leads
/api/assignments
/api/analytics
```

Standard HTTP methods and status codes are used to provide predictable API behavior.

### Data Transfer Objects

Data Transfer Objects (DTOs) define the request and response contracts exposed by the API.

DTOs separate the public API representation from internal database entities, allowing the application to control which fields clients can submit or receive while keeping persistence models independent from API contracts.

### ASP.NET Core Identity

ASP.NET Core Identity manages application users, password hashing, roles, and other authentication-related functionality.

Using Identity avoids implementing security-sensitive functionality such as password storage from scratch and integrates directly with Entity Framework Core.

### JWT Authentication

LeadFlow uses JSON Web Tokens for API authentication.

After successful authentication, clients receive a JWT that can be included with subsequent requests:

```http
Authorization: Bearer <token>
```

The API validates the token and uses its claims to determine the identity and permissions of the requesting user.

### Role-Based Authorization

Authorization rules restrict functionality based on user roles.

Initial application roles include:

* **Admin** — manages users, campaigns, leads, and system-level operations.
* **Agent** — works with leads assigned to them and manages their progress.
* **Analyst** — accesses campaign and lead performance data.

Authorization is enforced by the API rather than relying on client-side restrictions.

### Validation

ASP.NET Core model validation and Data Annotations provide initial request validation.

Validation includes requirements such as:

* Required fields
* String length restrictions
* Valid email formats
* Numeric ranges
* Valid request values

More complex business validation is handled within the application layer rather than relying exclusively on request-model validation.

### Application Architecture

LeadFlow follows a layered backend architecture designed to keep HTTP handling, business logic, and persistence concerns separate.

The primary request flow is:

```text
HTTP Request
      ↓
Controller
      ↓
Service
      ↓
Entity Framework Core
      ↓
PostgreSQL
```

**Controllers** are responsible for handling HTTP requests and responses.

**Services** contain application and business logic, including lead processing, scoring, assignment, and lifecycle management.

**Entity Framework Core** handles persistence and database queries.

Repository abstractions may be introduced where data-access logic becomes sufficiently complex or reusable to justify separating it from application services.

This approach avoids unnecessary abstraction while keeping the architecture extensible as the application grows.

### Swagger / OpenAPI

OpenAPI documentation is provided for the REST API.

Swagger UI provides an interactive interface for exploring and testing endpoints, including authenticated API operations.

### Testing

Automated testing is built around the .NET testing ecosystem.

**xUnit** is used as the primary testing framework.

Unit tests focus on business rules such as:

* Lead validation
* Lead scoring
* Lead assignment
* Lead lifecycle transitions
* Authorization behavior

ASP.NET Core integration testing can be used to verify the complete request pipeline, including routing, validation, authentication, middleware, and HTTP responses.

### Docker

Docker provides reproducible local infrastructure for the application.

PostgreSQL runs through Docker Compose during development, allowing the database environment to be created consistently without requiring a manually configured local PostgreSQL installation.

The ASP.NET Core API initially runs directly through the .NET CLI to preserve a simple debugging workflow. The API can also be containerized as the deployment configuration evolves.

### Git and GitHub

Git provides version control for the project, with GitHub hosting the source repository.

Development follows a lightweight branching structure:

```text
main
develop
feature/<feature-name>
```

Feature branches isolate individual changes before they are integrated into the primary development branch.

### Configuration and Secrets

ASP.NET Core's configuration system manages environment-specific application settings.

Configuration includes values such as:

* Database connection strings
* JWT configuration
* Allowed origins
* Environment-specific application settings

Sensitive configuration is excluded from source control and supplied through environment variables or .NET User Secrets during local development.

### Logging and Error Handling

ASP.NET Core's logging abstractions provide application logging throughout the API.

Centralized exception handling provides consistent error responses and prevents internal exception details from being exposed to API clients.

Sensitive information such as passwords, authentication tokens, and unnecessary personal data is excluded from application logs.

## Architecture Philosophy

LeadFlow is structured as a modular ASP.NET Core application rather than a collection of independent microservices.

The architecture prioritizes:

* Clear separation of responsibilities
* Thin controllers
* Focused business services
* Strong relational data integrity
* Secure authentication and authorization
* Testable business logic
* Consistent REST API design
* Reproducible development environments
* Maintainability without unnecessary abstraction

Additional infrastructure and architectural patterns are introduced when they solve a concrete application requirement rather than being included solely for architectural complexity.
