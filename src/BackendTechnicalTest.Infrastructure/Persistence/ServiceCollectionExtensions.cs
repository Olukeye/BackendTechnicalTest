using BackendTechnicalTest.Application.Interface;
using BackendTechnicalTest.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTechnicalTest.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("BackendTechnicalTestDb");
        });

        services.AddScoped<ICountryRepository, CountryRepository>();

        return services;
    }
}
