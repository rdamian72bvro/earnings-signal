# Earnings-Reaction Signal Generator Web App

## 1. Research Thesis and Detailed Findings

### Core thesis

The signal generator should not be designed as a blind “short stocks after earnings” app. The stronger and more defensible thesis is:

> In a high-valuation market, earnings reactions become asymmetric: positive surprises are often rewarded only modestly, while negative surprises and low-quality beats can be punished heavily. The best opportunity is a long/short post-earnings reaction engine that compares expectations, reported results, guidance, call tone, valuation, and confirmed price action.

The app should therefore identify:

1. Expensive or crowded companies that are priced for perfection before earnings.
2. Companies that report a headline beat but reveal weaker quality underneath.
3. Companies that miss expectations and are rejected by the market.
4. Companies with clean beat-and-raise reports where price action confirms upside.
5. Companies where the correct answer is no trade.

The tradeable edge is not “EPS beat” or “EPS miss” by itself. The tradeable edge is:

> Expectations versus reality versus market verdict.

---

### Summary of observed market behavior

Across the recent S&P 500 earnings seasons, the broad pattern was:

| Observation | Interpretation |
|---|---|
| Most S&P 500 companies beat EPS estimates | EPS beats are common and weak as standalone long signals |
| Positive surprises were often rewarded modestly | Good results were already expected in many cases |
| Negative surprises were punished harshly in multiple quarters | Downside reactions have been asymmetric |
| Some high-quality companies fell after decent or strong reports | Expectations, valuation, and guidance matter |
| Guidance, margins, and call tone often mattered more than headline EPS | The app needs qualitative and fundamental context |
| Price action after the report was highly informative | The app should require confirmation before issuing strong signals |

The most important conclusion is that the market was not simply “anti-earnings” or “short everything.” Instead, it was selective and often unforgiving. Companies needed not only to beat consensus, but also to clear a higher implied expectation bar.

---

### 52-week earnings-reaction pattern

The last 52 weeks broadly cover these S&P 500 reporting seasons:

| Earnings season | Approximate reporting window | Usefulness for the thesis |
|---|---:|---|
| Q2 2025 | July-August 2025 | Strong downside asymmetry |
| Q3 2025 | October-November 2025 | Beats weakly rewarded, misses punished |
| Q4 2025 | January-February 2026 | Milder negative reaction, but beats still not strongly rewarded |
| Q1 2026 | April-May 2026 | Strong earnings season, but negative surprises punished hard |

The aggregate pattern from public earnings-season data showed that positive surprises generally produced small average gains, while negative surprises often produced much larger average losses.

Indicative summary:

| Season | Positive EPS surprise reaction | Negative EPS surprise reaction | Interpretation |
|---|---:|---:|---|
| Q2 2025 | About +0.9% | About -5.6% | Misses punished extremely hard |
| Q3 2025 | Around flat to slightly positive | Around -4.5% to nearly -5% | Beats were barely rewarded; misses punished |
| Q4 2025 | About +0.6% | About -2.5% | Softer punishment, but positive rewards still modest |
| Q1 2026 | About +1.1% | About -4.6% to -4.9% | Strong beat season, harsh punishment for misses |

The exact figures depend on the source and measurement window, but the broad conclusion is consistent: the downside reaction to disappointment has recently been larger than the upside reaction to a normal beat.

---

### Why EPS surprise alone is not enough

A simple EPS-surprise strategy is too weak because most S&P 500 companies often beat estimates. If 75-85% of companies beat EPS, then “beat = buy” becomes structurally noisy.

The app must distinguish between:

| Report type | Meaning | Likely signal |
|---|---|---|
| EPS beat + revenue beat + guidance raise + margin strength | High-quality beat | Long candidate |
| EPS beat + revenue miss | Low-quality beat | Short watch or avoid long |
| EPS beat + margin miss | Profitability concern | Short watch |
| EPS beat + guidance maintained only | Possibly “not enough” | Watch price reaction |
| EPS beat + guidance cut | Bearish despite headline beat | Short candidate |
| EPS miss + revenue miss + guide down | Clean disappointment | Short candidate |
| EPS miss but stock rises | Bad news already priced in | Avoid short or possible long watch |
| Beat + stock gaps up and holds VWAP | Market confirms upside | Long candidate |
| Beat + stock sells off and fails VWAP | Market rejects the report | Short candidate |

The main mistake to avoid is treating “beat” as good and “miss” as bad without context.

---

### The most valuable short setup: low-quality beat rejected by the market

The app should pay special attention to reports that look good on the surface but fail underneath.

A low-quality beat may include:

- EPS beat but revenue weak.
- EPS beat but gross margin misses.
- EPS beat but operating margin compresses.
- EPS beat helped by cost cuts rather than demand.
- EPS beat helped by buybacks.
- Revenue beat but guidance lowered.
- Full-year guidance maintained when investors expected a raise.
- Free cash flow weaker than earnings.
- Inventory or receivables rising faster than sales.
- Management tone cautious despite headline beat.
- Analysts pushing hard during Q&A.
- Stock sells off despite the headline beat.

This is the “good, but not good enough” setup.

The ideal short pattern is:

```text
High valuation
+ strong pre-earnings run-up
+ positive headline EPS surprise
+ weak guidance, margins, cash flow, or call tone
+ negative price reaction
+ failure to reclaim post-earnings VWAP
= high-quality short candidate
```

This is the pattern most aligned with the original observation: solid companies selling off after earnings calls.

---

### Clean miss short setup

The simpler short setup is a clean miss confirmed by price action.

Pattern:

```text
EPS miss
+ revenue miss or guidance cut
+ stock gaps down
+ high volume
+ fails VWAP
+ underperforms sector ETF
= short continuation candidate
```

This setup is easier to detect and should probably be part of the MVP. However, it may be more crowded and less subtle than the low-quality beat setup.

---

### Priced-for-perfection failure setup

In an expensive market, some companies can report strong numbers and still fall because investors expected even more.

Pattern:

```text
Stock near 52-week high
+ high forward valuation
+ strong 20-day and 60-day run-up
+ optimistic analyst revisions
+ high media or AI/growth hype
+ decent earnings report
+ only in-line guidance or margin pressure
+ stock closes red
= priced-for-perfection failure
```

