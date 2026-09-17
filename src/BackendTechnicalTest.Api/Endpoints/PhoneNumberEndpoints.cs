using BackendTechnicalTest.Application.DTOs;
using BackendTechnicalTest.Application.Interface;


namespace BackendTechnicalTest.Api.Endpoints;

public static class PhoneNumberEndpoints
{
    public static IEndpointRouteBuilder MapPhoneNumberEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/phone-numbers")
            .WithTags("Phone Numbers");

        group.MapGet("/country", Country)
            .WithName("GetCountryByPhoneNumber")
            .WithSummary("Detects the country associated with a phone number.")
            .WithDescription("Accepts a phone number and determines the country using the country code.")
            .Produces<PhoneCountryLookupResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        return endpoints;
    }


    private static async Task<IResult> Country(
    string phoneNumber,
    ICountryService countryService,
    CancellationToken cancellationToken)
    {

    var result =await countryService.GetCountryByPhoneNumber(phoneNumber, cancellationToken);
    if (result.IsSuccess)
    {
        return Results.Ok(result.Value);
    }

    return result.Error!.Code switch
    {
        "PhoneNumber.Required" =>
            Results.BadRequest(new
            {
                code = result.Error.Code,
                message = result.Error.Message
            }),

        "PhoneNumber.Invalid" =>
            Results.BadRequest(new
            {
                code = result.Error.Code,
                message = result.Error.Message
            }),

        "CountryCode.Unsupported" =>
            Results.NotFound(new
            {
                code = result.Error.Code,
                message = result.Error.Message
            }),

        "Country.NotFound" =>
            Results.NotFound(new
            {
                code = result.Error.Code,
                message = result.Error.Message
            }),

        _ => Results.Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "An unexpected error occurred.")
        };

    }
}
