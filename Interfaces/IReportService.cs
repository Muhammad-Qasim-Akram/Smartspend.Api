using Smartspend.Api.Dtos.Reports;
namespace SmartSpend.Api.Interfaces;
public interface IReportService
{
    Task<ReportDto> GetReportAsync(ReportRequestDto dto, int userId);
}