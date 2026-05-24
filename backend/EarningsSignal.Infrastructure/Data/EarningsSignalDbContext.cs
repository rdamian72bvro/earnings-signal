using EarningsSignal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Data;

public class EarningsSignalDbContext(DbContextOptions<EarningsSignalDbContext> options) : DbContext(options)
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<EarningsEvent> EarningsEvents => Set<EarningsEvent>();
    public DbSet<EarningsEstimate> EarningsEstimates => Set<EarningsEstimate>();
    public DbSet<EarningsActual> EarningsActuals => Set<EarningsActual>();
    public DbSet<DailyPrice> DailyPrices => Set<DailyPrice>();
    public DbSet<Signal> Signals => Set<Signal>();
    public DbSet<BacktestRun> BacktestRuns => Set<BacktestRun>();
    public DbSet<BacktestTrade> BacktestTrades => Set<BacktestTrade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("companies");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Ticker).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Sector).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Industry).HasMaxLength(150).IsRequired();
            entity.HasIndex(x => x.Ticker).IsUnique();
        });

        modelBuilder.Entity<EarningsEvent>(entity =>
        {
            entity.ToTable("earnings_events");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ReportTime).HasMaxLength(30).IsRequired();
            entity.Property(x => x.PreSignal).HasMaxLength(50).IsRequired();
            entity.Property(x => x.ExpectationPressureScore).HasPrecision(5, 2);

            entity.HasOne(x => x.Company)
                .WithMany(x => x.EarningsEvents)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EarningsEstimate>(entity =>
        {
            entity.ToTable("earnings_estimates");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.EpsEstimate).HasPrecision(18, 4);
            entity.Property(x => x.RevenueEstimate).HasPrecision(18, 2);

            entity.HasOne(x => x.EarningsEvent)
                .WithOne(x => x.Estimate)
                .HasForeignKey<EarningsEstimate>(x => x.EarningsEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EarningsActual>(entity =>
        {
            entity.ToTable("earnings_actuals");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.EpsActual).HasPrecision(18, 4);
            entity.Property(x => x.RevenueActual).HasPrecision(18, 2);

            entity.HasOne(x => x.EarningsEvent)
                .WithOne(x => x.Actual)
                .HasForeignKey<EarningsActual>(x => x.EarningsEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DailyPrice>(entity =>
        {
            entity.ToTable("daily_prices");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Open).HasPrecision(18, 4);
            entity.Property(x => x.High).HasPrecision(18, 4);
            entity.Property(x => x.Low).HasPrecision(18, 4);
            entity.Property(x => x.Close).HasPrecision(18, 4);

            entity.HasOne(x => x.Company)
                .WithMany(x => x.DailyPrices)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(x => new { x.CompanyId, x.TradeDate }).IsUnique();
        });

        modelBuilder.Entity<Signal>(entity =>
        {
            entity.ToTable("signals");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.SignalType).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Score).HasPrecision(5, 2);
            entity.Property(x => x.ReasonSummary).HasMaxLength(500).IsRequired();

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Signals)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.EarningsEvent)
                .WithMany(x => x.Signals)
                .HasForeignKey(x => x.EarningsEventId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<BacktestRun>(entity =>
        {
            entity.ToTable("backtest_runs");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.StrategyType).HasMaxLength(80).IsRequired();
            entity.Property(x => x.WinRatePct).HasPrecision(9, 4);
            entity.Property(x => x.AverageReturnPct).HasPrecision(9, 4);
            entity.Property(x => x.CreatedAtUtc).IsRequired();

            entity.HasMany(x => x.Trades)
                .WithOne(x => x.BacktestRun)
                .HasForeignKey(x => x.BacktestRunId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BacktestTrade>(entity =>
        {
            entity.ToTable("backtest_trades");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Ticker).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Direction).HasMaxLength(10).IsRequired();
            entity.Property(x => x.SetupType).HasMaxLength(80).IsRequired();
            entity.Property(x => x.EntryPrice).HasPrecision(18, 4);
            entity.Property(x => x.ExitPrice).HasPrecision(18, 4);
            entity.Property(x => x.ReturnPct).HasPrecision(9, 4);
            entity.Property(x => x.Notes).HasMaxLength(400).IsRequired();

            entity.HasIndex(x => x.BacktestRunId);
            entity.HasIndex(x => new { x.Ticker, x.EntryDate });
        });
    }
}
