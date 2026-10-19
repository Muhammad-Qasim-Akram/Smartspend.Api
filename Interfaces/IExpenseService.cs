using Smartspend.Api.Dtos.Expenses;
namespace SmartSpend.Api.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllAsync(int userId);
    Task<ExpenseDto?> GetByIdAsync(int id, int userId);

    Task<ExpenseDto?> CreateAsync(int userId, CreateExpenseDto dto);
    Task<ExpenseDto?>  UpdateAsync(int id, int userId, UpdateExpenseDto dto);
    Task<bool> DeleteAsync(int id, int userId);

}
