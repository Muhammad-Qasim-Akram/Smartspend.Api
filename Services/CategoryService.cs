using SmartSpend.Api.Interfaces;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Categories;
using Smartspend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Smartspend.Api.Services;

public class CategoryService: ICategoryService
{
    private readonly AppDbContext _dbcontext;
    public CategoryService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;

    }
    
    public async Task<List<CategoryDto>> GetAllAsync(int userId)
    {
        return await _dbcontext.Set<Category>()
                .Where(c => c.UserId == userId || c.IsDefault )
                .Select(c => new CategoryDto(c.Id , c.Name , c.IsDefault))
                .ToListAsync();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id,int userId)
    {
        return await _dbcontext.Set<Category>()
                .Where(c => c.Id == id && (c.UserId == userId || c.IsDefault))
                .Select(c => new CategoryDto(c.Id, c.Name, c.IsDefault))
                .FirstOrDefaultAsync();
    }

    public async Task<
}
