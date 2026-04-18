using OutageApi.Application.Services;
using OutageApi.Application.Interfaces;
using OutageApi.Infrastructure.Repositories;
using Scalar.AspNetCore;
using Asp.Versioning;
using Asp.Versioning.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc(options =>
{
    // Map version based on folder (namespace) convention
    options.Conventions.Add(new VersionByNamespaceConvention());
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddOpenApi("v1", options =>
{
    options.ShouldInclude = (description) => description.GroupName == "v1";
});

builder.Services.AddOpenApi("v2", options =>
{
    options.ShouldInclude = (description) => description.GroupName == "v2";
});

//Inject dependencies
builder.Services.AddSingleton<IOutageRepository, InMemoryOutageRepository>();
builder.Services.AddSingleton<IOutageService, OutageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .AddDocument("v1", "Version 1", "/openapi/v1.json")
        .AddDocument("v2", "Version 2", "/openapi/v2.json");
});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
