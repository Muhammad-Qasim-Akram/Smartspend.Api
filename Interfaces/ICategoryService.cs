using Smartspend.Api.Dtos.Categories;
namespace SmartSpend.Api.Interfaces;

public interface ICategoryService
{
  Task<List<CategoryDto>> GetAllAsync(int id , int userId);
Task<CategoryDto> GetByIdAsync(int id);
Task<CategoryDto> CreateAsync(int userId,CreateCategoryDto dto);
Task<CategoryDto> UpdateAsync(int userId,UpdateCategoryDto dto);
Task<bool> DeleteAsync (int id,int userId);






}
