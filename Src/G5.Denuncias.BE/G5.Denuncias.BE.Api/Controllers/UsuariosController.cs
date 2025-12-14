using Microsoft.AspNetCore.Mvc;
using G5.Denuncias.BE.Api.Services.Interface;
using G5.Denuncias.BE.Api.Services.Models;
using G5.Denuncias.BE.Domain.Models.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Api.Controllers
{
    [ApiController]
    [Tags("Usuarios")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _log;
        private readonly IDenunciasService _service;

        public UsuariosController(ILogger<UsuariosController> log, IDenunciasService service)
        {
            _log = log;
            _service = service;
        }

        #region Usuarios

        /// <summary>
        /// Autenticar usuario
        /// </summary>
        [HttpPost("/usuario/v1/autenticar")]
        [ProducesResponseType(typeof(Autenticar), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] AutenticarInput body)
        {
            using (_log.BeginScope("Autenticar usuario --> {request}", body))
            {
                var response = await _service.AutenticarAsync(body);
                return Ok(response);
            }
        }

        /// <summary>
        /// Registrar usuario
        /// </summary>
        [HttpPost("/usuario/v1/registrar")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioInput body)
        {
            using (_log.BeginScope("Registrar usuario --> {request}", body))
            {
                var response = await _service.RegistrarUsuarioAsync(body);
                return Ok(response);
            }
        }

        #endregion Usuarios

    }
}
