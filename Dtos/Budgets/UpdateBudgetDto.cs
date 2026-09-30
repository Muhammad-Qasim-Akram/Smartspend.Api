using System.ComponentModel.DataAnnotations;
namespace Smartspend.Api.Dtos.Budgets;

public record UpdateBudgetDto
(
    [Required] decimal Amount,
    DateOnly  StartDate,
    DateOnly EndDate 

);
