using G5.Denuncias.BE.Domain.Denuncias.Entities;
using G5.Denuncias.BE.Domain.Denuncias;
using Microsoft.AspNetCore.Http;
using G5.Denuncias.BE.Infraestructure.Extentions;
using G5.Denuncias.BE.Infraestructure.Token;
using Microsoft.Extensions.Configuration;
using G5.Denuncias.BE.Domain.Models;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;

namespace G5.Denuncias.BE.Infraestructure.Repository.Denuncias
{
    public class DenunciaRepository(IHttpContextAccessor contextoHttp, IConfiguration configuration) : IDenunciaRepository
    {
        private ISession Session { get; } = contextoHttp.HttpContext!.Session;
        private readonly IConfiguration _configuration = configuration;

        private const string UsersKey = "denuncias_users";
        private const string DenunciasKey = "denuncias_denuncias";
        private const string MensajesKey = "denuncias_mensajes";


        private List<Usuario> Users => Session.ReadFromSession<List<Usuario>>(UsersKey) ?? new List<Usuario>();
        private List<Denuncia> Denuncias => Session.ReadFromSession<List<Denuncia>>(DenunciasKey) ?? new List<Denuncia>();
        private List<Mensaje> Mensajes => Session.ReadFromSession<List<Mensaje>>(MensajesKey) ?? new List<Mensaje>();

        #region Usuarios
        public async Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string claveHash)
        {
            var users = Users; 
            if (string.IsNullOrEmpty(nombreUsuario) || 
                nombreUsuario.Length  is 0 || 
                !users.Any(x=>x.NombreUsuario.Trim().ToLower().Equals(nombreUsuario.Trim().ToLower())))
            {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA,"Usuario ya existe");
            }

            var user = new Usuario { NombreUsuario = nombreUsuario, ClaveHash = claveHash };
            users.Add(user);
            Session.WriteToSession(UsersKey, users);
            return await Task.FromResult(user);
        }

        public async Task<Autenticar?> AutenticarAsync(string nombreUsuario, string claveHash)
        {
            var user = Users.FirstOrDefault(u => u.NombreUsuario == nombreUsuario && u.ClaveHash == claveHash);
            var token = user == null ? null : _configuration.GenerateToken(user);
            var result = new Autenticar
            {
                Token = await Task.FromResult(token)
            };
            return result;
        }
        #endregion Usuarios

        #region Denuncias
        public async Task<Denuncia> CrearDenunciaAsync(Denuncia denuncia)
        {
            var list = Denuncias;
            denuncia.Id = Guid.NewGuid();
            denuncia.CreatedAt = DateTime.UtcNow;
            list.Add(denuncia);
            Session.WriteToSession(DenunciasKey, list);
            return await Task.FromResult(denuncia);
        }

        public async Task<Denuncia?> ObtenerDenunciaAsync(Guid id)
        {
            var d = Denuncias.FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(d);
        }

        public async Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias)
        {
            var desde = DateTime.UtcNow.AddDays(-dias);
            var result = Denuncias.Where(d => d.EsPublica && d.CreatedAt >= desde).ToList().AsEnumerable();
            return await Task.FromResult(result);
        }
        #endregion Denuncias

        #region Mensajes
        public async Task<Mensaje> EnviarMensajeAsync(Mensaje mensaje)
        {
            var list = Mensajes;
            mensaje.Id = Guid.NewGuid();
            mensaje.CreatedAt = DateTime.UtcNow;
            list.Add(mensaje);
            Session.WriteToSession(MensajesKey, list);
            return await Task.FromResult(mensaje);
        }

        public async Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId)
        {
            var result = Mensajes.Where(m => m.UsuarioDestinoId == usuarioId).ToList().AsEnumerable();
            return await Task.FromResult(result);
        }
        #endregion Mensajes

    }
}
