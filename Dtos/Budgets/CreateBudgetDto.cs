using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Budgets;

public record CreateBudgetDto
(
    [Required] decimal Amount,
    DateOnly  StartDate,
    DateOnly EndDate 
);
