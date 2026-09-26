namespace Smartspend.Api.Models;

public class Budget
{
    public int Id {get; set;}
    public required decimal Amount {get; set;}

    public DateOnly  StartDate {get; set;}
    public DateOnly EndDate {get; set;}
    public int? UserId {get; set;}
    public int? CategoryId {get; set;}
    public Category? Category{get; set;}
    public User? User {get; set;}
}
