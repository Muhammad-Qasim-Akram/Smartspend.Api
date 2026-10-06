using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Budgets;
using Smartspend.Api.Models;
using SmartSpend.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Smartspend.Api.Services;

public class BudgetService : IBudgetService
{
    private readonly AppDbContext _dbcontext;

    public BudgetService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<List<BudgetDto>> GetAllAsync(int userId)
    {
        return await _dbcontext.Set<Budget>()
                .Where(b => b.UserId == userId)
                .Select(b => new BudgetDto(b.Id, b.Amount, b.Category!.Name, b.StartDate, b.EndDate))
                .ToListAsync();  
         }

    public async Task<BudgetDto?> GetByIdAsync(int id, int userId)
    {
        return await _dbcontext.Set<Budget>()
                .Where(b => (b.UserId == userId && b.Id == id))
                .Select(b => new BudgetDto(b.Id, b.Amount, b.Category!.Name, b.StartDate, b.EndDate))
                .FirstOrDefaultAsync();
    }

    public async Task<BudgetDto?> CreateAsync(int userId,CreateBudgetDto dto)
    {
        var category = await _dbcontext.Set<Category>()
                        .FirstOrDefaultAsync(c => c.UserId == userId && (c.Id == dto.CategoryId || c.IsDefault));
                        
        if(category is null)return null;

        var budget = new Budget
        {
            Amount = dto.Amount,
            CategoryId = dto.CategoryId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            UserId = userId
        };
        _dbcontext.Set<Budget>().Add(budget);
        await _dbcontext.SaveChangesAsync();

        return new BudgetDto(budget.Id, budget.Amount,category.Name, budget.StartDate, budget.EndDate);
    }  

    public async Task<BudgetDto?> UpdateAsync(int id,int userId,UpdateBudgetDto dto)
    {    
        var oldbudget = await _dbcontext.Set<Budget>()
                          .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId  );
        
        if(oldbudget is null)return null;

        var category = await _dbcontext.Set<Category>()
                         .FirstOrDefaultAsync(c => c.Id == dto.CategoryId && (c.UserId == userId || c.IsDefault));

        if(category is null)return null;

        oldbudget.Amount = dto.Amount;
        oldbudget.CategoryId = dto.CategoryId;
        oldbudget.StartDate = dto.StartDate;
        oldbudget.EndDate = dto.EndDate;

        await _dbcontext.SaveChangesAsync();

        return new BudgetDto(oldbudget.Id, oldbudget.Amount, category.Name, oldbudget.StartDate, oldbudget.EndDate);
    }

    public async Task<bool> DeleteAsync(int id,int userId)
    {
        var row = await _dbcontext.Set<Budget>()
                   .Where(b => b.Id == id && b.UserId == userId)
                   .ExecuteDeleteAsync();
        return (row > 0);
    }
}
