using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Entities
{
    public class Mensaje
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? DenunciaId { get; set; }
        public Guid UsuarioDestinoId { get; set; }
        public string Remitente { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
