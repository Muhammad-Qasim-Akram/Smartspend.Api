namespace Smartspend.Api.Dtos.Reports;

public record ReportDto
(
   decimal TotalSpent,
   DateOnly StartDate,
   DateOnly EndDate,
   List<CategorySpendingDto> CategoryBreakdown
);