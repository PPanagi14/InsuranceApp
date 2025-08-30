using FluentValidation;
using InsuranceApp.Application.Common.Behaviors;
using InsuranceApp.Application.Common.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InsuranceApp.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {

        // Register MediatR handlers
        services.AddMediatR(cfg =>
        {
            cfg.AsScoped();
        }, Assembly.GetExecutingAssembly());

        // Register AutoMapper profiles
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Register all validators automatically
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register the ValidationBehavior pipeline
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
