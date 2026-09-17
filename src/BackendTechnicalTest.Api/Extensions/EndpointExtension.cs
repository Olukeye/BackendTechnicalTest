using BackendTechnicalTest.Api.Endpoints;

namespace BackendTechnicalTest.Api.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPhoneNumberEndpoints();
        app.MapScoreEndpoints();


        return app;
    }
}
