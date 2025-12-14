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
        Task<Denuncia?> ObtenerDenunciaAsync(Guid id);
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias);
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(EnviarMensajeInput request);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId);
        #endregion Mensajes

    }
}
