# Project Scope and Requirements

## Project Purpose

LeadFlow is a customer lead processing platform designed to manage the lifecycle of leads generated through performance marketing campaigns.

Businesses running marketing campaigns may receive customer leads from multiple sources, such as comparison websites, landing pages, advertisements, partner integrations, or other external services. These leads need to be received, validated, prioritized, distributed to the appropriate users, and tracked as they move through the customer acquisition process.

LeadFlow provides a centralized backend system for managing this workflow.

At a high level, the platform is responsible for:

```text
Lead Source
    ↓
Lead Submission
    ↓
Validation
    ↓
Scoring / Qualification
    ↓
Assignment
    ↓
Agent Follow-Up
    ↓
Lifecycle Tracking
    ↓
Conversion / Loss
    ↓
Campaign Analytics
```

The initial version focuses on the backend services and REST API required to support this workflow.

---

## Core Domain

LeadFlow revolves around three primary concepts:

### Campaigns

A campaign represents a marketing initiative responsible for generating customer leads.

Examples could include:

* Home internet sign-up campaigns
* Auto insurance quote campaigns
* Home services campaigns
* Subscription campaigns
* E-commerce customer acquisition campaigns

Campaigns provide the context in which leads are generated and allow performance to be measured independently across different marketing efforts.

Each lead belongs to a campaign.

---

### Leads

A lead represents a potential customer who has expressed interest through one of the supported lead sources.

A lead contains the information necessary to identify, process, prioritize, and follow up with the potential customer.

Typical lead information may include:

* First name
* Last name
* Email address
* Phone number
* Location information
* Campaign
* Lead source
* Submission timestamp
* Current status
* Qualification score
* Assigned agent

The exact data collected may evolve as the application develops.

Leads are the primary business entity processed by LeadFlow.

---

### Lead Assignments

Qualified leads can be assigned to agents responsible for following up with the potential customer.

Assignments connect a lead with the agent currently responsible for it.

LeadFlow tracks assignment information so that the system can determine:

* Which agent owns a lead
* When the lead was assigned
* Whether a lead is currently unassigned
* How leads are distributed between agents

Assignment logic can initially remain simple and become more sophisticated as the platform evolves.

---

## System Actors

LeadFlow initially supports four types of actors.

### Administrator

Administrators manage the operational configuration of the platform.

Administrators can perform actions such as:

* Create and manage campaigns
* View all leads
* Manage application users
* View lead assignments
* Reassign leads when necessary
* Access platform-wide analytics
* Monitor system activity

Administrators have the highest level of application access.

---

### Agent

Agents are responsible for working with qualified leads.

Agents can:

* View leads assigned to them
* View relevant lead information
* Update the status of their assigned leads
* Record progress as leads move through the lifecycle
* Mark leads as converted or lost

Agents should not have unrestricted access to administrative functionality.

---

### Analyst

Analysts focus on campaign and lead performance.

Analysts can:

* View campaign performance
* View lead volume
* View conversion statistics
* Compare campaign results
* Analyze lead quality and outcomes

Analysts primarily have read-only access to operational data.

---

### External Lead Source

An external lead source represents a system that submits leads to LeadFlow.

Examples include:

* Marketing landing pages
* Comparison websites
* Partner services
* Simulated advertising platforms
* Internal test clients

External lead sources interact with LeadFlow through the lead submission API rather than through normal user workflows.

The initial implementation may simulate these integrations using API requests through Swagger or another HTTP client.

---

# Initial Lead Lifecycle

A lead progresses through a defined lifecycle.

The initial lifecycle is:

```text
Submitted
    ↓
Validated
    ↓
Qualified
    ↓
Assigned
    ↓
Contacted
   ↙     ↘
Converted  Lost
```

Not every lead successfully progresses through every stage.

---

## Submitted

A lead enters the `Submitted` state when LeadFlow receives a valid API request containing lead information.

At this point, the lead has entered the system but has not yet been fully processed.

---

## Validated

LeadFlow validates the submitted information before the lead can continue through the pipeline.

Validation may include checks such as:

* Required fields are present
* Email address has a valid format
* Phone number has an acceptable format
* Campaign exists and is active
* Submitted values fall within expected constraints

A lead that passes these checks can proceed to qualification.

---

## Qualified

A validated lead is evaluated to determine its quality or priority.

The initial implementation will use a deterministic scoring system based on predefined business rules.

The resulting score can be used to determine whether the lead qualifies for assignment and how highly it should be prioritized.

This scoring system is intentionally designed so that additional rules can be introduced later without redesigning the entire lead-processing pipeline.

