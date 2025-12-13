using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Domain.Denuncias.Dtos
{
    public class RegistrarUsuarioDtoIn
    {
        public string NombreUsuario {  get; set; }
        public string ClaveHash {  get; set; }
    }
}
