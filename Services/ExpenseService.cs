using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Expenses;
using SmartSpend.Api.Interfaces;
using Smartspend.Api.Models;

namespace Smartspend.Api.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _dbcontext;

    public ExpenseService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    private async Task<bool> IsBudgetExceededAsync(int userId, int categoryId, DateOnly date)
    {
        var budgets = await _dbcontext.Set<Budget>()
            .Where(b =>
                b.UserId == userId &&
                b.CategoryId == categoryId &&
                b.StartDate <= date &&
                b.EndDate >= date)
            .ToListAsync();

        foreach (var budget in budgets)
        {
            var totalExpenses = await _dbcontext.Set<Expense>()
                .Where(e =>
                    e.UserId == userId &&
                    e.CategoryId == categoryId &&
                    e.Date >= budget.StartDate &&
                    e.Date <= budget.EndDate)
                .SumAsync(e => (decimal?)e.Amount) ?? 0m;

            if (totalExpenses > budget.Amount)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<List<ExpenseDto>> GetAllAsync(int userId)
    {
        return await _dbcontext.Set<Expense>()
            .Where(e => e.UserId == userId)
            .Select(e => new ExpenseDto(e.Id, e.Amount, e.Description, e.Category!.Name, e.Date, false))
            .ToListAsync();
    }

    public async Task<ExpenseDto?> GetByIdAsync(int id, int userId)
    {
        return await _dbcontext.Set<Expense>()
            .Where(e => e.Id == id && e.UserId == userId)
            .Select(e => new ExpenseDto(e.Id, e.Amount, e.Description, e.Category!.Name, e.Date, false))
            .FirstOrDefaultAsync();
    }

    public async Task<ExpenseDto?> CreateAsync(int userId, CreateExpenseDto dto)
    {
        var category = await _dbcontext.Set<Category>()
            .FirstOrDefaultAsync(c => c.Id == dto.CategoryId && (c.UserId == userId || c.IsDefault));

        if (category is null) return null;

        var expense = new Expense
        {
            Amount = dto.Amount,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            Date = dto.Date!.Value,
            UserId = userId
        };

        _dbcontext.Set<Expense>().Add(expense);
        await _dbcontext.SaveChangesAsync();

        var limit = await IsBudgetExceededAsync(userId, dto.CategoryId, expense.Date);

        return new ExpenseDto(expense.Id, expense.Amount, expense.Description, category.Name, expense.Date, limit);
    }

    public async Task<ExpenseDto?> UpdateAsync(int id, int userId, UpdateExpenseDto dto)
    {
        var oldexpense = await _dbcontext.Set<Expense>()
            .FirstOrDefaultAsync(e => e.UserId == userId && e.Id == id);

        if (oldexpense is null) return null;

        var category = await _dbcontext.Set<Category>()
            .FirstOrDefaultAsync(c => c.Id == dto.CategoryId && (c.UserId == userId || c.IsDefault));

        if (category is null) throw new InvalidOperationException("Invalid category.");

        oldexpense.Amount = dto.Amount;
        oldexpense.Description = dto.Description;
        oldexpense.CategoryId = dto.CategoryId;
        oldexpense.Date = dto.Date!.Value;

        await _dbcontext.SaveChangesAsync();

        var limit = await IsBudgetExceededAsync(userId, oldexpense.CategoryId, oldexpense.Date);

        return new ExpenseDto(oldexpense.Id, oldexpense.Amount, oldexpense.Description, category.Name, oldexpense.Date, limit);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var row = await _dbcontext.Set<Expense>()
            .Where(e => e.Id == id && e.UserId == userId)
            .ExecuteDeleteAsync();

        return row > 0;
    }
}