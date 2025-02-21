using HL7ConsentProcessing.Application.Services;
using HL7ConsentProcessing.Domain.Entities;
using HL7ConsentProcessing.Domain.Repositories;
using HL7ConsentProcessing.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<FhirConsent>, FhirConsentRepository>();
builder.Services.AddScoped<FhirConsentService>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


