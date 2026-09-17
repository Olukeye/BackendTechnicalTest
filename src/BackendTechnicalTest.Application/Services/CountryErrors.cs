using BackendTechnicalTest.Application.Common.Results;

namespace BackendTechnicalTest.Application.Services;

public static class CountryErrors
{
    public static readonly ResultError PhoneNumberRequired =
        new(
            "PhoneNumber.Required",
            "Phone number is required.");

    public static readonly ResultError InvalidPhoneNumber =
        new(
            "PhoneNumber.Invalid",
            "The supplied phone number is invalid.");

    public static readonly ResultError UnsupportedCountryCode =
        new(
            "CountryCode.Unsupported",
            "The supplied phone number contains an unsupported country code.");

    public static readonly ResultError CountryNotFound =
        new(
            "Country.NotFound",
            "The country associated with the supplied country code was not found.");
}
