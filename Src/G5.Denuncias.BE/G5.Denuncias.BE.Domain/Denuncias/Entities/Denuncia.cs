using G5.Denuncias.BE.Domain.Enums.Denuncias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Entities
{
    public class Denuncia
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
        public string CiudadProvincia { get; set; } = string.Empty;
        public bool EsPublica { get; set; }
        public TipoDenuncia Tipo { get; set; }
        public Guid? UsuarioId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
