using BackendTechnicalTest.Application.Interface;
namespace BackendTechnicalTest.Api.Endpoints;

public static class ScoreEndpoints
{
    public static IEndpointRouteBuilder MapScoreEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup("/api/v1/scores")
            .WithTags("Scores");

        group.MapPost("/calculate",ScoreCalculator)
            .WithName("CalculateScore")
            .WithSummary("Calculates the score for an array of integers.")
            .WithDescription(
                "Adds 1 for each even number, " +
                "3 for each odd number, and an additional 5 " +
                "for every occurrence of the number 8.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        return endpoints;
    }


   private static async Task<IResult> ScoreCalculator(
   int[] numbers,
   IScoreCalculatorService scoreService)
   {
        if (numbers is null)
        {
            return Results.BadRequest(new
            {
                message = "Numbers are required."
            });
        }

        var score = scoreService.CalculateScore(numbers);

        return Results.Ok(new
        {
            score
        });
   }
}