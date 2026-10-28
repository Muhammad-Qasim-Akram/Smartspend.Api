using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Data;
using Smartspend.Api.Dtos.Expenses;
using Smartspend.Api.Models;
using Smartspend.Api.Services;
using Xunit;

namespace SmartSpend.Tests;

public class ExpenseServiceTests
{
    
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    private static async Task<AppDbContext> SeedAsync(decimal existingExpense, decimal? budgetAmount = null )
{
    var db = CreateContext();
    db.Set<Category>().Add(new Category { Id = 1, Name = "Food", IsDefault = true });
    if(budgetAmount is not null){
    db.Set<Budget>().Add(new Budget
    {
        Amount = budgetAmount.Value, CategoryId = 1, UserId = 1,
        StartDate = new DateOnly(2026, 10, 1), EndDate = new DateOnly(2026, 10, 31)
    }); }
    db.Set<Expense>().Add(new Expense
    {
        Amount = existingExpense, CategoryId = 1, UserId = 1,
        Date = new DateOnly(2026, 10, 10)
    });
    await db.SaveChangesAsync();
    return db;
}

    [Fact]
    public async Task CreateAsync_TotalGoesOverBudget_ReturnsBudgetExceededTrue()
    {
        var db = await SeedAsync(600,1000);
        var service = new ExpenseService(db);

        // ACT: call the method we are testing (600 already spent, adding 500)
        var result = await service.CreateAsync(
            1,
            new CreateExpenseDto(500, "Dinner", 1, new DateOnly(2026, 10, 22)));

        // ASSERT: check the result
        Assert.NotNull(result);
        Assert.True(result.BudgetExceeded);
    }
 
    [Fact]
    public async Task CreateAsync_NoBudget_ReturnsFalse()
    {
        // ARRANGE: put data into the fake database
        var db = await SeedAsync(500);

        var service = new ExpenseService(db);

        // ACT: call the method we are testing (600 already spent, adding 500)
        var result = await service.CreateAsync(
            1,
            new CreateExpenseDto(500, "Dinner", 1, new DateOnly(2026, 10, 22)));

        // ASSERT: check the result
        Assert.NotNull(result);
        Assert.False(result.BudgetExceeded);
    }




}