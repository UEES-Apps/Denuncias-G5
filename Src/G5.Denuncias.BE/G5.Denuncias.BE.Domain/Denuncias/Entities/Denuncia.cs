using G5.Denuncias.BE.Domain.Enums.Denuncias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Entities
{
    public class Denuncia
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; } = string.Empty;
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        [JsonPropertyName("fechaEvento")]
        public DateTime FechaEvento { get; set; }
        [JsonPropertyName("ciudad")]
        public string CiudadProvincia { get; set; } = string.Empty;
        [JsonPropertyName("esPublica")]
        public string EsPublica { get; set; } = string.Empty;
        [JsonPropertyName("tipo")]
        public string Tipo { get; set; } = string.Empty;
        [JsonPropertyName("autor")]
        public string Usuario { get; set; }
        [JsonPropertyName("fechaCreacion")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
