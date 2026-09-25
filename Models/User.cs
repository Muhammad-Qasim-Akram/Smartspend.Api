namespace Smartspend.Api.Models;

public class User
{
    public int Id {get; set;}
    public  string Email {get; set;}
    public required string FirstName {get; set;}
    public string LastName {get; set;}
    public string Password {get; set;}
    public DateTime CreatedAt {get; set;}
   

}
