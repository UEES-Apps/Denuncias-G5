using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using G5.Denuncias.BE.Api.IoC;
using G5.Denuncias.BE.Application.IoC;
using G5.Denuncias.BE.Domain.Database;
using G5.Denuncias.BE.Infraestructure.Extentions;
using G5.Denuncias.BE.Infraestructure.IoC;
using G5.Denuncias.BE.Infraestructure.Observabilidad;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Serilog;

Log.Information("Inicia BE Denuncias G5");

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Services.AddControllers();
    builder.Services.AddMemoryCache();

    // Versionado
    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(
            new UrlSegmentApiVersionReader()
        );
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

    // Swagger
    builder.Services.AddSwaggerGen(options =>
    {

        var serviceProvider = builder.Services.BuildServiceProvider();
        var apiVersionDescriptionProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            var title = builder.Configuration["OpenApi:info:title"];
            var apiDescription = builder.Configuration["OpenApi:info:description"];
            var termsOfService = new Uri(builder.Configuration["OpenApi:info:termsOfService"]);
            var contact = new OpenApiContact
            {
                Name = builder.Configuration["OpenApi:info:contact:name"],
                Url = new Uri(builder.Configuration["OpenApi:info:contact:url"]),
                Email = builder.Configuration["OpenApi:info:contact:email"]
            };
            var license = new OpenApiLicense
            {
                Name = builder.Configuration["OpenApi:info:License:name"],
                Url = new Uri(builder.Configuration["OpenApi:info:License:url"])
            };

            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = title,
                Description = BuildApiDescription(apiDescription, description),
                TermsOfService = termsOfService,
                Contact = contact,
                License = license
            });
        }

        if (!apiVersionDescriptionProvider.ApiVersionDescriptions.Any())
        {
            var version = builder.Configuration["OpenApi:info:version"];
            var title = builder.Configuration["OpenApi:info:title"];
            var description = builder.Configuration["OpenApi:info:description"];
            var termsOfService = new Uri(builder.Configuration["OpenApi:info:termsOfService"]);
            var contact = new OpenApiContact
            {
                Name = builder.Configuration["OpenApi:info:contact:name"],
                Url = new Uri(builder.Configuration["OpenApi:info:contact:url"]),
                Email = builder.Configuration["OpenApi:info:contact:email"]
            };
            var license = new OpenApiLicense
            {
                Name = builder.Configuration["OpenApi:info:License:name"],
                Url = new Uri(builder.Configuration["OpenApi:info:License:url"])
            };

            options.SwaggerDoc(version, new OpenApiInfo
            {
                Version = version,
                Title = title,
                Description = description,
                TermsOfService = termsOfService,
                Contact = contact,
                License = license
            });
        }
        List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
    });

    builder.Services.AddHttpClient();
    builder.Services.AddHttpContextAccessor();
    builder.Services.RegisterDependencies(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Logging.AddSerilog();
    builder.Logging.AddOpenTelemetry();

    builder.Host.UseSerilog();

    // Agregar servicios de compresión
    builder.Services.AddResponseCompression(options =>
    {
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = new[]
        {
            "text/plain",
            "application/json",
            "text/html",
        };
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = System.IO.Compression.CompressionLevel.Optimal;
    });
    builder.Services.AddEndpointsApiExplorer();

    var app = builder.Build();

    Log.Information(" Run  BE Denuncias G5");

    // Inicializar Migracion EntityFramework
    var useInMemory = builder.Configuration.GetValue<bool>("UseInMemorySession");
    if (!useInMemory)
    {
        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            await initializer.InitializeAsync();
        }
    }

    // Global Exception Handler
    app.ConfigureExceptionHandler();

    // Middleware
    app.UseMiddleware<ExceptionMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        // SwaggerUI Dinámico
        app.UseSwaggerUI(c =>
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            // Crear endpoints para cada versión disponible (en orden inverso para mostrar la más nueva primero)
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"Denuncias G5 API {description.GroupName.ToUpperInvariant()}"
                );
            }

            // usar configuración por defecto
            if (!apiVersionDescriptionProvider.ApiVersionDescriptions.Any())
            {
                string version = builder.Configuration["OpenApi:info:version"] ?? "v1";
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Denuncias G5 API {version.ToUpperInvariant()}");
            }
        });
    }
    else
    {
        app.UseHsts();
    }

    app.UseResponseCompression();

    // Middleware para encabezados personalizados
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        await next();
    });

    app.UseRouting();
    app.UseCors("AllowSpecificOrigin");
    app.UseSession();
    app.UseAuthorization(); 
    app.MapControllers();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Error(ex, "Error BE Denuncias G5");
}
finally
{
    Log.CloseAndFlush();
}

static string BuildApiDescription(string description, ApiVersionDescription apiVersionDescription)
{
    var text = description;
    if (apiVersionDescription.IsDeprecated)
    {
        text += " Esta versión de la API está obsoleta.";
    }
    return text;
}