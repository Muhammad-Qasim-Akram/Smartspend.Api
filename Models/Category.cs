namespace Smartspend.Api.Models;

public class Category
{
    public int Id { get; set;}
    public required string Name {get; set;}
    public bool IsDefault{get; set;} 
    public ICollection<Expense> Expenses {get; set;} = [];
    public ICollection<Budget> Budgets {get; set;} = [];

}
