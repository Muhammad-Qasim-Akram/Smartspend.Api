namespace Smartspend.Api.Dtos.Budgets;

public record BudgetDto
(
    int Id,
    decimal Amount,
    string CategoryName,
    DateOnly StartDate,
    DateOnly EndDate
);
