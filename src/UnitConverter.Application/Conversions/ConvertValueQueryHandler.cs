using FluentValidation;
using MediatR;

namespace UnitConverter.Application.Conversions;

public sealed class ConvertValueQueryHandler(
    IConversionProvider conversionProvider,
    IValidator<ConvertValueQuery> validator)
    : IRequestHandler<ConvertValueQuery, ConversionResultDto>
{
    public async Task<ConversionResultDto> Handle(
        ConvertValueQuery request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var conversion = conversionProvider.GetConversion(
            request.FromUnit,
            request.ToUnit);

        var value = request.Value!.Value;
        var convertedValue = conversion.Convert(value);

        return new ConversionResultDto(
            value,
            conversion.FromUnit.Name,
            conversion.ToUnit.Name,
            convertedValue);
    }
}
