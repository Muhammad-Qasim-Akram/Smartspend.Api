using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Expenses;

public record ExpenseDto
(
    int Id,
    decimal Amount,
    string? Description,
    string CategoryName,
    DateOnly Date
    );
