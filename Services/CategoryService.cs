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

    public async Task<CategoryDto> CreateAsync(int userId, CreateCategoryDto dto)
    {
         var category = new Category
        {
            Name = dto.Name,
            UserId = userId,
            IsDefault = false
        };
        _dbcontext.Set<Category>().Add(category);
        await _dbcontext.SaveChangesAsync();

        return new CategoryDto(category.Id, category.Name, category.IsDefault);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, int userId, UpdateCategoryDto dto)
    {
        var oldcategory = await _dbcontext.Set<Category>().FirstOrDefaultAsync(c => (c.UserId == userId && c.Id == id));
        
        if(oldcategory is null) return null;
        oldcategory.Name = dto.Name;
        await _dbcontext.SaveChangesAsync();

        return new CategoryDto(oldcategory.Id, oldcategory.Name, oldcategory.IsDefault);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {   
        var row = await _dbcontext.Set<Category>().Where(c => (c.Id == id || c.UserId == userId)).ExecuteDeleteAsync();
        return row > 0;
    }
}