---

## Assigned

A qualified lead can be assigned to an agent.

The assignment identifies which agent is currently responsible for following up with the lead.

The initial assignment strategy can use simple deterministic rules. More advanced distribution strategies can be introduced later.

---

## Contacted

An agent moves a lead into the `Contacted` state after attempting or successfully establishing contact with the potential customer.

This indicates that active follow-up has begun.

---

## Converted

A lead becomes `Converted` when the desired campaign outcome has been completed.

Depending on the campaign, a conversion could represent:

* A customer sign-up
* A completed purchase
* A service subscription
* A completed quote
* Another campaign-specific success event

`Converted` represents a terminal state in the initial lead lifecycle.

---

## Lost

A lead becomes `Lost` when it will no longer progress toward conversion.

Possible reasons may include:

* Customer is not interested
* Customer cannot be contacted
* Lead information is invalid
* Customer does not meet campaign requirements
* Lead expires
* Agent determines the opportunity should be closed

`Lost` represents a terminal state in the initial lifecycle.

---

# MVP Requirements

The Minimum Viable Product establishes the complete core workflow of LeadFlow without introducing unnecessary infrastructure or advanced features.

The MVP should demonstrate an end-to-end flow from campaign creation through lead conversion.

## Authentication

The system must support:

* User registration
* User login
* Secure password storage
* JWT authentication
* Authenticated API requests
* Role-based authorization

---

## User Management

The system must support application users with defined roles.

Initial roles are:

```text
Admin
Agent
Analyst
```

Administrators must be able to view and manage users.

Agents and analysts must only access functionality appropriate to their roles.

---

## Campaign Management

Administrators must be able to:

* Create campaigns
* View campaigns
* Update campaign information
* Activate or deactivate campaigns
* View leads associated with a campaign

Campaigns should contain enough information to identify and manage the marketing activity they represent.

---

## Lead Submission

LeadFlow must expose an endpoint capable of receiving new leads.

A lead submission must:

* Identify the associated campaign
* Include required customer information
* Record the source of the lead
* Record when the lead was submitted
* Create a persistent lead record

---

## Lead Validation

Submitted leads must be validated before progressing through the processing pipeline.

The system must reject or appropriately flag submissions that violate required validation rules.

Validation rules should be implemented independently from HTTP request handling so that the business rules remain reusable and testable.

---

## Lead Scoring

Validated leads must receive a qualification score.

The MVP scoring algorithm should be deterministic and rule-based.

The score should provide a simple representation of lead quality or priority.

The scoring logic must be isolated from controllers so that it can be tested independently and modified without changing the API layer.

---

## Lead Assignment

Qualified leads must be assignable to agents.

The system must track:

* Assigned agent
* Assigned lead
* Assignment timestamp

The system must also support identifying leads that have not yet been assigned.

---

## Lead Management

Authorized users must be able to:

* Retrieve leads
* Retrieve individual lead details
* Filter leads by relevant properties
* View lead status
* View assignment information

Agents should only be able to modify leads they are authorized to manage.

---

## Lead Status Management

Agents must be able to move assigned leads through the supported lifecycle.

Status transitions must follow defined business rules rather than allowing arbitrary status changes.

For example:

```text
Assigned → Contacted      ✓
Contacted → Converted     ✓
Contacted → Lost          ✓

Converted → Contacted     ✗
Lost → Assigned           ✗
```

Invalid lifecycle transitions should be rejected by the application.

---

## Lead History

Important lifecycle changes should be recorded.

At minimum, the system should track:

* Previous status
* New status
* Timestamp
* User responsible for the change

This creates an auditable history of how a lead progressed through the system.

---

## Campaign Analytics

The MVP should expose basic campaign performance metrics.

Initial analytics may include:

* Total leads
* Qualified leads
* Assigned leads
* Converted leads
* Lost leads
* Conversion rate
* Average lead score

Analytics should be calculated from persisted application data rather than maintained as manually updated counters.

---

## API Documentation

All major API functionality should be documented through OpenAPI.

The API documentation should clearly expose:

* Available endpoints
* HTTP methods
* Request models
* Response models
* Authentication requirements
* Expected status codes

Swagger UI should allow authenticated endpoints to be tested during development.

---

## Automated Testing

Core business logic should have automated test coverage.

Testing should prioritize:

* Lead validation
* Lead scoring
* Lead assignment
* Lifecycle transitions
* Authorization rules
* Important API workflows

The goal is not necessarily complete code coverage, but confidence in the application's most important business rules.

---

# MVP Success Criteria

