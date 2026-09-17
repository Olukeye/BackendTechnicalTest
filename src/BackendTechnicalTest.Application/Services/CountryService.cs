using BackendTechnicalTest.Application.Common.Results;
using BackendTechnicalTest.Application.DTOs;
using BackendTechnicalTest.Application.Interface;



namespace BackendTechnicalTest.Application.Services;

public sealed class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Result<PhoneCountryLookupResponse>> GetCountryByPhoneNumber(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Result<PhoneCountryLookupResponse>.Failure(
                CountryErrors.PhoneNumberRequired);
        }

        var normalizedNumber = NormalizePhoneNumber(phoneNumber);

        if (normalizedNumber is null)
        {
            return Result<PhoneCountryLookupResponse>.Failure(
                CountryErrors.InvalidPhoneNumber);
        }

        var countryCode = await FindCountryCode(
            normalizedNumber,
            cancellationToken);

        if (countryCode is null)
        {
            return Result<PhoneCountryLookupResponse>.Failure(
                CountryErrors.UnsupportedCountryCode);
        }

        var country = await _countryRepository.GetCountryCode(
            countryCode,
            cancellationToken);

        if (country is null)
        {
            return Result<PhoneCountryLookupResponse>.Failure(
                CountryErrors.CountryNotFound);
        }

        var countryDto = new CountryDto(
            country.CountryCode,
            country.Name,
            country.CountryIso,
            country.CountryDetails
                .Select(detail =>
                    new CountryDetails(
                        detail.Operator,
                        detail.OperatorCode))
                .ToList());

        var response = new PhoneCountryLookupResponse(
            normalizedNumber,
            countryDto);

        return Result<PhoneCountryLookupResponse>.Success(response);
    }




    private async Task<string?> FindCountryCode(
        string phoneNumber,
        CancellationToken cancellationToken)
    {
        var supportedCountryCodes =await _countryRepository.GetCountryCodes(cancellationToken);

        return supportedCountryCodes
            .OrderByDescending(code => code.Length)
            .FirstOrDefault(phoneNumber.StartsWith);
    }

    private static string? NormalizePhoneNumber(string phoneNumber)
    {
        var number = phoneNumber.Trim();

        if (number.StartsWith("+"))
        {
            number = number[1..];
        }

        if (string.IsNullOrWhiteSpace(number))
        {
            return null;
        }

        if (number.Any(character => !char.IsDigit(character)))
        {
            return null;
        }

        return number;
    }
}
