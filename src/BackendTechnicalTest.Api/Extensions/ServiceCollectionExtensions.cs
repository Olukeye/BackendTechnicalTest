using BackendTechnicalTest.Application.Interface;
using BackendTechnicalTest.Application.Services;


namespace BackendTechnicalTest.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IScoreCalculatorService, ScoreCalculatorService>();
        services.AddScoped<ICountryService, CountryService>();

        return services;
    }
}