The initial LeadFlow MVP is considered functionally complete when the following end-to-end workflow is possible:

```text
1. An administrator creates a campaign.

2. An agent account exists.

3. An external source submits a lead
   associated with the campaign.

4. LeadFlow validates the submission.

5. LeadFlow calculates the lead's
   qualification score.

6. The qualified lead is assigned
   to an agent.

7. The agent retrieves their
   assigned leads.

8. The agent marks the lead
   as contacted.

9. The agent marks the lead
   as converted or lost.

10. LeadFlow records the lead's
    status history.

11. Campaign analytics reflect
    the resulting lead outcome.
```

This workflow represents the core business process that the initial version of LeadFlow must support.

---

# Initial Scope Boundaries

The first version intentionally limits its scope.

The objective is to build a complete and maintainable lead-processing workflow before introducing infrastructure or features that are not required by the core domain.

## Included in the Initial Scope

The initial scope includes:

* ASP.NET Core REST API
* PostgreSQL persistence
* User authentication
* Role-based authorization
* Campaign management
* Lead submission
* Lead validation
* Rule-based lead scoring
* Lead assignment
* Lead lifecycle management
* Lead status history
* Basic campaign analytics
* Swagger/OpenAPI documentation
* Automated testing
* Docker-based local database environment

---

# Deferred Features

The following capabilities are intentionally outside the initial MVP.

They may be introduced in future phases when there is a concrete requirement for them.

## Dedicated Frontend

The initial application is backend-focused.

Swagger and direct HTTP requests provide the primary interface for interacting with the API.

A dedicated web interface can be introduced later without changing the underlying API architecture.

---

## Real External Marketing Integrations

The initial application will simulate external lead sources through HTTP requests.

Direct integrations with advertising platforms, comparison websites, CRM systems, or other third-party providers are deferred.

---

## Machine Learning Lead Scoring

Lead scoring initially uses deterministic business rules.

Machine learning models could eventually use historical conversion data to estimate lead quality, but this introduces additional data and infrastructure requirements that are unnecessary for the initial implementation.

---

## Advanced Assignment Algorithms

Initial assignment logic will remain intentionally simple.

Future strategies could consider:

* Agent availability
* Workload balancing
* Agent specialization
* Geographic region
* Campaign specialization
* Lead score
* Historical agent performance

The assignment architecture should allow these strategies to evolve without requiring major changes to the rest of the application.

---

## Asynchronous Lead Processing

The initial processing pipeline will operate within the application.

Message brokers and background processing systems such as RabbitMQ or other queue-based architectures are deferred until asynchronous processing provides a meaningful benefit.

---

## Caching

Redis or another caching layer is not required for the initial application.

Caching can be introduced if performance measurements identify queries or operations that would benefit from it.

---

## Microservices

LeadFlow will initially operate as a modular monolithic application.

Separating authentication, lead processing, analytics, or campaign management into independently deployed services would add operational complexity without providing a meaningful advantage at the initial scale.

---

## Real-Time Notifications

Real-time notifications, WebSockets, email notifications, and SMS integrations are outside the initial scope.

---

## Advanced Analytics

The MVP provides basic campaign metrics.

Advanced capabilities such as:

* Time-series reporting
* Attribution modeling
* Funnel visualization
* Agent performance dashboards
* Predictive analytics
* Custom report generation

are deferred until the underlying lead and campaign data model is established.

---

# Design Principles

Several principles guide the initial implementation of LeadFlow.

### Business Logic Belongs Outside Controllers

Controllers should coordinate HTTP requests and responses.

Lead validation, scoring, assignment, and lifecycle rules should live in application services or other appropriate domain components where they can be independently tested.

### Authorization Is Enforced by the Backend

Client applications should never be responsible for enforcing security rules.

LeadFlow's API determines whether a user is authorized to perform an operation.

### Database Integrity Matters

Relationships and constraints should be enforced at both the application and database levels where appropriate.

### Business Rules Should Be Explicit

Rules such as valid lifecycle transitions and qualification requirements should be represented directly in the application rather than being implied by controller behavior.

### Architecture Should Follow Requirements

Additional abstractions and infrastructure should be introduced when they solve an identifiable problem.

LeadFlow should favor understandable, maintainable code over architectural complexity for its own sake.

### The Core Workflow Comes First

New functionality should strengthen the primary workflow:

```text
Campaign
    ↓
Lead
    ↓
Validation
    ↓
Qualification
    ↓
Assignment
    ↓
Follow-Up
    ↓
Outcome
    ↓
Analytics
```

Features that do not meaningfully support this workflow can be deferred until the core platform is complete.
