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

    [Fact]
    public async Task CreateAsync_TotalGoesOverBudget_ReturnsBudgetExceededTrue()
    {
        // ARRANGE: put data into the fake database
        var db = CreateContext();

        db.Set<Category>().Add(new Category
        {
            Id = 1, Name = "Food", IsDefault = true
        });

        db.Set<Budget>().Add(new Budget
        {
            Amount = 1000,
            CategoryId = 1,
            UserId = 1,
            StartDate = new DateOnly(2026, 10, 1),
            EndDate = new DateOnly(2026, 10, 31)
        });

        db.Set<Expense>().Add(new Expense
        {
            Amount = 600,
            CategoryId = 1,
            UserId = 1,
            Date = new DateOnly(2026, 10, 10)
        });

        await db.SaveChangesAsync();

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
        var db = CreateContext();

        db.Set<Category>().Add(new Category
        {
            Id = 1, Name = "Food", IsDefault = true
        });

    
        db.Set<Expense>().Add(new Expense
        {
            Amount = 600,
            CategoryId = 1,
            UserId = 1,
            Date = new DateOnly(2026, 10, 10)
        });

        await db.SaveChangesAsync();

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