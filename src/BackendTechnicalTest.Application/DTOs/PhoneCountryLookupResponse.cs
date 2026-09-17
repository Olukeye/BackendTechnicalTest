namespace BackendTechnicalTest.Application.DTOs;

public sealed record PhoneCountryLookupResponse(
    string Number,
    CountryDto Country
    );