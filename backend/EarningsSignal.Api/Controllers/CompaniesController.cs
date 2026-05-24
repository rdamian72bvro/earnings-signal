using EarningsSignal.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EarningsSignal.Api.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
    {
        var companies = await companyService.GetCompaniesAsync(cancellationToken);
        return Ok(companies);
    }
}
