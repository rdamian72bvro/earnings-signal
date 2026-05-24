using System;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EarningsSignal.Infrastructure.Migrations;

[DbContext(typeof(EarningsSignalDbContext))]
[Migration("20260525090000_AddBacktests")]
public partial class AddBacktests : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "backtest_runs",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                StrategyType = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                HoldingDays = table.Column<int>(type: "integer", nullable: false),
                FromDate = table.Column<DateOnly>(type: "date", nullable: true),
                ToDate = table.Column<DateOnly>(type: "date", nullable: true),
                TotalEventsEvaluated = table.Column<int>(type: "integer", nullable: false),
                TotalTrades = table.Column<int>(type: "integer", nullable: false),
                WinningTrades = table.Column<int>(type: "integer", nullable: false),
                WinRatePct = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                AverageReturnPct = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_backtest_runs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "backtest_trades",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BacktestRunId = table.Column<Guid>(type: "uuid", nullable: false),
                EarningsEventId = table.Column<Guid>(type: "uuid", nullable: false),
                CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                Direction = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                SetupType = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                EntryDate = table.Column<DateOnly>(type: "date", nullable: false),
                EntryPrice = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                ExitDate = table.Column<DateOnly>(type: "date", nullable: false),
                ExitPrice = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                ReturnPct = table.Column<decimal>(type: "numeric(9,4)", precision: 9, scale: 4, nullable: false),
                Notes = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_backtest_trades", x => x.Id);
                table.ForeignKey(
                    name: "FK_backtest_trades_backtest_runs_BacktestRunId",
                    column: x => x.BacktestRunId,
                    principalTable: "backtest_runs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_backtest_trades_BacktestRunId",
            table: "backtest_trades",
            column: "BacktestRunId");

        migrationBuilder.CreateIndex(
            name: "IX_backtest_trades_Ticker_EntryDate",
            table: "backtest_trades",
            columns: new[] { "Ticker", "EntryDate" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "backtest_trades");

        migrationBuilder.DropTable(
            name: "backtest_runs");
    }
}
