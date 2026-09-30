using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Expenses;

public record UpdateExpenseDto
(
    [Required][Range(1,1000000000)] decimal Amount,
    string? Description,
    [Required] int CategoryId,
    [Required] DateOnly Date
);