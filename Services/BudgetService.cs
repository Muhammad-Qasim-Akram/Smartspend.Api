using Smartspend.Api.Data;
using SmartSpend.Api.Interfaces;

namespace Smartspend.Api.Services;

public class BudgetService : IBudgetService
{
    private readonly AppDbContext _dbcontext;

    public BudgetService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    
}
