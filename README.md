# PhoneCountryApi

ASP.NET Core 8 solution for the back-end practical test:

1. **Score calculation** — score an integer array (+1 even, +3 odd, +5 bonus every time `8` appears).
2. **Phone → country/operator lookup** — an in-memory-seeded Web API that detects a phone number's
   country dialing code and returns the country plus its mobile network operators.

## Architecture

Clean, layered architecture so each concern can be tested and changed independently:

```
PhoneCountryApi.sln
├── src/
│   ├── PhoneCountryApi.Domain         
│   ├── PhoneCountryApi.Application    
│   ├── PhoneCountryApi.Infrastructure  
│   └── PhoneCountryApi.Api             
└── tests/
    └── PhoneCountryApi.Tests          
```

- **Dependency direction**: Api → Infrastructure/Application → Domain. Controllers depend on
  `IScoreCalculatorService` / `ICountryLookupService` interfaces, not concrete classes, so the
  logic is unit-testable without spinning up ASP.NET Core.
- **Database**: `Microsoft.EntityFrameworkCore.InMemory`, seeded via `HasData` in
  `AppDbContext.OnModelCreating` directly from Figure 1 (Country) and Figure 2 (Country Details) —
  no external DB required.
- **Error handling**: a single `ExceptionHandlingMiddleware` maps `CountryNotFoundException` → 404
  and `InvalidPhoneNumberException` → 400, both as RFC 7807 `ProblemDetails`, so controllers stay
  free of try/catch blocks.

## Running it

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
cd PhoneCountryApi
dotnet restore
dotnet run --project src/PhoneCountryApi.Api
```

The API starts on `http://localhost:5080` (see `Properties/launchSettings.json`) and opens
Swagger UI at `http://localhost:5080/swagger` automatically in Development.

Run the tests:

```bash
dotnet test
```

Manual requests are in `src/PhoneCountryApi.Api/PhoneCountryApi.Api.http` (usable directly from
VS Code's REST Client extension or JetBrains Rider), or via curl:

### Task 1 — Score

```bash
curl -X POST http://localhost:5080/api/v1/scores/calculate \
  -H "Content-Type: application/json" \
  -d '{"numbers": [1,2,3,4,5]}'
# -> {"input":[1,2,3,4,5],"score":11}
```

### Task 2 — Country lookup

```bash
curl http://localhost:5080/api/v1/countries/2348033432323
```

```json
{
  "number": "2348033432323",
  "country": {
    "countryCode": "234",
    "name": "Nigeria",
    "countryIso": "NG",
    "countryDetails": [
      { "operator": "MTN Nigeria", "operatorCode": "MTN NG" },
      { "operator": "Airtel Nigeria", "operatorCode": "ANG" },
      { "operator": "9 Mobile Nigeria", "operatorCode": "ETN" },
      { "operator": "Globacom Nigeria", "operatorCode": "GLO NG" }
    ]
  }
}
```

The endpoint also accepts a leading `+` or `00` international dial-out prefix
(`+2348033432323`, `002348033432323`). An unmatched code returns `404`; a non-numeric input
returns `400`.

## Design notes / assumptions

- **Country code matching** uses longest-prefix matching against the seeded dialing codes, so
  codes that share a leading digit (e.g. `233` Ghana vs `234` Nigeria) resolve correctly even
  though the current data set only has 3-digit codes — this keeps the logic correct if
  differently-sized codes are added later.
- **Score rule**: `8` counts once as an even number (+1) *and* separately triggers the "every
  time you encounter an 8" bonus (+5), which is what makes `[8,8] → 12` correct
  (2 × 1 for even, plus 2 × 5 for the bonus).
- Response DTOs deliberately mirror the exact JSON shape given in the spec
  (`number`, `country.countryCode`, `country.countryDetails[].operator`, etc.) via
  `JsonPropertyName` attributes, independent of the internal C# property names.
