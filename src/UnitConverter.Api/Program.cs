using FastEndpoints;
using FastEndpoints.Swagger;
using FluentValidation;
using UnitConverter.Application.Conversions;
using UnitConverter.Infrastructure.Conversions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.SwaggerDocument(options =>
{
    options.DocumentSettings = settings =>
    {
        settings.Title = "Unit Converter API";
        settings.Version = "v1";
    };
});

builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssemblyContaining<ConvertValueQuery>());

builder.Services.AddValidatorsFromAssemblyContaining<ConvertValueValidator>();

builder.Services.AddSingleton<IConversionProvider, HardcodedConversionProvider>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseFastEndpoints();
app.UseSwaggerGen();

app.MapHealthChecks("/healthz");
app.MapHealthChecks("/ready");

app.Run();

public partial class Program;
