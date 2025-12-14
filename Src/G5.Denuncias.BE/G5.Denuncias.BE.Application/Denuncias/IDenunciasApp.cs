using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G5.Denuncias.BE.Application.Denuncias
{
    public interface IDenunciasApp
    {
        #region Usuarios
        Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioDtoIn request);
        Task<Autenticar?> AutenticarAsync(AutenticarDtoIn request);
        #endregion Usuarios

        #region Denuncias
        Task<Denuncia> CrearDenunciaAsync(CrearDenunciaDtoIn request);
        Task<Denuncia?> ObtenerDenunciaAsync(Guid id);
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias);
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(EnviarMensajeDtoIn mensaje);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId);
        #endregion Mensajes
    }
}
