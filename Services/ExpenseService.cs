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
          return await _dbcontext.Set<Expense>()
                .Where(e => (e.Id == id && e.UserId == userId))
                .Select(e => new ExpenseDto(e.Id, e.Amount, e.Description, e.Category.Name, e.Date))
                .FirstOrDefaultAsync();
    }

    public async Task<ExpenseDto?> CreateAsync(int userId, CreateExpenseDto dto)
    {
        var category = await _dbcontext.Set<Category>()
                  .FirstOrDefaultAsync(c => (c.Id == dto.CategoryId && (c.UserId == userId || c.IsDefault)));
        if(category is null) return null;

        var expense = new Expense
        {
            Amount = dto.Amount,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            Date = dto.Date,
            UserId = userId
        };

        _dbcontext.Set<Expense>().Add(expense);
        await _dbcontext.SaveChangesAsync();

        return new ExpenseDto(expense.Id, expense.Amount, expense.Description, category!.Name, expense.Date);
    } 

    public async Task<ExpenseDto?> UpdateAsync(int id, int userId ,UpdateExpenseDto dto)
    {
        var oldexpense = await _dbcontext.Set<Expense>().FirstOrDefaultAsync(e => (e.UserId == userId && e.Id == id));

        if(oldexpense is null) return null;
        oldexpense.Amount = dto.Amount;
        oldexpense.Description = dto.Description;
        oldexpense.CategoryId  = dto.CategoryId;
        oldexpense.Date = dto.Date;
        await _dbcontext.SaveChangesAsync();

        var category = await _dbcontext.Set<Category>()
                       .FirstOrDefaultAsync(c => (c.Id == oldexpense.CategoryId && (c.UserId == userId || c.IsDefault)));

        return new ExpenseDto(oldexpense.Id, oldexpense.Amount, oldexpense.Description, category!.Name, oldexpense.Date );
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var row = await _dbcontext.Set<Expense>()
                .Where(e => (e.Id == id && e.UserId == userId))
                .ExecuteDeleteAsync();
        
        return (row >0);
    }

}

