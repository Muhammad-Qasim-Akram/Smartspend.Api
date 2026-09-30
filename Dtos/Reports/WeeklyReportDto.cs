namespace Smartspend.Api.Dtos.Reports;

public record WeeklyReportDto
(
   decimal Amount,
   DateOnly StartDate,
   DateOnly EndDate,
   List<CategorySpendingDto> CategoryBreakdown
);
