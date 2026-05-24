using System;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace EarningsSignal.Infrastructure.Migrations;

[DbContext(typeof(EarningsSignalDbContext))]
partial class EarningsSignalDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.10");

        modelBuilder.Entity("EarningsSignal.Domain.Entities.Company", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<string>("Industry")
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnType("character varying(150)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnType("character varying(200)");

                b.Property<string>("Sector")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.Property<string>("Ticker")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)");

                b.HasKey("Id");

                b.HasIndex("Ticker")
                    .IsUnique();

                b.ToTable("companies");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.BacktestRun", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<decimal>("AverageReturnPct")
                    .HasPrecision(9, 4)
                    .HasColumnType("numeric(9,4)");

                b.Property<DateTime>("CreatedAtUtc")
                    .HasColumnType("timestamp with time zone");

                b.Property<DateOnly?>("FromDate")
                    .HasColumnType("date");

                b.Property<int>("HoldingDays")
                    .HasColumnType("integer");

                b.Property<string>("StrategyType")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("character varying(80)");

                b.Property<int>("TotalEventsEvaluated")
                    .HasColumnType("integer");

                b.Property<int>("TotalTrades")
                    .HasColumnType("integer");

                b.Property<DateOnly?>("ToDate")
                    .HasColumnType("date");

                b.Property<decimal>("WinRatePct")
                    .HasPrecision(9, 4)
                    .HasColumnType("numeric(9,4)");

                b.Property<int>("WinningTrades")
                    .HasColumnType("integer");

                b.HasKey("Id");

                b.ToTable("backtest_runs");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.BacktestTrade", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("BacktestRunId")
                    .HasColumnType("uuid");

                b.Property<Guid>("CompanyId")
                    .HasColumnType("uuid");

                b.Property<string>("Direction")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)");

                b.Property<Guid>("EarningsEventId")
                    .HasColumnType("uuid");

                b.Property<DateOnly>("EntryDate")
                    .HasColumnType("date");

                b.Property<decimal>("EntryPrice")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<DateOnly>("ExitDate")
                    .HasColumnType("date");

                b.Property<decimal>("ExitPrice")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<string>("Notes")
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasColumnType("character varying(400)");

                b.Property<decimal>("ReturnPct")
                    .HasPrecision(9, 4)
                    .HasColumnType("numeric(9,4)");

                b.Property<string>("SetupType")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("character varying(80)");

                b.Property<string>("Ticker")
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("character varying(10)");

                b.HasKey("Id");

                b.HasIndex("BacktestRunId");

                b.HasIndex("Ticker", "EntryDate");

                b.ToTable("backtest_trades");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.DailyPrice", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<decimal>("Close")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<Guid>("CompanyId")
                    .HasColumnType("uuid");

                b.Property<decimal>("High")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<decimal>("Low")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<decimal>("Open")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<DateOnly>("TradeDate")
                    .HasColumnType("date");

                b.Property<long>("Volume")
                    .HasColumnType("bigint");

                b.HasKey("Id");

                b.HasIndex("CompanyId", "TradeDate")
                    .IsUnique();

                b.ToTable("daily_prices");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsActual", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("EarningsEventId")
                    .HasColumnType("uuid");

                b.Property<decimal>("EpsActual")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<DateTime>("ReportedAtUtc")
                    .HasColumnType("timestamp with time zone");

                b.Property<decimal>("RevenueActual")
                    .HasPrecision(18, 2)
                    .HasColumnType("numeric(18,2)");

                b.HasKey("Id");

                b.HasIndex("EarningsEventId")
                    .IsUnique();

                b.ToTable("earnings_actuals");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsEstimate", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<DateTime>("AsOfUtc")
                    .HasColumnType("timestamp with time zone");

                b.Property<Guid>("EarningsEventId")
                    .HasColumnType("uuid");

                b.Property<decimal>("EpsEstimate")
                    .HasPrecision(18, 4)
                    .HasColumnType("numeric(18,4)");

                b.Property<decimal>("RevenueEstimate")
                    .HasPrecision(18, 2)
                    .HasColumnType("numeric(18,2)");

                b.HasKey("Id");

                b.HasIndex("EarningsEventId")
                    .IsUnique();

                b.ToTable("earnings_estimates");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsEvent", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("CompanyId")
                    .HasColumnType("uuid");

                b.Property<decimal>("ExpectationPressureScore")
                    .HasPrecision(5, 2)
                    .HasColumnType("numeric(5,2)");

                b.Property<string>("PreSignal")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.Property<DateOnly>("ReportDate")
                    .HasColumnType("date");

                b.Property<string>("ReportTime")
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnType("character varying(30)");

                b.HasKey("Id");

                b.HasIndex("CompanyId");

                b.ToTable("earnings_events");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.Signal", b =>
            {
                b.Property<Guid>("Id")
                    .HasColumnType("uuid");

                b.Property<Guid>("CompanyId")
                    .HasColumnType("uuid");

                b.Property<Guid?>("EarningsEventId")
                    .HasColumnType("uuid");

                b.Property<DateTime>("GeneratedAtUtc")
                    .HasColumnType("timestamp with time zone");

                b.Property<bool>("IsLive")
                    .HasColumnType("boolean");

                b.Property<string>("ReasonSummary")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("character varying(500)");

                b.Property<decimal>("Score")
                    .HasPrecision(5, 2)
                    .HasColumnType("numeric(5,2)");

                b.Property<string>("SignalType")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("character varying(50)");

                b.HasKey("Id");

                b.HasIndex("CompanyId");

                b.HasIndex("EarningsEventId");

                b.ToTable("signals");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.DailyPrice", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.Company", "Company")
                    .WithMany("DailyPrices")
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Company");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.BacktestTrade", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.BacktestRun", "BacktestRun")
                    .WithMany("Trades")
                    .HasForeignKey("BacktestRunId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("BacktestRun");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsActual", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.EarningsEvent", "EarningsEvent")
                    .WithOne("Actual")
                    .HasForeignKey("EarningsSignal.Domain.Entities.EarningsActual", "EarningsEventId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("EarningsEvent");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsEstimate", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.EarningsEvent", "EarningsEvent")
                    .WithOne("Estimate")
                    .HasForeignKey("EarningsSignal.Domain.Entities.EarningsEstimate", "EarningsEventId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("EarningsEvent");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.EarningsEvent", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.Company", "Company")
                    .WithMany("EarningsEvents")
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Company");
            });

        modelBuilder.Entity("EarningsSignal.Domain.Entities.Signal", b =>
            {
                b.HasOne("EarningsSignal.Domain.Entities.Company", "Company")
                    .WithMany("Signals")
                    .HasForeignKey("CompanyId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("EarningsSignal.Domain.Entities.EarningsEvent", "EarningsEvent")
                    .WithMany("Signals")
                    .HasForeignKey("EarningsEventId")
                    .OnDelete(DeleteBehavior.SetNull);

                b.Navigation("Company");

                b.Navigation("EarningsEvent");
            });
#pragma warning restore 612, 618
    }
}
