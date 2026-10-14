# UnitConverter API

A small ASP.NET Core Web API for converting values between units.

## Architecture

- .NET 9
- FastEndpoints
- MediatR / CQRS
- FluentValidation
- Clean Architecture + Vertical Slice organization
- No database
- No Docker
- No authentication

## Supported conversions

| Category | Units |
|---|---|
| Length | meter, foot |
| Temperature | celsius, fahrenheit |
| Weight | kilogram, pound |

Unit names are case-insensitive.

## Run in Visual Studio

1. Open `UnitConverter.sln`.
2. Set `UnitConverter.Api` as the startup project.
3. Press `F5`.
4. Swagger opens automatically.
5. Call `POST /api/conversions`.

## Example

Request:

```json
{
  "value": 1,
  "fromUnit": "meter",
  "toUnit": "foot"
}
```

Response:

```json
{
  "value": 1,
  "fromUnit": "meter",
  "toUnit": "foot",
  "convertedValue": 3.280839895013123
}
```

## Invalid conversion

A conversion between different categories returns `400 Bad Request`.

Example: `meter` to `fahrenheit`.

## Future extension

`ConvertValueQueryHandler` depends on `IConversionProvider`.

The current implementation is `HardcodedConversionProvider`. A future implementation can load unit definitions from a database or another source without changing the API endpoint or CQRS query.
