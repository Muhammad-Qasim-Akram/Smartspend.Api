using Smartspend.Api.Dtos.Budgets;
namespace SmartSpend.Api.Interfaces;
public interface IBudgetService
{
    Task<List<BudgetDto>> GetAllAsync(int userId);
    Task<BudgetDto?> GetByIdAsync(int id,int userId);

    Task<BudgetDto> CreateAsync(CreateBudgetDto dto,int userId);
    Task<BudgetDto>  UpdateAsync(int id, UpdateBudgetDto dto,int userId);
    Task<bool> DeleteAsync(int id,int userId);
}