using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OutageApi.Application.Interfaces;
using OutageApi.Application.Services;
using OutageApi.Infrastructure.Data;
using OutageApi.Infrastructure.Repositories;
using Scalar.AspNetCore;
using Serilog;
using Microsoft.EntityFrameworkCore;
using OutageApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

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

// Setup Serilog
// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
//     .CreateLogger();

// Will use elow Serilog from appsettings.json
// builder.Host.UseSerilog((context, config) =>
// {
//     config.MinimumLevel.Information().MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
//           .WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}")
//           .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}")
//           .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
// }); 

//Fetch serolig settings from appsettings.json
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

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
builder.Services.AddScoped<IUserService, AuthService>();
builder.Services.AddInfrastructure(builder.Configuration);

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//     app.MapScalarApiReference(options =>
//     {
//         options
//             .AddDocument("v1", "Version 1", "/openapi/v1.json")
//             .AddDocument("v2", "Version 2", "/openapi/v2.json");
//     });
// }

//Register swagger with versions v1, v2
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Outage API",
        Version = "v1",
        Description = "API for monitoring outages - Version 1",
        Contact = new OpenApiContact
        {
            Name = "Jasmeet Singh",
            Email = "jas@gmail.com"
        }
    });

    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "Outage API",
        Version = "v2",
        Description = "API for monitoring outages - Version 2",
        Contact = new OpenApiContact
        {
            Name = "Jasmeet Singh",
            Email = "jas@gmail.com"
        }
    });

    // Tell Swashbuckle which endpoints go in which version document
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        return apiDesc.GroupName == docName;
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Outage API v1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Outage API v2");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
