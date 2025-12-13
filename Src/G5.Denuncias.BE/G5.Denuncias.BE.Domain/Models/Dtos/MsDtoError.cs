using System.Diagnostics.CodeAnalysis;

namespace G5.Denuncias.BE.Domain.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class MsDtoError
    {
        public int code { get; set; }

        public string message { get; set; }
    }
}
