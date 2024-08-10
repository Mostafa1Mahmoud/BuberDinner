using Microsoft.Extensions.DependencyInjection;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Application.Common.Interfaces.Persistence;
namespace BuberDinner.Application;

public static class DependencyInjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}