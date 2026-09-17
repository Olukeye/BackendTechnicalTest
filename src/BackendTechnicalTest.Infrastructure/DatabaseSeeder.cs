using BackendTechnicalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace BackendTechnicalTest.Infrastructure;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        AppDbContext context,
        CancellationToken cancellationToken = default)
    {
        if (await context.Countries.AnyAsync(cancellationToken))
        {
            return;
        }

        var countries = new[]
        {
            new Country(1, "234", "Nigeria", "NG"),
            new Country(2, "233", "Ghana", "GH"),
            new Country(3, "229", "Benin Republic", "BN"),
            new Country(4, "225", "Côte d'Ivoire", "CIV")
        };

        var countryDetails = new[]
        {
            new CountryDetail(1, 1, "MTN Nigeria", "MTN NG"),
            new CountryDetail(2, 1, "Airtel Nigeria", "ANG"),
            new CountryDetail(3, 1, "9 Mobile Nigeria", "ETN"),
            new CountryDetail(4, 1, "Globacom Nigeria", "GLO NG"),

            new CountryDetail(5, 2, "Vodafone Ghana", "Vodafone GH"),
            new CountryDetail(6, 2, "MTN Ghana", "MTN Ghana"),
            new CountryDetail(7, 2, "Tigo Ghana", "Tigo Ghana"),

            new CountryDetail(8, 3, "MTN Benin", "MTN Benin"),
            new CountryDetail(9, 3, "Moov Benin", "Moov Benin"),

            new CountryDetail(10, 4, "MTN Côte d'Ivoire", "MTN CIV")
        };

        await context.Countries.AddRangeAsync(
            countries,
            cancellationToken);

        await context.CountryDetails.AddRangeAsync(
            countryDetails,
            cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}
