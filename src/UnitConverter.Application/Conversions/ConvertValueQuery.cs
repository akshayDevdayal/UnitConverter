using MediatR;

namespace UnitConverter.Application.Conversions;

public sealed record ConvertValueQuery(
    double? Value,
    string FromUnit,
    string ToUnit) : IRequest<ConversionResultDto>;
