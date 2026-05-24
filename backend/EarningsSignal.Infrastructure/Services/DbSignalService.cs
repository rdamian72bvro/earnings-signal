using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EarningsSignal.Infrastructure.Services;

public class DbSignalService(EarningsSignalDbContext dbContext) : ISignalService
{
    public async Task<IReadOnlyList<LiveSignalDto>> GetLiveSignalsAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Signals
            .AsNoTracking()
            .Include(signal => signal.Company)
            .Where(signal => signal.IsLive)
            .OrderByDescending(signal => signal.GeneratedAtUtc)
            .Select(signal => new LiveSignalDto(
                signal.Company!.Ticker,
                signal.Company.Name,
                signal.Company.Sector,
                signal.SignalType,
                signal.Score,
                signal.ReasonSummary,
                signal.GeneratedAtUtc))
            .ToListAsync(cancellationToken);
    }
}
