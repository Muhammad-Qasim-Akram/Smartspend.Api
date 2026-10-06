using Smartspend.Api.Dtos.Budgets;
namespace SmartSpend.Api.Interfaces;
public interface IBudgetService
{
    Task<List<BudgetDto>> GetAllAsync(int userId);
    Task<BudgetDto?> GetByIdAsync(int id,int userId);

    Task<BudgetDto?> CreateAsync(int userId, CreateBudgetDto dto);
    Task<BudgetDto?>  UpdateAsync(int id,int userId, UpdateBudgetDto dto);
    Task<bool> DeleteAsync(int id,int userId);
}