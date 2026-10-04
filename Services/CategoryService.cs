using SmartSpend.Api.Interfaces;
using Smartspend.Api.Data;

namespace Smartspend.Api.Services;

public class CategoryService: ICategoryService
{
    private readonly AppDbContext _dbcontext;
    public CategoryService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    
}
