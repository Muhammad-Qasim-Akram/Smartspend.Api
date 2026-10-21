using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Budgets;
using Smartspend.Api.Dtos.Expenses;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetsController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }  

    private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); 

    [HttpGet]
    public async Task<ActionResult<List<BudgetDto>>> GetAllBudget()
    {
        var userId = GetUserId();
        var result = await _budgetService.GetAllAsync(userId);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetDto>> GetById(int id)
    {
        var userId = GetUserId();
        var result = await _budgetService.GetByIdAsync(id,userId);
        if(result is null) return NotFound();
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<BudgetDto>> CreateExpense(CreateBudgetDto dto)
    {
        var userId = GetUserId();
        var result = await _budgetService.CreateAsync(userId,dto);
        if(result is null) return BadRequest("Invalid Budget");
        return CreatedAtAction(nameof(GetById),new {id = result.Id},result );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BudgetDto>> UpdateCategory(int id,UpdateBudgetDto dto)
    {
        var userId = GetUserId();
        var result = await _budgetService.UpdateAsync(id,userId,dto);
        if(result is null) return NotFound();
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBudget(int id)
    {
        var userId = GetUserId();
        var result = await _budgetService.DeleteAsync(id,userId);
        if(!result) return NotFound();
        return NoContent();
    }
}
