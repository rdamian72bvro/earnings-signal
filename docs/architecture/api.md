# API Architecture

## Goal

Expose a minimal REST API for the MVP v1 Angular Weekly Scanner page using seeded data.

Base path:

```text
/api
```

## Implemented MVP endpoints

### GET /api/companies

Returns seeded companies.

Response shape:

```json
[
  {
    "id": "11111111-1111-1111-1111-111111111111",
    "ticker": "AAPL",
    "name": "Apple Inc.",
    "sector": "Technology",
    "industry": "Consumer Electronics"
  }
]
```

### GET /api/earnings/upcoming

Returns upcoming earnings rows for Weekly Scanner.

Response shape:

```json
[
  {
    "ticker": "AAPL",
    "companyName": "Apple Inc.",
    "reportDate": "2026-05-26",
    "reportTime": "After Close",
    "sector": "Technology",
    "expectationPressureScore": 78.0,
    "preSignal": "Short Watch"
  }
]
```

### GET /api/signals/live

Returns current live mock signals.

Response shape:

```json
[
  {
    "ticker": "NVDA",
    "companyName": "NVIDIA Corporation",
    "sector": "Technology",
    "signalType": "Strong Short",
    "score": 87.3,
    "reasonSummary": "Priced-for-perfection setup with bearish confirmation.",
    "generatedAtUtc": "2026-05-24T18:50:00Z"
  }
]
```

## API rules

1. Controllers remain thin.
2. API returns DTOs, not EF entities directly.
3. Async methods for data access.
4. MVP uses seed/mock data only.

## Phase 2 endpoints (Backtest Lab v1)

### POST /api/backtests/run

Runs a basic historical backtest using seeded historical earnings and price data.

### GET /api/backtests

Returns saved backtest run summaries.

### GET /api/backtests/{id}

Returns one backtest run summary by id.

### GET /api/backtests/{id}/trades

Returns generated trades for a run.
