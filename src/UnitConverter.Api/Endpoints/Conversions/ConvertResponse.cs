namespace UnitConverter.Api.Endpoints.Conversions;

public sealed record ConvertResponse(
    double Value,
    string FromUnit,
    string ToUnit,
    double ConvertedValue);
