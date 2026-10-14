namespace UnitConverter.Domain.Conversions;

public sealed class ConversionDefinition
{
    public ConversionDefinition(UnitDefinition fromUnit, UnitDefinition toUnit)
    {
        ArgumentNullException.ThrowIfNull(fromUnit);
        ArgumentNullException.ThrowIfNull(toUnit);

        if (fromUnit.Category != toUnit.Category)
            throw new ArgumentException("Units must belong to the same conversion category.");

        FromUnit = fromUnit;
        ToUnit = toUnit;
    }

    public UnitDefinition FromUnit { get; }
    public UnitDefinition ToUnit { get; }

    public double Convert(double value)
    {
        if (!double.IsFinite(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var baseValue = FromUnit.ToBase(value);
        return ToUnit.FromBase(baseValue);
    }
}
