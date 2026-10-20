using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Smartspend.Api.Dtos.Expenses;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet/{id}]
    public async Task<ActionResult<ExpenseDto>> GetById(int id)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _expenseService.GetByIdAsync(id, userId);
            return Ok(result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense(CreateExpenseDto dto)
    {
        try
        {
            var userId =  int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result =  await _expenseService.CreateAsync(userId,dto);
            return Ok(result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }    
    }
    
    [HttpPut]
    public async Task<ActionResult<ExpenseDto>> UpdateExpense(int id,UpdateExpenseDto dto)
    { 
        try
        {
          var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
          var result = await _expenseService.UpdateAsync(id,userId,dto);
          return Ok(result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }

    }
    
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteExpense(int id)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _expenseService.DeleteAsync(id,userId);
            return Ok(result);
        }
        catch(InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

   

}
