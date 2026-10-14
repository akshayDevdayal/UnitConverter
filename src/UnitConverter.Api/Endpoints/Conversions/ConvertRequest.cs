namespace UnitConverter.Api.Endpoints.Conversions;

public sealed class ConvertRequest
{
    public double? Value { get; init; }
    public string FromUnit { get; init; } = string.Empty;
    public string ToUnit { get; init; } = string.Empty;
}
