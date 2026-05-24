using EarningsSignal.Application.DTOs;
using EarningsSignal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EarningsSignal.Api.Controllers;

[ApiController]
[Route("api/backtests")]
public class BacktestsController(IBacktestService backtestService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBacktests(CancellationToken cancellationToken)
    {
        var runs = await backtestService.GetBacktestRunsAsync(cancellationToken);
        return Ok(runs);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetBacktest(Guid id, CancellationToken cancellationToken)
    {
        var run = await backtestService.GetBacktestRunAsync(id, cancellationToken);
        if (run is null)
        {
            return NotFound();
        }

        return Ok(run);
    }

    [HttpGet("{id:guid}/trades")]
    public async Task<IActionResult> GetBacktestTrades(Guid id, CancellationToken cancellationToken)
    {
        var run = await backtestService.GetBacktestRunAsync(id, cancellationToken);
        if (run is null)
        {
            return NotFound();
        }

        var trades = await backtestService.GetBacktestTradesAsync(id, cancellationToken);
        return Ok(trades);
    }

    [HttpPost("run")]
    public async Task<IActionResult> RunBacktest(
        [FromBody] BacktestRunRequestDto? request,
        CancellationToken cancellationToken)
    {
        var effectiveRequest = request ?? new BacktestRunRequestDto(
            StrategyType: "CleanMissShort",
            HoldingDays: 3,
            FromDate: null,
            ToDate: null,
            MinReactionPct: -2m);

        try
        {
            var result = await backtestService.RunBacktestAsync(effectiveRequest, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }
}
