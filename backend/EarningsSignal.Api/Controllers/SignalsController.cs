using EarningsSignal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EarningsSignal.Api.Controllers;

[ApiController]
[Route("api/signals")]
public class SignalsController(ISignalService signalService) : ControllerBase
{
    [HttpGet("live")]
    public async Task<IActionResult> GetLiveSignals(CancellationToken cancellationToken)
    {
        var liveSignals = await signalService.GetLiveSignalsAsync(cancellationToken);
        return Ok(liveSignals);
    }
}
