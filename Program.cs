using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString)
}
);

var app = builder.Build();
app.Run();


