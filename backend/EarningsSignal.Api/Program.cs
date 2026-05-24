using EarningsSignal.Infrastructure.DependencyInjection;
using EarningsSignal.Infrastructure.Data;
using EarningsSignal.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<EarningsSignalDbContext>();

    if (dbContext.Database.IsRelational())
    {
        await dbContext.Database.MigrateAsync();
    }

    await DatabaseSeeder.SeedAsync(dbContext);
}

app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program;
