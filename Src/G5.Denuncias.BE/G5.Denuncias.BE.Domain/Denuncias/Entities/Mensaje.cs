using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Entities
{
    public class Mensaje
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonPropertyName("denunciaId")]
        public Guid? DenunciaId { get; set; }
        [JsonPropertyName("usuarioDestino")]
        public string UsuarioDestino { get; set; }
        [JsonPropertyName("remitente")]
        public string Remitente { get; set; } = string.Empty;
        [JsonPropertyName("texto")]
        public string Contenido { get; set; } = string.Empty;
        [JsonPropertyName("fecha")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
