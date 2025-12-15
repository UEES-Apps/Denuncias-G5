using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace G5.Denuncias.BE.Infraestructure.Observabilidad
{
    public static class Monitoring
    {
        public static IServiceCollection AddLoggingService(this IServiceCollection services, IConfiguration configuration)
        {
            // Se agregan Logs ELK
            Log.Logger = new LoggerConfiguration()
                  .ReadFrom
                  .Configuration(configuration).CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            return services;
        }

        public static IServiceCollection AddOpenTelemetryTracingService(this IServiceCollection services, IConfiguration configuration)
        {
            // Se agregan OpenTelemetry Otlp Exporter
            _ = int.TryParse(configuration["Jaeger:Telemetry:Port"], out int portNumber);
            services.AddOpenTelemetry().WithTracing(tracerProviderBuilder =>
            {
                tracerProviderBuilder
                .AddSource(configuration["Serilog:Properties:Application"] ?? typeof(Monitoring).Namespace ?? "")
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: configuration["Serilog:Properties:Application"] ?? typeof(Monitoring).Namespace ?? ""))
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.EnrichWithHttpRequest = (activity, httpRequest) =>
                    {

                        if (httpRequest is HttpRequest)
                        {
                            string? traceid = httpRequest.HttpContext?.TraceIdentifier;
                            if (!string.IsNullOrWhiteSpace(traceid))
                                activity.SetTag("Log-Traceid", traceid);

                            var forwardedFor = httpRequest.HttpContext?.Request.Headers["X-Forwarded-For"];
                            if (!string.IsNullOrWhiteSpace(forwardedFor))
                            {
                                string? forwardedForIpAddress = forwardedFor?[0]?.Split(',').Select(s => s.Trim()).FirstOrDefault();
                                if (!string.IsNullOrWhiteSpace(forwardedForIpAddress))
                                {
                                    activity.SetTag("X-Forwarded-For", forwardedForIpAddress);
                                }
                            }
                            string? clientAddress = httpRequest.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? httpRequest.HttpContext?.Request.Headers["REMOTE_ADDR"][0]?.Split(',').Select(s => s.Trim()).FirstOrDefault();
                            if (!string.IsNullOrWhiteSpace(clientAddress))
                                activity.SetTag("Client-Address", clientAddress);
                        }

                    };
                })
                .AddSqlClientInstrumentation(options =>
                {
                    options.EnableConnectionLevelAttributes = true;
                    options.SetDbStatementForStoredProcedure = true;
                    options.SetDbStatementForText = true;
                    options.RecordException = false;
                    options.Enrich = (activity, x, y) => activity.SetTag("db.type", "sql");
                });
            });
            return services;
        }

        public static IServiceCollection AddHealthChecksService(this IServiceCollection services)
        {

            services.AddHealthChecks();
            return services;
        }

        public static IApplicationBuilder ConfigureMetricServerApp(this IApplicationBuilder app)
        {
            app.UseMetricServer();
            app.UseHttpMetrics();
            return app;
        }

        public static IApplicationBuilder ConfigureHealthChecksApp(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });

                endpoints.MapHealthChecks("/health/liveness", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    Predicate = _ => false
                });
            });
            return app;
        }
    }
}
