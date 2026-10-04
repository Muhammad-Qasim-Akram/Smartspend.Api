using Smartspend.Api.Dtos.Categories;
namespace SmartSpend.Api.Interfaces;

public interface ICategoryService
{
Task<List<CategoryDto>> GetAllAsync(int userId);
Task<CategoryDto?> GetByIdAsync(int id , int userId);
Task<CategoryDto> CreateAsync(int userId,CreateCategoryDto dto);
Task<CategoryDto?> UpdateAsync(int userId,UpdateCategoryDto dto,int id);
Task<bool> DeleteAsync (int id,int userId);
}
