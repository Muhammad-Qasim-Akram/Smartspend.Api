using System.ComponentModel.DataAnnotations;

namespace Smartspend.Api.Dtos.Reports;
public record ReportRequestDto(
    [Required] DateOnly StartDate,
    [Required] DateOnly EndDate
);