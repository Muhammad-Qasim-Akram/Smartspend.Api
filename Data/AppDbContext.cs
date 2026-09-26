using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Models;

namespace Smartspend.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users => Set<User>();
  public DbSet<Category> Categories => Set<Category>();
  public DbSet<Expense> Expenses => Set<Expense>();
  public DbSet<Budget> Budgets => Set<Budget>();
 
}