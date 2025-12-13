using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace G5.Denuncias.BE.Domain.Models.Dtos
{
    [ExcludeFromCodeCoverage]
    public class MessageResponseDto
    {
        [JsonPropertyName("message")]
        public string? Data { get; set; }
    }
}
