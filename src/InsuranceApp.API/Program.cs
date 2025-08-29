using InsuranceApp.Infrastructure;
using InsuranceApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/db-check", async (AppDbContext db) =>
{
    try
    {
        var can = await db.Database.CanConnectAsync();
        return Results.Ok(new { connected = can });
    }
    catch (Exception ex)
    {
        return Results.Problem($"DB connection failed: {ex.Message}");
    }
});
app.MapControllers();

app.Run();

// allow integration tests to find Program
public partial class Program { }
