# API Architecture

## Goal

Expose a simple API for the Angular frontend and future background jobs.

MVP v1 should focus on read-heavy endpoints and mock/seed data.

## Base path

```text
/api
```

## MVP endpoints

### Get companies

```http
GET /api/companies
```

Response example:

```json
[
  {
    "ticker": "MSFT",
    "name": "Microsoft Corporation",
    "sector": "Technology",
    "marketCap": 3000000000000,
    "isSp500": true
  }
]
```

### Get upcoming earnings

```http
GET /api/earnings/upcoming
```

Optional query parameters:

```text
from
to
sector
signalType
minExpectationPressure
```

Response example:

```json
[
  {
    "ticker": "MSFT",
    "companyName": "Microsoft Corporation",
    "reportDate": "2026-07-28",
    "reportTime": "AMC",
    "sector": "Technology",
    "forwardPe": 34.2,
    "return20d": 8.4,
    "return60d": 17.1,
    "distanceFrom52WeekHigh": -2.3,
    "expectationPressureScore": 78,
    "preSignal": "Short Watch"
  }
]
```

### Get live signals

```http
GET /api/signals/live
```

Response example:

```json
[
  {
    "ticker": "XYZ",
    "signalType": "Strong Short",
    "direction": "Short",
    "score": 84,
    "confidence": 0.82,
    "setupType": "Low-quality beat rejected by market",
    "generatedAt": "2026-05-24T18:45:00Z",
    "modelVersion": "earnings-reaction-v0.1",
    "reasons": [
      "Expectation pressure is high",
      "Guidance was not raised",
      "Stock failed post-earnings VWAP"
    ],
    "risks": [
      "Company remains fundamentally high quality",
      "Market regime is risk-on"
    ]
  }
]
```

## Later endpoints

### Earnings

```http
GET /api/earnings/recent
GET /api/earnings/{id}
GET /api/earnings/{id}/reaction
GET /api/earnings/{id}/transcript-analysis
```

### Companies

```http
GET /api/companies/{ticker}
GET /api/companies/{ticker}/earnings-history
GET /api/companies/{ticker}/fundamentals
GET /api/companies/{ticker}/price
```

### Signals

```http
GET /api/signals/history
GET /api/signals/{id}
GET /api/signals/{id}/outcome
POST /api/signals/recalculate
```

### Backtests

```http
GET /api/backtests
POST /api/backtests/run
GET /api/backtests/{id}
GET /api/backtests/{id}/trades
```

### Forward tests

```http
GET /api/forward-tests
GET /api/forward-tests/summary
GET /api/forward-tests/by-model-version
```

### Paper trading

```http
GET /api/paper-trading/account
GET /api/paper-trading/positions
GET /api/paper-trading/trades
POST /api/paper-trading/enable
POST /api/paper-trading/disable
POST /api/paper-trading/close/{tradeId}
```

## API design rules

1. Controllers should be thin.
2. Use DTOs rather than returning EF entities directly.
3. Use async methods.
4. Keep calculations in services.
5. Return clear error responses.
6. Include model version and data timestamp on signal responses.
7. Do not expose API keys or provider secrets.
8. Use pagination for historical lists.

## First controller targets

Implement these first:

```text
CompaniesController
EarningsController
SignalsController
```

## First services

Implement these first:

```text
CompanyService
EarningsCalendarService
SignalReadService
SeedDataService
```

Do not implement external-provider services until the basic API and UI work with seed data.