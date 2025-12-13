using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace G5.Denuncias.BE.Domain.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ErroresDto
    {
        public string? code { get; set; }

        public string? traceid { get; set; }

        public string? message { get; set; }

        public List<Error> errors { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ErroresEnum
    {
        public enum TipoErrorEnum
        {
            [EnumMember(Value = "api_solicitud_invalida")]
            SOLICITUD_INVALIDA,
            [EnumMember(Value = "api_internal_error")]
            ERROR_INTERNO,
            [EnumMember(Value = "api_no_datos")]
            SOLICITUD_NO_DATOS,
            [EnumMember(Value = "api_request_no_valido")]
            REQUEST_NO_VALIDO,
            [EnumMember(Value = "api_validacion_no_valido")]
            SOLICITUD_NO_VALIDACION,
            [EnumMember(Value = "api_token_no_valido")]
            TOKEN_NO_VALIDO,

            [EnumMember(Value = "api_no_datos")]
            NO_SE_RETORNO_DATOS

        }
    }
}
