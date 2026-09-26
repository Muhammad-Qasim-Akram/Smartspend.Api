namespace Smartspend.Api.Models;

public class Expense
{
    public int Id {get; set;}
    public double Amount {get; set;}
    public string? Description {get; set;}
    public DateOnly Date {get; set;}
    public int? CategoryId {get; set;}
    public Category? Category {get; set;}
    public int? UserId {get; set;}
    public User? User {get; set;}
}
