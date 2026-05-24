# Earnings Signal Generator

MVP v1 skeleton for an S&P 500 earnings-reaction signal generator.

This repo currently implements only Phase 1 foundation:

- .NET 8 backend solution (`/backend/EarningsSignal.sln`)
- Angular frontend app (`/frontend/earnings-signal-ui`)
- PostgreSQL via Docker Compose
- Seed/mock data for companies, upcoming earnings, and live signals
- Basic API endpoints:
  - `GET /api/companies`
  - `GET /api/earnings/upcoming`
  - `GET /api/signals/live`
  - `GET /api/backtests`
  - `POST /api/backtests/run`
  - `GET /api/backtests/{id}`
  - `GET /api/backtests/{id}/trades`

Not included in MVP v1:

- real trading
- Alpaca integration
- external data-provider integrations
- LLM transcript analysis
- options trading
- authentication

## Project structure

```text
backend/
  EarningsSignal.sln
  EarningsSignal.Api/
  EarningsSignal.Application/
  EarningsSignal.Domain/
  EarningsSignal.Infrastructure/
  EarningsSignal.Tests/

frontend/
  earnings-signal-ui/
```

## Start PostgreSQL (Docker Compose)

From the repository root:

```bash
docker compose up -d postgres
```

Start all defined services:

```bash
docker compose up -d
```

## Run backend API

From repository root:

```bash
cd backend
dotnet restore EarningsSignal.sln
dotnet run --project EarningsSignal.Api/EarningsSignal.Api.csproj
```

On startup, the API applies pending EF Core migrations and seeds mock MVP data into PostgreSQL.

Manual migration commands:

```bash
cd backend
# run once if dotnet-ef is not installed:
# dotnet tool install --global dotnet-ef
dotnet ef migrations list --project EarningsSignal.Infrastructure/EarningsSignal.Infrastructure.csproj --startup-project EarningsSignal.Api/EarningsSignal.Api.csproj
dotnet ef database update --project EarningsSignal.Infrastructure/EarningsSignal.Infrastructure.csproj --startup-project EarningsSignal.Api/EarningsSignal.Api.csproj
```

NuGet sources are pinned in `NuGet.Config` at the repo root.

The API uses this default connection string (configurable):

- `ConnectionStrings:Postgres` in `backend/EarningsSignal.Api/appsettings.json`
- override via env var `EARNINGS_SIGNAL_CONNECTION_STRING`

Default local URL is usually:

- `http://localhost:5086`

## Run frontend

From repository root:

```bash
cd frontend/earnings-signal-ui
npm install
npm run start
```

Default Angular dev URL:

- `http://localhost:4200`

The dev server proxies API calls to the backend at `http://localhost:5086` using `frontend/earnings-signal-ui/proxy.conf.json`.

The Weekly Scanner page calls:

- `/api/earnings/upcoming`
- `/api/signals/live`

Backtest Lab calls:

- `/api/backtests`
- `/api/backtests/run`
- `/api/backtests/{id}/trades`

The Backtest Lab page is available at:

- `http://localhost:4200/backtests`

## Build commands

Backend build:

```bash
cd backend
dotnet build EarningsSignal.sln
```

Frontend build:

```bash
cd frontend/earnings-signal-ui
npm run build
```

## CI checks (GitHub Actions)

Workflow file:

- `.github/workflows/ci.yml`

Runs on push/pull request to `master` and `development`:

- Backend:
  - `dotnet restore ./backend/EarningsSignal.sln --configfile ./NuGet.Config`
  - `dotnet build ./backend/EarningsSignal.sln --configuration Release --no-restore`
  - `dotnet test ./backend/EarningsSignal.sln --configuration Release --no-build`
  - `dotnet list ./backend/EarningsSignal.sln package --vulnerable --include-transitive --format json`
- Frontend:
  - `npm ci`
  - `npm run build`
  - `npm audit --audit-level=low`

## Notes

- All current market data in MVP is mock/seed data.
- Domain entities are in `backend/EarningsSignal.Domain/Entities`.
- EF Core `DbContext` is in `backend/EarningsSignal.Infrastructure/Data/EarningsSignalDbContext.cs`.
