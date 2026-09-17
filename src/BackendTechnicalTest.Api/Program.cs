using BackendTechnicalTest.Api.Extensions;
using BackendTechnicalTest.Api.Middleware;
using BackendTechnicalTest.Infrastructure;
using BackendTechnicalTest.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Phone Country Lookup API",
        Version = "v1",
        Description = "Technical test: array scoring + phone-number country/operator lookup backed by an in-memory database."
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();


app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTechnicalTest API v1");
});

app.UseHttpsRedirection();


await SeedDatabaseAsync(app);
app.MapEndpoints();

app.Run();

static async Task SeedDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await DatabaseSeeder.SeedAsync(context);
}

public partial class Program;