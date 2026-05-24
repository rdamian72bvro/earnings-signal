using EarningsSignal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EarningsSignal.Api.Controllers;

[ApiController]
[Route("api/earnings")]
public class EarningsController(IEarningsService earningsService) : ControllerBase
{
    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcomingEarnings(CancellationToken cancellationToken)
    {
        var upcoming = await earningsService.GetUpcomingEarningsAsync(cancellationToken);
        return Ok(upcoming);
    }
}
