namespace Smartspend.Api.Dtos.Reports;

public record WeeklyReportDto
(
   decimal Amount,
   DateOnly StartDate,
   DateOnly EndDate,
   string AtCategory,
   string Expenses

);
