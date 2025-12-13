using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NombreUsuario { get; set; } = string.Empty;
        public string ClaveHash { get; set; } = string.Empty;
    }
}
