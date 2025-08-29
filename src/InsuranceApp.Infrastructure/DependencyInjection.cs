using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InsuranceApp.Infrastructure.Persistence;

namespace InsuranceApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Postgres")
                 ?? "Host=localhost;Port=5432;Database=insuranceapp;Username=postgres;Password=postgres";
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

        return services;
    }
}
