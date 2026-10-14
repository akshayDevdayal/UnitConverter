namespace UnitConverter.Domain.Conversions;

public sealed class UnitDefinition
{
    public UnitDefinition(
        string name,
        ConversionCategory category,
        double factorToBase,
        double offsetToBase = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Unit name is required.", nameof(name));

        if (!double.IsFinite(factorToBase) || factorToBase == 0)
            throw new ArgumentOutOfRangeException(nameof(factorToBase));

        if (!double.IsFinite(offsetToBase))
            throw new ArgumentOutOfRangeException(nameof(offsetToBase));

        Name = name;
        Category = category;
        FactorToBase = factorToBase;
        OffsetToBase = offsetToBase;
    }

    public string Name { get; }
    public ConversionCategory Category { get; }
    public double FactorToBase { get; }
    public double OffsetToBase { get; }

    public double ToBase(double value) =>
        (value * FactorToBase) + OffsetToBase;

    public double FromBase(double baseValue) =>
        (baseValue - OffsetToBase) / FactorToBase;
}
