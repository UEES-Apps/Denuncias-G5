using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Domain.Denuncias
{
    public interface IDenunciaRepository
    {
        #region Usuarios
        Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string claveHash);
        Task<string?> AutenticarAsync(string nombreUsuario, string claveHash);
        #endregion Usuarios

        #region Denuncias
        Task<Denuncia> CrearDenunciaAsync(Denuncia denuncia);
        Task<Denuncia?> ObtenerDenunciaAsync(Guid id);
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias);
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(Mensaje mensaje);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId);
        #endregion Mensajes
    }
}
