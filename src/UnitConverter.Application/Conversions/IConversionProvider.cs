using UnitConverter.Domain.Conversions;

namespace UnitConverter.Application.Conversions;

public interface IConversionProvider
{
    ConversionDefinition GetConversion(string fromUnit, string toUnit);
}
