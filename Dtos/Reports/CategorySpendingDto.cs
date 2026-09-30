namespace Smartspend.Api.Dtos.Reports;

public record CategorySpendingDto(
    string CategoryName,
    decimal TotalSpending,
    int TotalExpenses
);
