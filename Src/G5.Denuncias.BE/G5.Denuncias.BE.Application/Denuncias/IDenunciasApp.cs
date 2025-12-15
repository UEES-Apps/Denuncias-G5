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
        Task<IEnumerable<Denuncia>> ObtenerDenunciasAsync();
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasAsync();
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(EnviarMensajeDtoIn mensaje);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid denunciaId);
        #endregion Mensajes
    }
}
