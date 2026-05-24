using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Services;

public class DbEarningsService(EarningsSignalDbContext dbContext) : IEarningsService
{
    public async Task<IReadOnlyList<UpcomingEarningsDto>> GetUpcomingEarningsAsync(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        return await dbContext.EarningsEvents
            .AsNoTracking()
            .Include(earningsEvent => earningsEvent.Company)
            .Where(earningsEvent => earningsEvent.ReportDate >= today)
            .OrderBy(earningsEvent => earningsEvent.ReportDate)
            .ThenBy(earningsEvent => earningsEvent.Company!.Ticker)
            .Select(earningsEvent => new UpcomingEarningsDto(
                earningsEvent.Company!.Ticker,
                earningsEvent.Company.Name,
                earningsEvent.ReportDate,
                earningsEvent.ReportTime,
                earningsEvent.Company.Sector,
                earningsEvent.ExpectationPressureScore,
                earningsEvent.PreSignal))
            .ToListAsync(cancellationToken);
    }
}