The point is not that the company is bad. The point is that the stock had little room for imperfection.

This setup is especially relevant for:

- Mega-cap growth.
- AI-related names.
- Software.
- Semiconductors.
- High-multiple consumer names.
- Companies with crowded institutional positioning.
- Companies with a strong pre-earnings rally.

---

### Long opportunity: clean beat-and-raise with confirmation

The long side should exist, but the app should be stricter than usual.

A good long setup requires more than an EPS beat:

```text
EPS beat
+ revenue beat
+ guidance raised
+ margins stable or expanding
+ positive call tone
+ stock gaps up and holds VWAP
+ sector-relative strength
= long continuation candidate
```

The long thesis is strongest when:

- Guidance is raised.
- The company beats on both revenue and EPS.
- The earnings beat is not caused mainly by cost cuts.
- The call tone is confident.
- Analysts revise estimates upward after the report.
- The stock holds above VWAP after the initial reaction.
- The stock outperforms its sector ETF.

Because positive surprises have often been rewarded only modestly, the app should avoid weak long signals.

---

### Why price confirmation is mandatory

The app should never issue a strong trade signal based only on the earnings release or transcript.

The market reaction is itself data.

Bearish confirmation includes:

- Gap down after earnings.
- Failure to reclaim post-earnings VWAP.
- Close below prior-day low.
- Lower highs intraday.
- Underperformance versus sector ETF.
- High relative volume.
- Actual downside move exceeding implied move.
- Weak recovery after the call.

Bullish confirmation includes:

- Gap up after earnings.
- Holds post-earnings VWAP.
- Closes near high of day.
- Outperforms sector ETF.
- High relative volume.
- Guidance raise accepted by the market.
- Positive analyst revisions after the call.

Hard rule:

```text
No Strong Short or Strong Long without price confirmation.
```

---

### Proposed signal taxonomy

The app should produce multiple labels, not just buy/sell.

| Signal | Meaning |
|---|---|
| Strong Short | High-confidence bearish setup with price confirmation |
| Short | Bearish setup, but not maximum confidence |
| Short Watch | Vulnerable before or shortly after earnings, but confirmation incomplete |
| Strong Long | High-confidence bullish setup with guidance and price confirmation |
| Long | Bullish setup, but not maximum confidence |
| Long Watch | Possible upside setup, but confirmation incomplete |
| Volatility Watch | Large expected move, direction unclear |
| Avoid | Risk too high, data poor, or conflicting signals |
| No Trade | No edge |

The app should be comfortable producing “No Trade” often.

---

### Core scoring concepts

#### Expectation Pressure Score

This is a pre-earnings score that asks:

> Is this stock priced for perfection?

Proposed formula:

```text
ExpectationPressureScore =
  25% valuation stretch
+ 20% pre-earnings run-up
+ 15% distance from 52-week high
+ 15% analyst optimism / revisions
+ 10% options implied move
+ 10% sector momentum
+ 5% hype / news intensity
```

Interpretation:

| Score | Meaning |
|---:|---|
| 0-39 | Low expectation pressure |
| 40-59 | Normal |
| 60-74 | Elevated |
| 75-100 | Priced for perfection |

High expectation pressure is not a short signal by itself. It means the stock is vulnerable if the report is not perfect.

---

#### Earnings Quality Score

This score evaluates whether the report is genuinely strong.

Inputs:

```text
EPS surprise
Revenue surprise
Gross margin trend
Operating margin trend
Free cash flow trend
Segment growth
Inventory trend
Receivables trend
Deferred revenue trend
Capex trend
Buyback contribution
One-time gains/losses
Guidance versus consensus
```

Interpretation:

| Score | Meaning |
|---:|---|
| 0-39 | Weak earnings quality |
| 40-59 | Mixed |
| 60-74 | Good |
| 75-100 | Strong |

A company can beat EPS and still have a weak Earnings Quality Score.

---

#### Guidance Score

This score evaluates forward-looking management expectations.

Inputs:

```text
Next-quarter revenue guidance vs consensus
Next-quarter EPS guidance vs consensus
Full-year revenue guidance vs consensus
Full-year EPS guidance vs consensus
Margin guidance
Capex guidance
Demand commentary
Management confidence
```

Interpretation:

| Score | Meaning |
|---:|---|
| 0-39 | Guide down / clearly weak |
| 40-59 | Mixed or cautious |
| 60-74 | Solid |
| 75-100 | Raised / strong |

Guidance should carry significant weight because earnings reactions are forward-looking.

---

#### Call Tone Score

The LLM should extract structured evidence from the call transcript.

It should score:

```text
Prepared remarks tone
Q&A tone
Demand trend
Margin trend
Pricing power
Customer behavior
Cost pressure
Inventory risk
Receivables risk
Analyst pushback
Management evasiveness
AI/growth hype versus actual numbers
```

The Q&A section should carry more weight than prepared remarks because analysts often expose weak points there.

---

#### Price Reaction Score

Inputs:

```text
Gap direction
Gap size
VWAP hold/fail
Close vs open
Close vs prior close
Relative move vs sector ETF
Volume vs 20-day average
Actual move vs options implied move
Intraday recovery or fade
```

Interpretation:

| Score | Meaning |
|---:|---|
| 0-39 | Bearish market verdict |
| 40-59 | Mixed |
| 60-74 | Bullish |
| 75-100 | Strong bullish confirmation |

For the short model, use a bearish version of this score.

---

### Short Score

Proposed formula:

```text
ShortScore =
  20% expectation pressure
+ 20% bearish price reaction
+ 15% guidance weakness
+ 15% margin / earnings quality weakness
+ 10% revenue weakness
+ 10% negative call tone
+ 5% sector-relative weakness
+ 5% market regime risk
```

Signal thresholds:

| Score | Output |
|---:|---|
| 0-49 | No short |
| 50-64 | Weak short watch |
| 65-74 | Short watch |
| 75-84 | Short |
| 85-100 | Strong short |

Hard blockers:

```text
No Strong Short if:
- stock is above post-earnings VWAP
- sector-relative strength is positive
- borrow is unavailable
- borrow cost is extreme
- short interest / squeeze risk is extreme
- spread or liquidity is poor
- the stock has already moved too far below implied move
```

