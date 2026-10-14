using UnitConverter.Application.Conversions;
using UnitConverter.Domain.Conversions;

namespace UnitConverter.Infrastructure.Conversions;

public sealed class HardcodedConversionProvider : IConversionProvider
{
    private static readonly IReadOnlyDictionary<string, UnitDefinition> Units =
        new Dictionary<string, UnitDefinition>(StringComparer.OrdinalIgnoreCase)
        {
            ["meter"] = new(
                "meter",
                ConversionCategory.Length,
                factorToBase: 1d),

            ["foot"] = new(
                "foot",
                ConversionCategory.Length,
                factorToBase: 0.3048d),

            ["celsius"] = new(
                "celsius",
                ConversionCategory.Temperature,
                factorToBase: 1d),

            ["fahrenheit"] = new(
                "fahrenheit",
                ConversionCategory.Temperature,
                factorToBase: 5d / 9d,
                offsetToBase: -160d / 9d),

            ["kilogram"] = new(
                "kilogram",
                ConversionCategory.Weight,
                factorToBase: 1d),

            ["pound"] = new(
                "pound",
                ConversionCategory.Weight,
                factorToBase: 0.45359237d)
        };

    public ConversionDefinition GetConversion(string fromUnit, string toUnit)
    {
        if (!Units.TryGetValue(fromUnit, out var from))
            throw new ArgumentException($"Unsupported unit '{fromUnit}'.", nameof(fromUnit));

        if (!Units.TryGetValue(toUnit, out var to))
            throw new ArgumentException($"Unsupported unit '{toUnit}'.", nameof(toUnit));

        if (from.Category != to.Category)
        {
            throw new ArgumentException(
                $"Cannot convert from '{from.Name}' to '{to.Name}' because the units belong to different categories.");
        }

        return new ConversionDefinition(from, to);
    }
}
