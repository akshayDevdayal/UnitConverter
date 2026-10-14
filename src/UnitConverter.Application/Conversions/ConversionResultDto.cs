namespace UnitConverter.Application.Conversions;

public sealed record ConversionResultDto(
    double Value,
    string FromUnit,
    string ToUnit,
    double ConvertedValue);
