namespace Smartspend.Api.Models;

public class User
{
    public int Id {get; set;}
    public  required string Email {get; set;}
    public required string FirstName {get; set;}
    public string? LastName {get; set;}
    public required string Password {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;

    public ICollection<Expense> Expenses {get; set;} = [];
    public ICollection<Budget> Budgets {get; set;} = [];
   

}
