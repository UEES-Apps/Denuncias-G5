using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Domain.Denuncias
{
    public interface IDenunciaRepository
    {
        #region Usuarios
        Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string claveHash);
        Task<Autenticar?> AutenticarAsync(string nombreUsuario, string claveHash);
        #endregion Usuarios

        #region Denuncias
        Task<Denuncia> CrearDenunciaAsync(Denuncia denuncia);
        Task<IEnumerable<Denuncia>> ObtenerDenunciasAsync();
        Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasAsync();
        #endregion Denuncias

        #region Mensajes
        Task<Mensaje> EnviarMensajeAsync(Mensaje mensaje);
        Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid denunciaId);
        #endregion Mensajes
    }
}
