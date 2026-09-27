using Microsoft.EntityFrameworkCore;
using Smartspend.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.AddSmartSpendDb();


var app = builder.Build();
app.MigrateDb();
app.Run();


