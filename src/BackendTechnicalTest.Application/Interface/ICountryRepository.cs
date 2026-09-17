using BackendTechnicalTest.Domain.Entities;
namespace BackendTechnicalTest.Application.Interface;

public interface ICountryRepository
{
    Task <Country?> GetCountryCode(string countryCode, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<string>> GetCountryCodes(
        CancellationToken cancellationToken = default);
}
