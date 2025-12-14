using G5.Denuncias.BE.Domain.Models.Dtos;
using G5.Denuncias.BE.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;
using Microsoft.AspNetCore.Diagnostics;

namespace G5.Denuncias.BE.Infraestructure.Extentions
{

    [ExcludeFromCodeCoverage]
    public class ErroresCustom
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Enum { get; set; }
    }

    public static class ApplicationExtentions
    {

        public static string GetEnumMemberValue<T>(this T enumValue) where T : Enum
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((EnumMemberAttribute)attributes[0]).Value;
                }
            }

            // Return default value if no EnumMemberAttribute is found
            return enumValue.ToString();
        }

        private static void ProcesarMensajeErrorConFormato(CustomException customException, IApplicationBuilder app, ErroresDto errores)
        {
            var enumMemberValue = customException._tipoError.GetEnumMemberValue();
            var enumParts = enumMemberValue.Split(':');

            if (enumParts.Length >= 2)
            {
                var configSection = enumParts[0];
                var errorCode = enumParts[1];

                var erroresSeccion = app.ApplicationServices
                    .GetRequiredService<IConfiguration>()
                    .GetSection(configSection)
                    .Get<List<ErroresCustom>>();

                errores.code = errorCode;

                if (erroresSeccion != null && customException._stringFormat != null)
                {
                    var errorConfig = erroresSeccion.FirstOrDefault(x => x.Enum == errorCode);
                    if (errorConfig != null)
                    {
                        errores.message = string.Format(errorConfig.Value, customException._stringFormat.ToArray());
                    }
                }

                if (string.IsNullOrEmpty(errores.message))
                {
                    errores.message = customException.Message;
                }

                errores.errors = customException._errores;
            }
        }
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder app)
        {

            var mensajeError = string.Empty;

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {


                    ErroresDto errores = new ErroresDto()
                    {
                        traceid = context.TraceIdentifier,


                        errors = new List<Error>(),

                    };

                    context.Response.ContentType = "application/json";
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    try
                    {
                        var customException = exceptionHandlerPathFeature?.Error as CustomException;
                        if (customException != null)
                        {
                            switch (customException._tipoError)
                            {

                                case TipoErrorEnum.ERROR_INTERNO:
                                    throw new CustomException(TipoErrorEnum.ERROR_INTERNO, string.Empty);

                                case TipoErrorEnum.TOKEN_NO_VALIDO:
                                    context.Response.StatusCode = 401;
                                    errores.code = customException._tipoError.GetEnumMemberValue();
                                    errores.message = customException.Message;
                                    break;

                                case TipoErrorEnum.NO_SE_RETORNO_DATOS:
                                    context.Response.StatusCode = 400;
                                    ProcesarMensajeErrorConFormato(customException, app, errores);
                                    break;

                                default:
                                    context.Response.StatusCode = 400;
                                    errores.code = customException._tipoError.GetEnumMemberValue();
                                    errores.message = customException.Message;
                                    errores.errors = customException._errores;

                                    var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();

                                    var spli = errores.code.Split(":");

                                    if (spli.Length == 2)
                                    {

                                        var arraysErrores = configuration.GetSection(spli.FirstOrDefault()).Get<List<ErroresCustom>>();

                                        errores.code = arraysErrores.FirstOrDefault(x => x.Enum == spli.LastOrDefault())?.Enum;
                                        if (!string.IsNullOrEmpty((arraysErrores.FirstOrDefault(x => x.Enum == errores.code)?.Key)))
                                            errores.errors = new List<Error> {
                                        new Error
                                        {
                                            Code=arraysErrores.FirstOrDefault(x => x.Enum == errores.code)?.Key,
                                            Message= arraysErrores.FirstOrDefault(x => x.Enum == errores.code)?.Value
                                        }
                                        };
                                    }
                                    break;

                            }
                        }
                        else
                        {
                            throw new CustomException(TipoErrorEnum.ERROR_INTERNO, string.Empty);
                        }

                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        errores.code = TipoErrorEnum.ERROR_INTERNO.GetEnumMemberValue();
                        errores.message = "Estamos presentando un error de conexión. Por favor, inténtalo de nuevo en unos minutos.";

                    }


                    string stringJson = JsonSerializer.Serialize(errores);
                                        
                    await context.Response.WriteAsync(stringJson);
                    await context.Response.Body.FlushAsync();
                });
            });



            return app;
        }
    }
}
