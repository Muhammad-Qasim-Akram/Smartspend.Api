using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Expenses;
using Smartspend.Api.Interfaces;
using Smartspend.Api.Models;

namespace Smartspend.Api.Services;
public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _dbcontext;
    public ExpenseService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<List<ExpenseDto>> GetAllAsync(int userId)
    {
        return await _dbcontext.Set<Expense>()
                    .Where(e => e.UserId == userId)
                    .Select(e => new ExpenseDto(e.Id, e.Amount, e.Description, e.Category.Name, e.Date))
                    .ToListAsync();
    }

    public async Task<ExpenseDto?> GetByIdAsync(int id, int userId)
    {
        
    }
}
