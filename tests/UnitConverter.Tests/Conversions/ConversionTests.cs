using UnitConverter.Application.Conversions;
using UnitConverter.Infrastructure.Conversions;

namespace UnitConverter.Tests.Conversions;

public sealed class ConversionTests
{
    private readonly ConvertValueQueryHandler _handler;

    public ConversionTests()
    {
        _handler = new ConvertValueQueryHandler(
            new HardcodedConversionProvider(),
            new ConvertValueValidator());
    }

    [Fact]
    public async Task Converts_Meter_To_Foot()
    {
        var result = await Convert(1, "meter", "foot");

        Assert.Equal(3.280839895013123, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Converts_Foot_To_Meter()
    {
        var result = await Convert(1, "foot", "meter");

        Assert.Equal(0.3048, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Converts_Celsius_To_Fahrenheit()
    {
        var result = await Convert(0, "celsius", "fahrenheit");

        Assert.Equal(32, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Converts_Fahrenheit_To_Celsius()
    {
        var result = await Convert(212, "fahrenheit", "celsius");

        Assert.Equal(100, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Converts_Kilogram_To_Pound()
    {
        var result = await Convert(1, "kilogram", "pound");

        Assert.Equal(2.2046226218487757, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Converts_Pound_To_Kilogram()
    {
        var result = await Convert(1, "pound", "kilogram");

        Assert.Equal(0.45359237, result.ConvertedValue, 12);
    }

    [Fact]
    public async Task Unit_Names_Are_Case_Insensitive()
    {
        var result = await Convert(1, "METER", "Foot");

        Assert.Equal("meter", result.FromUnit);
        Assert.Equal("foot", result.ToUnit);
    }

    [Fact]
    public async Task Rejects_Unsupported_Unit()
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => Convert(1, "yard", "meter"));

        Assert.Contains("Unsupported unit", exception.Message);
    }

    [Fact]
    public async Task Rejects_Cross_Category_Conversion()
    {
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => Convert(1, "meter", "fahrenheit"));

        Assert.Contains("different categories", exception.Message);
    }

    private Task<ConversionResultDto> Convert(
        double value,
        string fromUnit,
        string toUnit) =>
        _handler.Handle(
            new ConvertValueQuery(value, fromUnit, toUnit),
            CancellationToken.None);
}
