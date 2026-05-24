# Database Architecture

## Goal

Store enough structured data to research and generate S&P 500 earnings-reaction signals without hindsight bias.

The first version should support:

- companies;
- earnings events;
- EPS and revenue estimates;
- actual reported EPS and revenue;
- daily price history;
- generated signals;
- later outcome tracking.

## Core entities

### Company

Represents a tradable company, initially focused on S&P 500 constituents.

Fields:

```text
Id
Ticker
Name
Sector
Industry
MarketCap
IsSp500
Active
CreatedAt
UpdatedAt
```

### EarningsEvent

Represents a scheduled or historical earnings report.

Fields:

```text
Id
CompanyId
Ticker
FiscalYear
FiscalQuarter
ReportDate
ReportTime -- BMO / AMC / INTRADAY / UNKNOWN
Source
Confirmed
CreatedAt
UpdatedAt
```

### EarningsEstimate

Stores consensus estimates as known before the report.

Fields:

```text
Id
EarningsEventId
EpsEstimate
RevenueEstimate
EbitdaEstimate
GrossMarginEstimate
OperatingMarginEstimate
Source
AsOfDate
CreatedAt
```

Important: estimates must have an `AsOfDate` to reduce look-ahead bias.

### EarningsActual

Stores reported earnings results.

Fields:

```text
Id
EarningsEventId
EpsActual
RevenueActual
GrossMarginActual
OperatingMarginActual
FreeCashFlow
Capex
Inventory
Receivables
DeferredRevenue
EpsSurprisePct
RevenueSurprisePct
Source
CreatedAt
```

### DailyPrice

Stores daily OHLCV data.

Fields:

```text
Id
Ticker
TradeDate
Open
High
Low
Close
AdjustedClose
Volume
Source
```

Unique key:

```text
Ticker + TradeDate
```

### Signal

Stores immutable generated signal records.

Fields:

```text
Id
EarningsEventId
Ticker
SignalType
Direction
Score
Confidence
GeneratedAt
ModelVersion
EntryRule
EntryPrice
StopPrice
TargetPrice
InvalidationLevel
HoldingPeriodDays
ReasonsJson
RisksJson
FeaturesJson
Status
Immutable
```

## Later entities

Add these after MVP v1:

```text
IntradayPrice
GuidanceEvent
Transcript
LlmTranscriptAnalysis
SignalOutcome
BacktestRun
PaperTrade
OptionsSnapshot
BorrowSnapshot
MarketRegime
```

## Data-quality rules

1. Store provider source for imported data.
2. Store timestamps for all time-sensitive data.
3. Do not overwrite signal features after signal generation.
4. Keep estimates as-of date separate from earnings actuals.
5. Do not use transcript data in a backtest unless transcript availability time is known.
6. Preserve raw provider payloads where practical.

## Hindsight-bias rules

Every historical or live signal should be generated using only data that would have been known at that time.

This matters for:

- consensus estimates;
- guidance;
- earnings release time;
- call transcript availability;
- intraday price confirmation;
- analyst revisions;
- options implied move.

## Initial EF Core implementation order

1. Company
2. EarningsEvent
3. EarningsEstimate
4. EarningsActual
5. DailyPrice
6. Signal

After those are stable, add:

1. SignalOutcome
2. IntradayPrice
3. GuidanceEvent
4. Transcript
5. LlmTranscriptAnalysis
6. BacktestRun
7. PaperTrade