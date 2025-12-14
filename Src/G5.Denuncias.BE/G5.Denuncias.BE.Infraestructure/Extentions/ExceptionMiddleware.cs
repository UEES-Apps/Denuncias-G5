using G5.Denuncias.BE.Domain.Models.Dtos;
using G5.Denuncias.BE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Serilog;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace G5.Denuncias.BE.Infraestructure.Extentions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var customException = exceptionHandlerPathFeature?.Error as CustomException;
                if (customException is null)
                {
                    await _next(context);
                }
                else
                {
                    await HandleCustomExceptionAsync(context, customException);
                }
            }
            catch (CustomException ex)
            {
                await HandleCustomExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleUnhandledExceptionAsync(context, ex);
            }
        }

        private static async Task HandleCustomExceptionAsync(HttpContext context, CustomException ex)
        {
            var customException = ex;
            ErroresDto errores = new ErroresDto()
            {
                traceid = context.TraceIdentifier,


                errors = new List<Error>(),

            };

            context.Response.ContentType = "application/json";
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            if (customException != null)
            {
                switch (customException._tipoError)
                {

                    case TipoErrorEnum.ERROR_INTERNO:
                        context.Response.StatusCode = 500;
                        errores.code = customException._tipoError.GetEnumMemberValue();
                        errores.message = customException.Message;
                        break;

                    case TipoErrorEnum.TOKEN_NO_VALIDO:
                        context.Response.StatusCode = 401;
                        errores.code = customException._tipoError.GetEnumMemberValue();
                        errores.message = customException.Message;
                        break;

                    case TipoErrorEnum.NO_SE_RETORNO_DATOS:
                        context.Response.StatusCode = 400;
                        errores.code = customException._tipoError.GetEnumMemberValue();
                        errores.message = customException.Message;
                        break;

                    default:
                        context.Response.StatusCode = 400;
                        errores.code = customException._tipoError.GetEnumMemberValue();
                        errores.message = customException.Message;
                        errores.errors = customException._errores;

                        break;


                }
            }
            else
            {
                context.Response.StatusCode = 500;
                errores.code = TipoErrorEnum.ERROR_INTERNO.GetEnumMemberValue();
                errores.message = string.Empty;
            }

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(errores);
        }

        private static async Task HandleUnhandledExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErroresDto
            {
                code = ErroresEnum.TipoErrorEnum.ERROR_INTERNO.GetEnumMemberValue(),
                traceid = context.TraceIdentifier,
                message = "Ocurrió un error interno.",
                errors = new List<Error>()
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