---

### Long Score

Proposed formula:

```text
LongScore =
  20% EPS surprise strength
+ 20% guidance raise
+ 15% bullish price reaction
+ 15% earnings quality
+ 10% positive call tone
+ 10% sector-relative strength
+ 10% valuation sanity
```

Signal thresholds:

| Score | Output |
|---:|---|
| 0-49 | No long |
| 50-64 | Weak long watch |
| 65-74 | Long watch |
| 75-84 | Long |
| 85-100 | Strong long |

Hard blockers:

```text
No Strong Long if:
- guidance is weak
- stock fails VWAP
- stock underperforms sector ETF
- valuation is extreme and guidance is only in-line
- stock has already moved more than 2x implied move
- market is in sharp risk-off mode
```

---

### Risk filters

The app should apply risk filters before showing a tradeable signal.

Short blockers:

```text
Borrow unavailable
Borrow fee too high
Short interest extremely high
Float too low
Average volume too low
Bid/ask spread too wide
Stock already down more than 2x implied move
Major macro event same day
Takeover rumor or strategic review
Meme-stock behavior
Extreme gap with no clean stop
```

Long blockers:

```text
Stock already up more than 2x implied move
Guidance weak despite beat
Market selling off aggressively
Low liquidity
Gap up fades below VWAP
Valuation extreme with no guidance raise
Poor earnings quality
```

Portfolio blockers:

```text
Too many correlated positions
Too many same-sector trades
Max daily loss reached
Max weekly loss reached
Open event risk too high
Signal concentration too high
```

---

### Final research conclusion

The app should be a post-earnings confirmation engine, not a pre-earnings gambling engine.

The strongest short setup is:

```text
Expensive/crowded stock
+ low-quality beat or miss
+ weak guidance, margins, cash flow, or call tone
+ negative price reaction
+ VWAP failure
= short signal
```

The strongest long setup is:

```text
Clean beat
+ guidance raise
+ high-quality earnings
+ positive call tone
+ stock holds VWAP
+ sector-relative strength
= long signal
```

The key product principle is:

```text
Watch before earnings.
Trade only after confirmation.
Track everything in forward-test mode.
```

---

### Research sources to use for validation

Suggested primary and secondary sources:

1. FactSet Earnings Insight reports for quarterly S&P 500 earnings surprise and price-reaction statistics.
2. S&P 500 earnings calendar and constituent data.
3. Historical price data from Polygon, Alpaca, Tiingo, or similar.
4. Consensus estimates from Financial Modeling Prep, FactSet, Refinitiv, Benzinga, or similar.
5. Earnings-call transcripts from Quartr, Seeking Alpha, Finnhub, AlphaSense, or similar.
6. Options implied move data from Polygon Options, Tradier, ORATS, or similar.
7. Academic literature on post-earnings-announcement drift and earnings-call tone.

References that motivated the thesis:

- FactSet Earnings Insight: https://www.factset.com/earningsinsight
- FactSet Q1 analysis on negative EPS surprise punishment: https://insight.factset.com/market-is-punishing-negative-eps-surprises-more-than-average-for-q1
- FactSet Q2 analysis on negative EPS surprise punishment: https://insight.factset.com/market-is-punishing-negative-eps-surprises-more-than-average-for-q2-1
- Investopedia summary of Q3 2025 earnings reaction asymmetry: https://www.investopedia.com/investors-are-punishing-the-stocks-of-companies-that-miss-earnings-expectations-11844959
- Investopedia summary of Q1 2026 earnings season: https://www.investopedia.com/america-s-biggest-companies-had-a-strong-q1-traders-have-been-hard-to-impress-11973008

---

## 2. Detailed Development Plan

### Product definition

Build a web app that scans S&P 500 earnings events and generates long/short earnings-reaction signals.

The app should:

1. Build a weekly watchlist before earnings.
2. Score each company’s expectation pressure.
3. Ingest actual earnings results.
4. Compare actual results with consensus.
5. Parse guidance and earnings quality.
6. Analyze the earnings-call transcript.
7. Confirm with price action.
8. Generate a signal only when enough evidence exists.
9. Store every signal immutably for forward testing.
10. Optionally paper trade via Alpaca after validation.

The app should not make live trading decisions in v1. It should first prove the signal logic with historical backtests and forward testing.

---

### Recommended technology stack

Given a .NET and Angular background, use:

```text
Frontend:
- Angular
- Tailwind or Angular Material
- Lightweight charting library, e.g. TradingView Lightweight Charts

Backend:
- .NET 8 Web API
- Entity Framework Core
- PostgreSQL
- Redis
- Hangfire or Quartz.NET for scheduled jobs

Research / ML:
- Python microservice
- pandas
- numpy
- scikit-learn
- vectorized backtesting module

Infrastructure:
- Docker
- Docker Compose
- Portainer on Ubuntu server
- Nginx reverse proxy
- HTTPS via Caddy or Certbot

Broker / execution:
- Alpaca Paper Trading API first
- Real trading only after forward-test validation
```

---

### High-level architecture

```text
Angular Frontend
        |
.NET 8 API
        |
PostgreSQL ---- Redis
        |
Background Jobs
        |
Python Research / Backtest Service
        |
External Data Providers
        |
Alpaca Paper Trading
```

Main backend modules:

```text
MarketData
Earnings
Fundamentals
Estimates
Guidance
Transcripts
LLMAnalysis
Signals
Backtesting
ForwardTesting
PaperTrading
Notifications
Admin
```

---

### Core data providers

MVP provider choices:

| Data need | Possible providers |
|---|---|
| S&P 500 universe | Slickcharts, Wikipedia, paid index provider |
| Earnings calendar | Financial Modeling Prep, Finnhub, Nasdaq, Polygon |
| Daily prices | Alpaca, Polygon, Tiingo |
| Intraday prices | Alpaca, Polygon |
| Fundamentals | FMP, Polygon, FactSet if budget allows |
| Consensus estimates | FMP, Benzinga, FactSet, Refinitiv |
| Guidance | FMP, Benzinga, press releases, transcripts |
| Transcripts | Quartr, Seeking Alpha, Finnhub, EarningsCall.biz, AlphaSense |
| Options implied move | Polygon Options, Tradier, ORATS |
| Short interest / borrow | Interactive Brokers, S3, Ortex, Fintel, broker API if available |

