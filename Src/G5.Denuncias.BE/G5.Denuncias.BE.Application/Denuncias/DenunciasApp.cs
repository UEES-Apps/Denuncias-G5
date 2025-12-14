using G5.Denuncias.BE.Domain.Denuncias;
using G5.Denuncias.BE.Domain.Denuncias.Entities;
using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Models;
using static G5.Denuncias.BE.Domain.Models.Dtos.ErroresEnum;

namespace G5.Denuncias.BE.Application.Denuncias
{
    public class DenunciasApp(IDenunciaRepository repository) : IDenunciasApp
    {
        private readonly IDenunciaRepository _repository = repository;

        #region Usuarios
        public async Task<Usuario> RegistrarUsuarioAsync(RegistrarUsuarioDtoIn request)
        {
            var response = await _repository.RegistrarUsuarioAsync(request.NombreUsuario, request.ClaveHash);
            return response;
        }

        public async Task<Autenticar?> AutenticarAsync(AutenticarDtoIn request)
        {
            var response = await _repository.AutenticarAsync(request.NombreUsuario, request.ClaveHash);
            if (response is null || response.Token is null) {
                throw new CustomException(TipoErrorEnum.SOLICITUD_INVALIDA, "No autenticado");
            }
            return response;
        }
        #endregion Usuarios

        #region Denuncias
        public async Task<Denuncia> CrearDenunciaAsync(CrearDenunciaDtoIn request)
        {
            var response = await _repository.CrearDenunciaAsync(request);
            return response;
        }

        public async Task<Denuncia?> ObtenerDenunciaAsync(Guid id)
        {
            var response = await _repository.ObtenerDenunciaAsync(id);
            return response;
        }

        public async Task<IEnumerable<Denuncia>> ObtenerDenunciasPublicasUltimosDiasAsync(int dias)
        {
            var response = await _repository.ObtenerDenunciasPublicasUltimosDiasAsync(dias);
            return response;
        }
        #endregion Denuncias

        #region Mensajes
        public async Task<Mensaje> EnviarMensajeAsync(EnviarMensajeDtoIn mensaje)
        {
            var response = await _repository.EnviarMensajeAsync(mensaje);
            return response;
        }

        public async Task<IEnumerable<Mensaje>> ObtenerMensajesUsuarioAsync(Guid usuarioId)
        {
            var response = await _repository.ObtenerMensajesUsuarioAsync(usuarioId);
            return response;
        }
        #endregion Mensajes

    }
}
