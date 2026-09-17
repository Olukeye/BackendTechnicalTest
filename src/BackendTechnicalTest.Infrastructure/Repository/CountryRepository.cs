using BackendTechnicalTest.Application.Interface;
using BackendTechnicalTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Infrastructure.Repository;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Country?> GetCountryCode(string countryCode, CancellationToken cancellationToken = default)
    {
        return await _context.Countries.AsNoTracking()
            .Include(x => x.CountryDetails)
            .FirstOrDefaultAsync(x => x.CountryCode == countryCode, cancellationToken);
    }

    public async Task<IReadOnlyCollection<string>> GetCountryCodes(
       CancellationToken cancellationToken = default)
    {
        return await _context.Countries
            .AsNoTracking()
            .Select(country => country.CountryCode)
            .ToListAsync(cancellationToken);
    }
}