The MVP can start without options and borrow data, but those should become blockers before live shorting.

---

### Database design

#### companies

```sql
CREATE TABLE companies (
    id BIGSERIAL PRIMARY KEY,
    ticker TEXT NOT NULL UNIQUE,
    name TEXT NOT NULL,
    sector TEXT,
    industry TEXT,
    market_cap NUMERIC,
    is_sp500 BOOLEAN DEFAULT FALSE,
    active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMPTZ DEFAULT now(),
    updated_at TIMESTAMPTZ DEFAULT now()
);
```

#### earnings_events

```sql
CREATE TABLE earnings_events (
    id BIGSERIAL PRIMARY KEY,
    company_id BIGINT NOT NULL REFERENCES companies(id),
    ticker TEXT NOT NULL,
    fiscal_year INT,
    fiscal_quarter INT,
    report_date DATE NOT NULL,
    report_time TEXT, -- BMO, AMC, INTRADAY, UNKNOWN
    source TEXT,
    confirmed BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMPTZ DEFAULT now(),
    updated_at TIMESTAMPTZ DEFAULT now()
);
```

#### earnings_estimates

```sql
CREATE TABLE earnings_estimates (
    id BIGSERIAL PRIMARY KEY,
    earnings_event_id BIGINT NOT NULL REFERENCES earnings_events(id),
    eps_estimate NUMERIC,
    revenue_estimate NUMERIC,
    ebitda_estimate NUMERIC,
    gross_margin_estimate NUMERIC,
    operating_margin_estimate NUMERIC,
    source TEXT,
    as_of_date DATE,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### earnings_actuals

```sql
CREATE TABLE earnings_actuals (
    id BIGSERIAL PRIMARY KEY,
    earnings_event_id BIGINT NOT NULL REFERENCES earnings_events(id),
    eps_actual NUMERIC,
    revenue_actual NUMERIC,
    gross_margin_actual NUMERIC,
    operating_margin_actual NUMERIC,
    free_cash_flow NUMERIC,
    capex NUMERIC,
    inventory NUMERIC,
    receivables NUMERIC,
    deferred_revenue NUMERIC,
    eps_surprise_pct NUMERIC,
    revenue_surprise_pct NUMERIC,
    source TEXT,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### guidance_events

```sql
CREATE TABLE guidance_events (
    id BIGSERIAL PRIMARY KEY,
    earnings_event_id BIGINT NOT NULL REFERENCES earnings_events(id),
    period TEXT, -- NEXT_QUARTER, FULL_YEAR
    metric TEXT, -- EPS, REVENUE, MARGIN, CAPEX
    guidance_low NUMERIC,
    guidance_high NUMERIC,
    consensus NUMERIC,
    guidance_surprise_pct NUMERIC,
    direction TEXT, -- RAISED, MAINTAINED, LOWERED, MIXED, UNKNOWN
    raw_text TEXT,
    source TEXT,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### daily_prices

```sql
CREATE TABLE daily_prices (
    id BIGSERIAL PRIMARY KEY,
    ticker TEXT NOT NULL,
    trade_date DATE NOT NULL,
    open NUMERIC NOT NULL,
    high NUMERIC NOT NULL,
    low NUMERIC NOT NULL,
    close NUMERIC NOT NULL,
    adjusted_close NUMERIC,
    volume BIGINT,
    source TEXT,
    UNIQUE(ticker, trade_date)
);
```

#### intraday_prices

```sql
CREATE TABLE intraday_prices (
    id BIGSERIAL PRIMARY KEY,
    ticker TEXT NOT NULL,
    bar_time TIMESTAMPTZ NOT NULL,
    timeframe TEXT NOT NULL, -- 1m, 5m, 15m
    open NUMERIC NOT NULL,
    high NUMERIC NOT NULL,
    low NUMERIC NOT NULL,
    close NUMERIC NOT NULL,
    volume BIGINT,
    vwap NUMERIC,
    source TEXT,
    UNIQUE(ticker, bar_time, timeframe)
);
```

#### fundamentals_snapshots

```sql
CREATE TABLE fundamentals_snapshots (
    id BIGSERIAL PRIMARY KEY,
    ticker TEXT NOT NULL,
    snapshot_date DATE NOT NULL,
    forward_pe NUMERIC,
    trailing_pe NUMERIC,
    ev_sales NUMERIC,
    ev_ebitda NUMERIC,
    price_sales NUMERIC,
    fcf_yield NUMERIC,
    revenue_growth_yoy NUMERIC,
    eps_growth_yoy NUMERIC,
    gross_margin NUMERIC,
    operating_margin NUMERIC,
    debt_to_ebitda NUMERIC,
    source TEXT,
    UNIQUE(ticker, snapshot_date)
);
```

#### transcripts

```sql
CREATE TABLE transcripts (
    id BIGSERIAL PRIMARY KEY,
    earnings_event_id BIGINT NOT NULL REFERENCES earnings_events(id),
    ticker TEXT NOT NULL,
    transcript_text TEXT NOT NULL,
    source TEXT,
    call_datetime TIMESTAMPTZ,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### llm_transcript_analysis

```sql
CREATE TABLE llm_transcript_analysis (
    id BIGSERIAL PRIMARY KEY,
    transcript_id BIGINT NOT NULL REFERENCES transcripts(id),
    model_name TEXT NOT NULL,
    prompt_version TEXT NOT NULL,
    management_tone TEXT,
    qa_tone TEXT,
    guidance_tone TEXT,
    demand_trend TEXT,
    margin_trend TEXT,
    revenue_quality TEXT,
    earnings_quality TEXT,
    analyst_pushback TEXT,
    management_evasiveness TEXT,
    confidence NUMERIC,
    red_flags JSONB,
    green_flags JSONB,
    bullish_evidence JSONB,
    bearish_evidence JSONB,
    raw_json JSONB,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### signals

```sql
CREATE TABLE signals (
    id BIGSERIAL PRIMARY KEY,
    earnings_event_id BIGINT NOT NULL REFERENCES earnings_events(id),
    ticker TEXT NOT NULL,
    signal_type TEXT NOT NULL, -- STRONG_SHORT, SHORT, SHORT_WATCH, LONG, etc.
    direction TEXT NOT NULL, -- LONG, SHORT, NONE
    score NUMERIC NOT NULL,
    confidence NUMERIC,
    generated_at TIMESTAMPTZ NOT NULL DEFAULT now(),
    model_version TEXT NOT NULL,
    entry_rule TEXT,
    entry_price NUMERIC,
    stop_price NUMERIC,
    target_price NUMERIC,
    invalidation_level NUMERIC,
    holding_period_days INT,
    reasons JSONB,
    risks JSONB,
    features_json JSONB,
    status TEXT DEFAULT 'OPEN',
    immutable BOOLEAN DEFAULT TRUE
);
```

#### signal_outcomes

```sql
CREATE TABLE signal_outcomes (
    id BIGSERIAL PRIMARY KEY,
    signal_id BIGINT NOT NULL REFERENCES signals(id),
    return_1d NUMERIC,
    return_3d NUMERIC,
    return_5d NUMERIC,
    return_10d NUMERIC,
    max_favorable_excursion NUMERIC,
    max_adverse_excursion NUMERIC,
    hit_stop BOOLEAN,
    hit_target BOOLEAN,
    outcome_label TEXT,
    calculated_at TIMESTAMPTZ DEFAULT now()
);
```

#### backtest_runs

```sql
CREATE TABLE backtest_runs (
    id BIGSERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    strategy_name TEXT NOT NULL,
    parameters JSONB NOT NULL,
    universe TEXT,
    start_date DATE,
    end_date DATE,
    model_version TEXT,
    results JSONB,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

#### paper_trades

```sql
CREATE TABLE paper_trades (
    id BIGSERIAL PRIMARY KEY,
    signal_id BIGINT REFERENCES signals(id),
    ticker TEXT NOT NULL,
    direction TEXT NOT NULL,
    quantity NUMERIC NOT NULL,
    entry_price NUMERIC,
    exit_price NUMERIC,
    stop_price NUMERIC,
    target_price NUMERIC,
    opened_at TIMESTAMPTZ,
    closed_at TIMESTAMPTZ,
    status TEXT,
    pnl NUMERIC,
    broker_order_id TEXT,
    broker_position_id TEXT,
    created_at TIMESTAMPTZ DEFAULT now()
);
```

---

### Backend services

#### CompanyUniverseService

Responsibilities:

```text
Load S&P 500 constituents
Track additions/removals
Map tickers to company metadata
Handle symbol changes
```

#### EarningsCalendarService

Responsibilities:

```text
Fetch upcoming earnings dates
Confirm BMO/AMC timing
Store and update earnings events
Detect changes in report dates
```

#### MarketDataService

Responsibilities:

```text
Fetch daily OHLCV
Fetch intraday bars
Calculate VWAP
Calculate volume ratios
Calculate T-2 to T+2 returns
Calculate gap and close-to-close returns
```

#### FundamentalsService

Responsibilities:

```text
Fetch valuation metrics
Fetch growth metrics
Calculate sector-relative valuation percentiles
Store snapshots before earnings
```

#### EstimatesService

Responsibilities:

```text
Fetch EPS and revenue consensus
Track revisions into earnings
Calculate estimate momentum
Store estimates as-of date
```

#### EarningsActualsService

Responsibilities:

```text
Fetch reported EPS and revenue
Calculate surprise percentages
Store actual results
Flag mixed reports
```

#### GuidanceService

Responsibilities:

```text
Extract guidance from structured provider if available
Fallback to press-release parsing
Compare guidance to consensus
Classify raised / maintained / lowered / mixed
```

#### TranscriptService

Responsibilities:

```text
Fetch earnings-call transcript
Split prepared remarks and Q&A
Normalize speaker labels
Store transcript
```

#### LLMAnalysisService

Responsibilities:

```text
Run transcript prompt
Validate JSON response
Store red flags and green flags
Create Call Tone Score
Track prompt and model versions
```

#### SignalEngine

Responsibilities:

```text
Calculate Expectation Pressure Score
Calculate Earnings Quality Score
Calculate Guidance Score
Calculate Price Reaction Score
Calculate ShortScore and LongScore
Apply hard blockers
Generate signal
Store immutable signal record
```

#### BacktestEngine

Responsibilities:

```text
Run historical strategy simulations
Apply entry and exit rules
Calculate returns
Calculate drawdowns
Calculate win rate and profit factor
Compare strategy variants
```

#### ForwardTestService

Responsibilities:

```text
Freeze every signal at generation time
Calculate future returns automatically
Compare live signal performance by model version
Prevent hindsight editing
```

#### PaperTradingService

Responsibilities:

```text
Connect to Alpaca paper account
Size positions based on risk
Place paper orders
Track open positions
Close positions based on exit rules
Store paper trade results
```

---

### Signal engine workflow

#### Pre-earnings workflow

Runs daily, with a stronger scheduled run Sunday evening.

```text
1. Load S&P 500 companies reporting this week.
2. Fetch latest prices, valuation, estimates, and historical earnings reactions.
3. Calculate pre-earnings 20-day and 60-day returns.
4. Calculate distance from 52-week high.
5. Calculate sector-relative valuation.
6. Calculate options implied move if available.
7. Calculate Expectation Pressure Score.
8. Assign pre-earnings label:
   - Short Watch
   - Long Watch
   - Volatility Watch
   - Avoid
   - No Trade
9. Display in Weekly Scanner.
```

#### Earnings-release workflow

Runs when earnings are released.

```text
1. Detect earnings release.
2. Fetch EPS actual, revenue actual, and key financial metrics.
3. Compare with consensus.
4. Calculate EPS surprise and revenue surprise.
5. Parse guidance.
6. Calculate Earnings Quality Score.
7. Calculate Guidance Score.
8. Update event status.
```

#### Post-release price workflow

Runs intraday after the report.

```text
1. Fetch 1-minute or 5-minute candles.
2. Calculate post-earnings VWAP.
3. Detect gap direction and size.
4. Compare price to VWAP.
5. Compare move to sector ETF.
6. Compare actual move to implied move.
7. Calculate Price Reaction Score.
```

#### Post-call workflow

Runs after transcript is available.

```text
1. Fetch transcript.
2. Separate prepared remarks and Q&A.
3. Run LLM structured extraction.
4. Validate JSON.
5. Store red flags and green flags.
6. Calculate Call Tone Score.
7. Recalculate final ShortScore and LongScore.
```

#### Final signal workflow

```text
1. Combine all scores.
2. Apply hard blockers.
3. Generate signal type.
4. Generate explanation.
5. Store immutable signal.
6. Add to Live Signals dashboard.
7. Track outcome automatically.
```

---

### LLM transcript prompt

Use the LLM only as an evidence extractor, not as the final decision maker.

```md
You are an earnings-call analysis engine for a trading signal system.

Analyze the provided earnings press release, guidance, and call transcript.

Return only valid JSON.

Your job is not to recommend a trade. Your job is to extract structured evidence.

Focus on:
- demand strength
- revenue quality
- margin pressure
- guidance quality
- management confidence
- evasive answers
- analyst skepticism
- one-time benefits
- macro excuses
- inventory / receivables / churn / pricing issues
- AI or growth hype unsupported by numbers
- whether the report exceeded official consensus but disappointed implied expectations

Return:

{
  "company": "",
  "ticker": "",
  "quarter": "",
  "management_tone": "very_positive | positive | neutral | cautious | negative",
  "qa_tone": "very_positive | positive | neutral | cautious | negative",
  "guidance_tone": "raised | maintained | mixed | lowered | withdrawn",
  "demand_trend": "accelerating | stable | slowing | unclear",
  "margin_trend": "expanding | stable | pressured | unclear",
  "revenue_quality": "high | medium | low | unclear",
  "earnings_quality": "high | medium | low | unclear",
  "analyst_pushback": "low | medium | high",
  "management_evasiveness": "low | medium | high",
  "red_flags": [],
  "green_flags": [],
  "bearish_evidence": [],
  "bullish_evidence": [],
  "summary": "",
  "confidence": 0
}
```

---

### API design

#### Earnings

```http
GET /api/earnings/upcoming
GET /api/earnings/recent
GET /api/earnings/{id}
GET /api/earnings/{id}/reaction
GET /api/earnings/{id}/transcript-analysis
```

#### Companies

```http
GET /api/companies
GET /api/companies/{ticker}
GET /api/companies/{ticker}/earnings-history
GET /api/companies/{ticker}/fundamentals
GET /api/companies/{ticker}/price
```

#### Signals

```http
GET /api/signals/live
GET /api/signals/history
GET /api/signals/{id}
GET /api/signals/{id}/outcome
POST /api/signals/recalculate
```

#### Backtests

```http
GET /api/backtests
POST /api/backtests/run
GET /api/backtests/{id}
GET /api/backtests/{id}/trades
```

#### Forward tests

```http
GET /api/forward-tests
GET /api/forward-tests/summary
GET /api/forward-tests/by-model-version
```

#### Paper trading

```http
GET /api/paper-trading/account
GET /api/paper-trading/positions
GET /api/paper-trading/trades
POST /api/paper-trading/enable
POST /api/paper-trading/disable
POST /api/paper-trading/close/{tradeId}
```

---

### Frontend pages

#### 1. Weekly Earnings Scanner

Purpose:

```text
Show upcoming S&P 500 earnings and pre-earnings watch classifications.
```

Columns:

```text
Ticker
Company
Report date
Report time
Sector
Market cap
Forward P/E
EV/Sales
20-day return
60-day return
Distance from 52-week high
Expected move
Historical earnings move
Expectation Pressure Score
Pre-signal
```

Filters:

```text
Sector
Market cap
Report day
Report time
Expectation pressure
Historical volatility
Short Watch only
Long Watch only
Avoid hidden/shown
```

---

#### 2. Live Signal Dashboard

Purpose:

```text
Show actionable post-earnings signals.
```

Sections:

```text
Strong Short
Short
Short Watch
Strong Long
Long
Long Watch
Avoid
No Trade
```

Each signal card should show:

```text
Ticker
Signal
Score
Confidence
Setup type
Main reasons
Invalidation level
Stop level
Holding window
Data timestamp
Model version
```

---

#### 3. Ticker Detail Page

Purpose:

```text
Explain why a signal exists.
```

Sections:

```text
Company overview
Earnings event summary
EPS/revenue surprise
Guidance analysis
Price reaction chart
VWAP status
Sector-relative performance
Valuation context
Historical earnings reactions
Transcript red flags
Transcript green flags
Signal explanation
Risk blockers
Outcome tracking
```

---

#### 4. Backtest Lab

Purpose:

```text
Validate strategies before live use.
```

Controls:

```text
Strategy type
Date range
Sector
Market cap
Valuation bucket
Holding period
VWAP filter on/off
Guidance filter on/off
Transcript filter on/off
Price reaction threshold
```

Metrics:

```text
Total trades
Win rate
Average return
Median return
Profit factor
Sharpe
Sortino
Max drawdown
Average favorable excursion
Average adverse excursion
Best sector
Worst sector
Best market regime
Worst market regime
```

---

#### 5. Forward Test Results

Purpose:

```text
Measure live generated signals without hindsight.
```

Sections:

```text
All generated signals
Performance by signal type
Performance by model version
Performance by sector
Performance by holding period
Signal decay
Open signals
Closed outcomes
```

---

#### 6. Paper Trading Dashboard

Purpose:

```text
Track Alpaca paper trades generated from approved signal rules.
```

Sections:

```text
Account value
Open positions
Closed trades
P/L by signal type
P/L by sector
Max drawdown
Current risk exposure
Trade logs
Rejected trades and reasons
```

---

### Backtesting plan

Start with simple, explainable strategies.

#### Strategy A: Clean Miss Short

Entry:

```text
Next regular-session open after earnings confirmation
```

Conditions:

```text
EPS surprise < 0
Revenue surprise <= 0 or guidance cut
T-2 to T+2 return < -2%
Volume > 1.5x 20-day average
```

Exit tests:

```text
Hold 3 trading days
Hold 5 trading days
Hold 10 trading days
Exit on VWAP reclaim
Exit on stop above earnings-day high
```

---

#### Strategy B: Low-Quality Beat Short

Entry:

```text
Next regular-session open after earnings confirmation
```

Conditions:

```text
EPS surprise > 0
Revenue surprise weak or margin weakness
Guidance not raised
Stock reaction < -2%
ExpectationPressureScore >= 70
```

Exit tests:

```text
Hold 3 trading days
Hold 5 trading days
Hold 10 trading days
Exit if stock reclaims VWAP
Exit if stock closes above earnings-day high
```

---

#### Strategy C: Beat Rejected Short

Entry:

```text
Next open after earnings-day rejection
```

Conditions:

```text
EPS surprise > 0
Stock closes red after report
Fails VWAP
Underperforms sector ETF
High relative volume
```

Exit tests:

```text
Hold 5 trading days
Exit on VWAP reclaim
Exit on sector-relative strength reversal
```

---

#### Strategy D: Beat-and-Raise Long

Entry:

```text
Next regular-session open after earnings confirmation
```

Conditions:

```text
EPS surprise > 0
Revenue surprise > 0
Guidance raised
Stock reaction > +2%
Holds VWAP
Sector-relative strength positive
```

Exit tests:

```text
Hold 5 trading days
Hold 10 trading days
Hold 20 trading days
Exit on VWAP failure
Exit on close below earnings-day low
```

---

#### Strategy E: Bad News Priced In Long Watch

Entry:

```text
Next open after reversal confirmation
```

Conditions:

```text
EPS miss
Stock opens down but closes green
Guidance not disastrous
Valuation already compressed
High volume reversal
Sector-relative strength positive
```

This should be lower priority than the short strategies, but it may identify powerful reversals.

---

### Forward-testing rules

Forward testing is mandatory before real trading.

Rules:

```text
Every signal must be saved at generation time.
Signals cannot be edited after generation.
Feature values must be saved exactly as known at the time.
Model version must be saved.
Prompt version must be saved for LLM-derived fields.
Outcomes are calculated automatically later.
Manual notes may be added, but original signal data is immutable.
```

Outcome windows:

```text
1 trading day
3 trading days
5 trading days
10 trading days
20 trading days
```

Track:

```text
Return
Maximum favorable excursion
Maximum adverse excursion
Stop hit
Target hit
VWAP reclaim/failure
Sector-relative return
```

---

### Paper-trading rules

Only enable paper trading after the signal engine has produced enough forward-tested signals.

Initial paper-trading constraints:

```text
Max 3 open positions
Max 1% account risk per trade
Max 2 positions per sector
No trade if confidence < 75
No trade before earnings release
No trade without price confirmation
No low-volume stocks
No wide-spread stocks
No high short-interest shorts
No unavailable-borrow shorts
No trade after max daily loss
No trade after max weekly loss
```

Short trade risk rule:

```text
For short trades, prefer defined-risk options later.
For MVP, paper trade equity shorts only if borrow and liquidity are acceptable.
```

---

### Signal explanation format

Every signal should be explainable.

Example:

```text
Signal: Strong Short
Setup: Low-quality beat rejected by market
Confidence: 82/100

Reasons:
1. Expectation Pressure Score is high: 81/100.
2. EPS beat, but revenue growth decelerated.
3. Operating margin missed expectations.
4. Guidance was maintained, not raised.
5. Q&A tone was cautious.
6. Stock failed post-earnings VWAP.
7. Stock underperformed sector ETF by 2.3%.

Invalidation:
- Reclaim and hold post-earnings VWAP.
- Close above earnings-day high.

Suggested holding window:
- 3 to 10 trading days.

Risks:
- Company remains fundamentally high quality.
- Buyback support may limit downside.
- Market regime is risk-on.
- Short squeeze risk is moderate.
```

The UI should always show the “why,” not just the label.

---

### Development phases

#### Phase 1: Research database

Goal:

```text
Create the historical foundation for testing the thesis.
```

Build:

```text
S&P 500 universe loader
Earnings calendar ingestion
Daily price ingestion
EPS/revenue estimate ingestion
EPS/revenue actual ingestion
T-2 to T+2 reaction calculator
Basic earnings event dashboard
```

Deliverable:

```text
A database and UI showing the last 52 weeks of S&P 500 earnings events and outcomes.
```

---

#### Phase 2: Backtest Lab v1

Goal:

```text
Test whether the simple strategies work historically.
```

Build:

```text
Backtest engine
Strategy configuration UI
Result metrics
Trade list
Export to CSV
```

Deliverable:

```text
Backtest Lab with Clean Miss Short, Low-Quality Beat Short, Beat Rejected Short, and Beat-and-Raise Long.
```

---

#### Phase 3: Pre-earnings scanner

Goal:

```text
Generate weekly earnings watchlists.
```

Build:

```text
Expectation Pressure Score
Weekly scanner UI
Short Watch / Long Watch / Volatility Watch / Avoid labels
Historical reaction profile
Valuation snapshot
Run-up analysis
```

Deliverable:

```text
Sunday evening S&P 500 earnings watchlist.
```

---

#### Phase 4: Post-earnings signal engine

Goal:

```text
Generate real post-earnings signals.
```

Build:

```text
Earnings actuals parser
Guidance parser
Earnings Quality Score
Guidance Score
Price Reaction Score
VWAP detection
Sector-relative performance
ShortScore
LongScore
Signal explanation generator
```

Deliverable:

```text
Live Signal Dashboard with signal explanations and risk blockers.
```

---

#### Phase 5: Transcript and LLM engine

Goal:

```text
Add qualitative evidence from earnings calls.
```

Build:

```text
Transcript ingestion
Transcript normalization
Prepared remarks vs Q&A splitting
LLM structured extraction
Call Tone Score
Red flag / green flag UI
Prompt version tracking
```

Deliverable:

```text
Ticker Detail page with transcript-derived evidence.
```

---

#### Phase 6: Forward testing

Goal:

```text
Validate signals without hindsight.
```

Build:

```text
Immutable signal storage
Outcome calculator
Performance by model version
Performance by signal type
Performance by sector
Signal decay analysis
```

Deliverable:

```text
Forward Test Results dashboard.
```

---

#### Phase 7: Paper trading

Goal:

```text
Test execution logic safely.
```

Build:

```text
Alpaca paper account integration
Position sizing
Paper order placement
Paper trade monitoring
Stop/exit logic
P/L dashboard
Rejected trade logs
```

Deliverable:

```text
Paper Trading dashboard.
```

---

#### Phase 8: Options and advanced risk

Goal:

```text
Improve short risk management.
```

Build:

```text
Options implied move ingestion
Options liquidity filters
Put spread strategy suggestions
Borrow availability checks
Short interest checks
Defined-risk trade templates
```

Deliverable:

```text
Options-aware signal and risk module.
```

---

### MVP scope

The first practical MVP should include only:

```text
S&P 500 universe
Upcoming earnings calendar
Historical last-52-week earnings reactions
EPS/revenue surprise data
Daily prices
T-2 to T+2 return
Pre-earnings 20-day and 60-day run-up
Basic valuation snapshot
Expectation Pressure Score
Basic signal labels
Backtest Lab v1
```

Avoid in MVP v1:

```text
Real trading
Options
Complex ML
Overfitted models
Unstructured transcript dependency
Black-box LLM trade decisions
```

MVP v1 success criteria:

```text
Can reproduce the broad earnings-reaction asymmetry.
Can show which setups performed best historically.
Can classify upcoming earnings into useful watchlists.
Can explain why a company is a Short Watch or Long Watch.
```

---

### Versioned model design

Every signal should include:

```text
Model version
Feature version
Prompt version
Data timestamp
Signal timestamp
Known data at generation time
```

Example:

```json
{
  "model_version": "earnings-reaction-v0.3",
  "feature_version": "features-v0.2",
  "prompt_version": "transcript-v0.1",
  "generated_at": "2026-05-24T18:45:00Z"
}
```

This prevents accidental hindsight bias.

---

### Suggested model evolution

#### v0.1

Rules only.

```text
EPS surprise
Revenue surprise
T-2 to T+2 return
Pre-earnings run-up
Valuation
```

#### v0.2

Add price confirmation.

```text
VWAP
Volume ratio
Sector-relative return
Gap behavior
```

#### v0.3

Add guidance.

```text
Guidance direction
Guidance surprise
Full-year versus next-quarter distinction
```

#### v0.4

Add transcript analysis.

```text
Call tone
Q&A tone
Red flags
Green flags
Management evasiveness
Analyst pushback
```

#### v0.5

Add risk filters.

```text
Borrow cost
Short interest
Options implied move
Liquidity filters
Spread filters
```

#### v1.0

Paper-trading ready.

```text
Forward-tested
Risk-controlled
Signal explanations stable
No major data leakage
```

---

### Key engineering risks

#### Data quality risk

Earnings estimates, guidance, and transcripts can be messy or expensive.

Mitigation:

```text
Start with fewer data providers.
Store source and timestamp for every field.
Allow manual inspection.
Prefer simple fields first.
```

#### Look-ahead bias

Backtests can accidentally use data that was not available at signal time.

Mitigation:

```text
Use as-of timestamps.
Store estimate snapshots.
Separate release time, call time, and price time.
Freeze signals in forward testing.
```

#### Overfitting

The app can easily become optimized to the last few quarters.

Mitigation:

```text
Start with rules.
Use simple thresholds.
Test across sectors and regimes.
Use walk-forward validation.
Track live forward performance.
```

#### LLM hallucination

The LLM may infer things not in the transcript.

Mitigation:

```text
Force JSON output.
Ask for evidence only.
Do not let LLM decide trade.
Store confidence.
Validate output schema.
Optionally require quote references internally.
```

#### Short risk

Shorting has asymmetric risk.

Mitigation:

```text
Use hard blockers.
Limit position size.
Prefer paper trading first.
Later prefer defined-risk put spreads.
Avoid high short-interest names.
```

---

### What to build first, concretely

First implementation sprint:

```text
1. Create .NET 8 API project.
2. Create Angular project.
3. Create PostgreSQL schema for companies, earnings_events, estimates, actuals, daily_prices, signals.
4. Load S&P 500 universe.
5. Ingest upcoming earnings calendar.
6. Ingest daily prices.
7. Calculate pre-earnings 20-day and 60-day returns.
8. Calculate T-2 to T+2 earnings reaction for historical events.
9. Build Weekly Scanner table.
10. Build basic Backtest Lab for simple strategy rules.
```

Second implementation sprint:

```text
1. Add valuation snapshots.
2. Add Expectation Pressure Score.
3. Add EPS/revenue surprise calculation.
4. Add simple signal labels.
5. Add signal detail page.
6. Add immutable signal storage.
7. Add outcome tracking for 1, 3, 5, and 10 trading days.
```

Third implementation sprint:

```text
1. Add intraday candles.
2. Add VWAP calculation.
3. Add price-confirmation logic.
4. Add sector ETF relative performance.
5. Add ShortScore and LongScore.
6. Add Live Signal Dashboard.
```

Fourth implementation sprint:

```text
1. Add transcript ingestion.
2. Add LLM JSON extraction.
3. Add Call Tone Score.
4. Add red flag / green flag UI.
5. Add model and prompt versioning.
```

Fifth implementation sprint:

```text
1. Add Alpaca paper trading.
2. Add risk limits.
3. Add paper-trade dashboard.
4. Add rejected-trade log.
5. Add performance by signal type.
```

---

### Final recommendation

Build the app around this operating principle:

```text
Before earnings: classify expectation pressure.
After earnings: compare report quality and guidance to expectations.
After price reaction: confirm or reject the setup.
After the signal: freeze it and track the result.
Only after forward validation: paper trade.
```

The app should not chase every earnings event. It should wait for one of four high-quality situations:

```text
1. Clean Miss Short
2. Low-Quality Beat Short
3. Priced-for-Perfection Failure
4. Clean Beat-and-Raise Long
```

The best first product is:

```text
A S&P 500 post-earnings long/short signal generator with backtesting, forward testing, and clear explanations.
```

That gives you a useful research tool first, and a safer trading tool later.