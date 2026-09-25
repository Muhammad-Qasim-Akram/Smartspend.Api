namespace Smartspend.Api.Models;

public class Budget
{
    public int Id {get; set;}
    public required double Amount {get; set;}

    public int  Day {get; set;}
    public int  Month {get; set;}
    public int  Year {get; set;}
    public int UserId {get; set;}
}
