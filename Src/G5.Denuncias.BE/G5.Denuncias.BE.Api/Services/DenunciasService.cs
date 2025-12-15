using G5.Denuncias.BE.Api.Services.Interface;
using G5.Denuncias.BE.Api.Services.Models;
using G5.Denuncias.BE.Application.Denuncias;
using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Api.Services
{
    public class DenunciasService(IDenunciasApp application) : IDenunciasService
    {
        private readonly IDenunciasApp _application = application;

        #region Usuarios
        public async Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioInput request)
        {
            var req = new RegistrarUsuarioDtoIn
            {
                NombreUsuario = request.NombreUsuario,
                ClaveHash = request.ClaveHash
            };

            var response = await _application.RegistrarUsuarioAsync(req);
            return response;
        }

        public async Task<Autenticar?> AutenticarAsync(AutenticarInput request)
        {
            var req = new AutenticarDtoIn
            {
                NombreUsuario = request.NombreUsuario,
                ClaveHash = request.ClaveHash
            };

            var response = await _application.AutenticarAsync(req);
            return response;
        }
        #endregion Usuarios

        #region Denuncias
        public async Task<Denuncia> CrearDenunciaAsync(CrearDenunciaInput request)
        {
            var req = new CrearDenunciaDtoIn
            {
                Id = request.Id,
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                FechaEvento = request.FechaEvento,
                CiudadProvincia = request.CiudadProvincia,
                EsPublica = request.EsPublica,
                Tipo = request.Tipo,
                Usuario = request.Usuario,
                CreatedAt = request.CreatedAt
            };

            var response = await _application.CrearDenunciaAsync(req);
            return response;
        }

        public async Task<IEnumerable<Denuncia>> ObtenerDenunciasAsync()
        {
            var response = await _application.ObtenerDenunciasAsync();
            return response;
        }

        public async Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasAsync()
        {
            var response = await _application.ObtenerDenunciasPublicasAsync();
            return response;
        }
        #endregion Denuncias

        #region Mensajes
        public async Task<Mensaje> EnviarMensajeAsync(EnviarMensajeInput request)
        {
            var req = new EnviarMensajeDtoIn
            {
                Id = request.Id,
                Remitente = request.Remitente,
                UsuarioDestino = request.UsuarioDestino,
                DenunciaId = request.DenunciaId,
                Contenido = request.Contenido,
                CreatedAt = request.CreatedAt
            };

            var response = await _application.EnviarMensajeAsync(req);
            return response;
        }

        public async Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid denunciaId)
        {
            var response = await _application.ObtenerMensajesUsuarioAsync(denunciaId);
            return response;
        }
        #endregion Mensajes

    }
}
