using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Budgets;

public record CreateBudgetDto
(
    [Required][Range(1,1000000000)] decimal Amount,
    [Required] int CategoryId,
    [Required] DateOnly  StartDate,
    [Required] DateOnly EndDate

);
