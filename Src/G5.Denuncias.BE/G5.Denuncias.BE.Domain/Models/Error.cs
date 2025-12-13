using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace G5.Denuncias.BE.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class Error
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}