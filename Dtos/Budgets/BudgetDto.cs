namespace Smartspend.Api.Dtos.Budgets;

public record BudgetDto
(
    int Id,
    decimal Amount,
    DateOnly StartDate,
    DateOnly EndDate
);
