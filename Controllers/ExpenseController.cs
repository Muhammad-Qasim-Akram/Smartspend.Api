using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Expenses;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }
    
    private int  GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetAllExpenses()
    {
            var userId = GetUserId();
            var result = await _expenseService.GetAllAsync(userId);
            return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseDto>> GetById(int id)
    {
            var userId = GetUserId();
            var result = await _expenseService.GetByIdAsync(id, userId);
            if(result is null) return NotFound();
            return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense(CreateExpenseDto dto)
    {
            var userId =  GetUserId();
            var result =  await _expenseService.CreateAsync(userId,dto);
            if(result is null) return BadRequest("Invalid Category");
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);    
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ExpenseDto>> UpdateExpense(int id,UpdateExpenseDto dto)
    {
          var userId = GetUserId();
          var result = await _expenseService.UpdateAsync(id,userId,dto);
          if(result is null) return NotFound();
          return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteExpense(int id)
    {
            var userId = GetUserId();
            var result = await _expenseService.DeleteAsync(id,userId);
            if(!result) return NotFound();
            return NoContent();
    }

}
