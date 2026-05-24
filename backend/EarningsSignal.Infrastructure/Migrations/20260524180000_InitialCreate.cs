using System;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EarningsSignal.Infrastructure.Migrations;

[DbContext(typeof(EarningsSignalDbContext))]
[Migration("20260524180000_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "companies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Sector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Industry = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_companies", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "earnings_events",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                ReportDate = table.Column<DateOnly>(type: "date", nullable: false),
                ReportTime = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                ExpectationPressureScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                PreSignal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_earnings_events", x => x.Id);
                table.ForeignKey(
                    name: "FK_earnings_events_companies_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "daily_prices",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                TradeDate = table.Column<DateOnly>(type: "date", nullable: false),
                Open = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                High = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                Low = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                Close = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                Volume = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_daily_prices", x => x.Id);
                table.ForeignKey(
                    name: "FK_daily_prices_companies_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "earnings_actuals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                EarningsEventId = table.Column<Guid>(type: "uuid", nullable: false),
                EpsActual = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                RevenueActual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                ReportedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_earnings_actuals", x => x.Id);
                table.ForeignKey(
                    name: "FK_earnings_actuals_earnings_events_EarningsEventId",
                    column: x => x.EarningsEventId,
                    principalTable: "earnings_events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "earnings_estimates",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                EarningsEventId = table.Column<Guid>(type: "uuid", nullable: false),
                EpsEstimate = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                RevenueEstimate = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                AsOfUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_earnings_estimates", x => x.Id);
                table.ForeignKey(
                    name: "FK_earnings_estimates_earnings_events_EarningsEventId",
                    column: x => x.EarningsEventId,
                    principalTable: "earnings_events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "signals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                EarningsEventId = table.Column<Guid>(type: "uuid", nullable: true),
                SignalType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Score = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                ReasonSummary = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                GeneratedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                IsLive = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_signals", x => x.Id);
                table.ForeignKey(
                    name: "FK_signals_companies_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_signals_earnings_events_EarningsEventId",
                    column: x => x.EarningsEventId,
                    principalTable: "earnings_events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex(
            name: "IX_companies_Ticker",
            table: "companies",
            column: "Ticker",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_daily_prices_CompanyId_TradeDate",
            table: "daily_prices",
            columns: new[] { "CompanyId", "TradeDate" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_earnings_actuals_EarningsEventId",
            table: "earnings_actuals",
            column: "EarningsEventId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_earnings_estimates_EarningsEventId",
            table: "earnings_estimates",
            column: "EarningsEventId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_earnings_events_CompanyId",
            table: "earnings_events",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_signals_CompanyId",
            table: "signals",
            column: "CompanyId");

        migrationBuilder.CreateIndex(
            name: "IX_signals_EarningsEventId",
            table: "signals",
            column: "EarningsEventId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "daily_prices");

        migrationBuilder.DropTable(
            name: "earnings_actuals");

        migrationBuilder.DropTable(
            name: "earnings_estimates");

        migrationBuilder.DropTable(
            name: "signals");

        migrationBuilder.DropTable(
            name: "earnings_events");

        migrationBuilder.DropTable(
            name: "companies");
    }
}
