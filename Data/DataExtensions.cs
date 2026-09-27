using Smartspend.Api.Data;
using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Models;

namespace Smartspend.Api.Data;
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    
    }

    public static void AddSmartSpendDb(this WebApplicationBuilder builder)
    {
       var connString = builder.Configuration.GetConnectionString("SmartSpendDb");
         builder.Services.AddNpgsql<AppDbContext>(connString,
             optionsAction: options => options.UseSeeding((context, _) =>
             {
                 if(!context.Set<Category>().Any())
                 {
                    context.Set<Category>().AddRange(
                        new Category { Name = "Food" , IsDefault = true },
                        new Category { Name = "Transportation", IsDefault = true },
                        new Category { Name = "Entertainment", IsDefault = true },
                        new Category { Name = "Utilities",IsDefault = true },
                        new Category { Name = "Healthcare",IsDefault = true  },
                        new Category { Name = "Education" , IsDefault = true}
                    );
                    context.SaveChanges();
                 };
                
             }

        
         )
         );
    }


    
}
