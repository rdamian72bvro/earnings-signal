# AGENTS.md

## Project

Build an earnings-reaction signal generator web app.

Use `docs/product/earnings-reaction-signal-generator.md` as the product specification, but do not implement every feature at once.

## Tech stack

- Backend: .NET 8 Web API
- Frontend: Angular
- Database: PostgreSQL
- ORM: Entity Framework Core
- Background jobs: Hangfire or Quartz.NET
- Cache: Redis, optional after MVP
- Deployment: Docker Compose
- Broker integration: Alpaca Paper Trading later, not in MVP v1

## MVP priority

Implement only Phase 1 first:

1. S&P 500 company model
2. Earnings events model
3. Earnings estimates model
4. Earnings actuals model
5. Daily prices model
6. Signals model
7. PostgreSQL migrations
8. Basic API endpoints
9. Angular Weekly Scanner page
10. Seed/mock data support

Do not implement in MVP v1:

- real trading
- Alpaca integration
- LLM transcript analysis
- options
- paper trading
- complex ML
- authentication
- paid external data-provider integrations

## Backend conventions

- Use .NET 8.
- Use clean service classes.
- Keep controllers thin.
- Use DTOs for API requests and responses.
- Use async methods.
- Keep financial calculations in dedicated services.
- Do not hardcode API keys.
- Use configuration via environment variables.
- Keep domain entities separate from API DTOs.
- Prefer explicit names over clever abstractions.
- Use PostgreSQL-compatible EF Core migrations.

## Frontend conventions

- Use Angular.
- Keep pages simple and data-focused.
- Prefer readable tables and cards over complex visuals.
- Create a Weekly Scanner page first.
- Do not add heavy UI libraries unless requested.
- Use clear component names.
- Keep API calls in Angular services.

## First implementation target

Create the initial monorepo structure:

- `/backend` with a .NET 8 Web API solution
- `/frontend` with an Angular app
- `/docs/product` with the product specification
- `/docs/architecture` with database and API notes
- `/docker-compose.yml` with PostgreSQL

For the backend, add initial EF Core entities and DbContext for:

- Company
- EarningsEvent
- EarningsEstimate
- EarningsActual
- DailyPrice
- Signal

Add initial REST endpoints:

- `GET /api/companies`
- `GET /api/earnings/upcoming`
- `GET /api/signals/live`

Use mock/seed data only. Do not integrate external APIs yet.

For the frontend, create a basic Weekly Scanner page that calls the upcoming earnings endpoint and displays a table.

## Verification

Before finishing a task, run the relevant commands:

- backend build
- backend tests, if present
- frontend build
- frontend tests, if present

If a command cannot run, explain why and what is missing.

## Done means

A task is not done unless:

- code builds, or the failure is clearly explained;
- new behavior is reachable from the app or API;
- important assumptions are documented;
- no secrets or API keys are committed;
- changes stay within the requested phase.