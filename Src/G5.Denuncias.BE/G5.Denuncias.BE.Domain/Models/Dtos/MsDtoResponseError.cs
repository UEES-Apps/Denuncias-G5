using System.Diagnostics.CodeAnalysis;

namespace G5.Denuncias.BE.Domain.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class MsDtoResponseError
    {
        public int code { get; set; }

        public string traceid { get; set; }

        public string message { get; set; }

        public List<MsDtoError> errors { get; set; }

    }
}
