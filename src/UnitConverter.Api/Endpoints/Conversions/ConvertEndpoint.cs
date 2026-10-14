using FastEndpoints;
using FluentValidation;
using MediatR;
using UnitConverter.Application.Conversions;

namespace UnitConverter.Api.Endpoints.Conversions;

public sealed class ConvertEndpoint(ISender sender)
    : Endpoint<ConvertRequest, ConvertResponse>
{
    public override void Configure()
    {
        Post("/api/conversions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        ConvertRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(
                new ConvertValueQuery(
                    request.Value,
                    request.FromUnit,
                    request.ToUnit),
                cancellationToken);

            var response = new ConvertResponse(
                result.Value,
                result.FromUnit,
                result.ToUnit,
                result.ConvertedValue);

            HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            await HttpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        }
        catch (ValidationException exception)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await HttpContext.Response.WriteAsJsonAsync(
                new
                {
                    errors = exception.Errors
                        .Select(error => error.ErrorMessage)
                        .Distinct()
                        .ToArray()
                },
                cancellationToken);
        }
        catch (ArgumentException exception)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await HttpContext.Response.WriteAsJsonAsync(
                new
                {
                    errors = new[] { exception.Message }
                },
                cancellationToken);
        }
    }
}
