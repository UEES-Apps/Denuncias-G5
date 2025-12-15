using G5.Denuncias.BE.Domain.Denuncias.Entities;
using G5.Denuncias.BE.Domain.Denuncias;
using G5.Denuncias.BE.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using G5.Denuncias.BE.Infraestructure.Token;
using G5.Denuncias.BE.Domain.Models;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;

namespace G5.Denuncias.BE.Infraestructure.Repository.Denuncias
{
    public class EfDenunciaRepository(DenunciasDbContext contexto, IConfiguration configuration) : IDenunciaRepository
    {
        private readonly DenunciasDbContext _contexto = contexto;

        private readonly IConfiguration _configuration = configuration;

        #region Usuario
        public async Task<Usuario> RegistrarUsuarioAsync(string nombreUsuario, string claveHash)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || nombreUsuario.Length is 0)
            {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, "Nombre de usuario no válido!");
            }

            if (_contexto.Usuarios.Any(x => x.NombreUsuario.Trim().ToLower().Equals(nombreUsuario.Trim().ToLower())))
            {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, $"Nombre de usuario '{nombreUsuario}' ya encuentra registrado!");
            }

            var user = new Usuario { NombreUsuario = nombreUsuario, ClaveHash = claveHash };
            _contexto.Usuarios.Add(user);
            await _contexto.SaveChangesAsync();
            return user;
        }

        public async Task<Autenticar?> AutenticarAsync(string nombreUsuario, string claveHash)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || nombreUsuario.Length is 0)
            {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, "Nombre de usuario no válido!");
            }

            if (!_contexto.Usuarios.Any(x => x.NombreUsuario.Trim().ToLower().Equals(nombreUsuario.Trim().ToLower())))
            {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, $"Usuario '{nombreUsuario}' no se encuentra registrado!");
            }

            var user = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario && u.ClaveHash == claveHash);
            var token = user == null ? null : _configuration.GenerateToken(user);
            var result = new Autenticar
            {
                Token = await Task.FromResult(token)
            };
            return result;
        }
        #endregion Usuario

        #region Denuncias
        public async Task<Denuncia> CrearDenunciaAsync(Denuncia denuncia)
        {
            _contexto.Denuncias.Add(denuncia);
            await _contexto.SaveChangesAsync();
            return denuncia;
        }

        public async Task<Denuncia?> ObtenerDenunciaAsync(Guid id)
        {
            return await _contexto.Denuncias.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias)
        {
            var desde = DateTime.UtcNow.AddDays(-dias);
            return await _contexto.Denuncias.Where(d => d.EsPublica && d.CreatedAt >= desde).ToListAsync();
        }
        #endregion Denuncias

        #region Mensajes
        public async Task<Mensaje> EnviarMensajeAsync(Mensaje mensaje)
        {
            _contexto.Mensajes.Add(mensaje);
            await _contexto.SaveChangesAsync();
            return mensaje;
        }

        public async Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId)
        {
            return await _contexto.Mensajes.Where(m => m.UsuarioDestinoId == usuarioId).ToListAsync();
        }
        #endregion Mensajes
    }
}
