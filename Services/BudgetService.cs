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
                .Select(b => new BudgetDto(b.Id, b.Amount, b.Category.Name, b.StartDate, b.EndDate))
                .ToListAsync();  
         }

    public async Task<BudgetDto?> GetByIdAsync(int id, int userId)
    {
        
    }   
}
