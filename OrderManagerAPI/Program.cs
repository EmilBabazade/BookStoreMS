using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration["DBConnectionString"]));

var app = builder.Build();

DataSeeder.Seed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

