using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Reports;
using Smartspend.Api.Dtos.Expenses;
using Smartspend.Api.Models;
using SmartSpend.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Smartspend.Api.Services;

public class ReportService : IReportService
{
    private readonly AppDbContext _dbcontext;
    public ReportService(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public async Task<ReportDto> GetReportAsync(int userId, ReportRequestDto dto)
    {
       var expenses = await _dbcontext.Set<Expense>()
                        .Where(e => e.UserId == userId &&
                               e.Date >= dto.StartDate &&
                               e.Date <= dto.EndDate)
                               .Include(e => e.Category)
                               .ToListAsync();
        
        var categoryBreakdown = expenses
                               .GroupBy(e => e.Category!.Name)
                               .Select(g => new CategorySpendingDto(
                                g.Key,
                                g.Sum(e => e.Amount),
                                g.Count()
                               ))
                               .ToList();

        var totalSpending = expenses.Sum(e => e.Amount);

       return new ReportDto(totalSpending, dto.StartDate, dto.EndDate, categoryBreakdown );    
    }
}
