using G5.Denuncias.BE.Api.Services.Models;
using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Api.Services.Interface
{
    public interface IDenunciasService
    {

        #region Usuarios
        Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioInput request);
        Task<Autenticar?> AutenticarAsync(AutenticarInput request);
        #endregion Usuarios

        #region Denuncias
        Task<Denuncia> CrearDenunciaAsync(CrearDenunciaInput request);
        Task<IEnumerable<Denuncia>> ObtenerDenunciasAsync();
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasAsync();
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(EnviarMensajeInput request);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid denunciaId);
        #endregion Mensajes

    }
}
