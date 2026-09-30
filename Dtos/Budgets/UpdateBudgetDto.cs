using System.ComponentModel.DataAnnotations;
namespace Smartspend.Api.Dtos.Budgets;

public record UpdateBudgetDto
(
    [Required][Range(1,100000000)] decimal Amount,
    [Required] int CategoryId,
    [Required] DateOnly  StartDate,
    [Required] DateOnly EndDate 

);
