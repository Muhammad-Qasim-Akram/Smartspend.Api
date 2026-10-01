public record ReportRequestDto(
    [Required] DateOnly StartDate,
    [Required] DateOnly EndDate
);