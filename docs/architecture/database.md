# Database Architecture

## Goal

Provide the MVP v1 relational model for seeded earnings-signal workflows.

Database: PostgreSQL  
ORM: Entity Framework Core

## Implemented MVP entities

### Company

```text
Id
Ticker
Name
Sector
Industry
```

### EarningsEvent

```text
Id
CompanyId
ReportDate
ReportTime
ExpectationPressureScore
PreSignal
```

### EarningsEstimate

```text
Id
EarningsEventId
EpsEstimate
RevenueEstimate
AsOfUtc
```

### EarningsActual

```text
Id
EarningsEventId
EpsActual
RevenueActual
ReportedAtUtc
```

### DailyPrice

```text
Id
CompanyId
TradeDate
Open
High
Low
Close
Volume
```

Unique key:

```text
CompanyId + TradeDate
```

### Signal

```text
Id
CompanyId
EarningsEventId (nullable)
SignalType
Score
ReasonSummary
GeneratedAtUtc
IsLive
```

## Notes

1. MVP seeding includes 5 companies, 5 upcoming earnings events, and 3 live signals.
2. External provider data is not used in MVP v1.
3. Future entities (backtesting, paper trading, transcript analysis) are intentionally deferred beyond MVP v1.
