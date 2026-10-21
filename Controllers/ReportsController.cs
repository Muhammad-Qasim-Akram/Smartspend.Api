using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Reports;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet] 
    public async Task<ActionResult<ReportDto>> Reports([FromQuery] ReportRequestDto dto)
    {
        if (dto.EndDate < dto.StartDate)
        return BadRequest("End date must be on or after start date.");
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _reportService.GetReportAsync(userId,dto);
        return Ok(result);
    }
}
