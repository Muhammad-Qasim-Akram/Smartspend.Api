using Smartspend.Api.Dtos.Expenses;
namespace Smartspend.Api.Interfaces;

public interface IExpenseService
{
    Task<List<ExpenseDto>> GetAllAsync(int userId);
    Task<ExpenseDto?> GetByIdAsync(int id,int userId);

    Task<ExpenseDto> CreateAsync(CreateExpenseDto dto,int userId);
    Task<ExpenseDto>  UpdateAsync(int id, UpdateExpenseDto dto,int userId);
    Task<bool> DeleteAsync(int id,int userId);

}
