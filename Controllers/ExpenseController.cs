using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Expenses;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[ApiController]
[Route("api/[controller")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _expenseService.GetAllAsync(userId);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
