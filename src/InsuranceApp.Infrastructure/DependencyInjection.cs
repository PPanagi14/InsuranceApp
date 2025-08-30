using InsuranceApp.Application.Common.Interfaces;
using InsuranceApp.Infrastructure.Auth;
using InsuranceApp.Infrastructure.Persistence;
using InsuranceApp.Infrastructure.Repositories;
using InsuranceApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Postgres")
                 ?? "Host=localhost;Port=5432;Database=insuranceapp;Username=postgres;Password=postgres";
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(cs));

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IPolicyRepository, PolicyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();


        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        return services;
    }
}
