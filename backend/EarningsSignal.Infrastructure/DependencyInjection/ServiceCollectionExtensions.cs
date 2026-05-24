using EarningsSignal.Application.Interfaces;
using EarningsSignal.Infrastructure.Data;
using EarningsSignal.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EarningsSignal.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Postgres")
            ?? Environment.GetEnvironmentVariable("EARNINGS_SIGNAL_CONNECTION_STRING")
            ?? "Host=localhost;Port=5432;Database=earnings_signal;Username=earnings_signal;Password=earnings_signal_dev_password";

        services.AddDbContext<EarningsSignalDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ICompanyService, MockCompanyService>();
        services.AddScoped<IEarningsService, MockEarningsService>();
        services.AddScoped<ISignalService, MockSignalService>();

        return services;
    }
}
