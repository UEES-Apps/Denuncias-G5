using Microsoft.AspNetCore.Mvc;
using G5.Denuncias.BE.Api.Services.Interface;
using G5.Denuncias.BE.Api.Services.Models;
using G5.Denuncias.BE.Domain.Models.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Api.Controllers
{

    [ApiController]
    [Tags("Buzon")]
    
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BuzonController : Controller
    {
        private readonly ILogger<BuzonController> _log;
        private readonly IDenunciasService _service;

        public BuzonController(ILogger<BuzonController> log, IDenunciasService service)
        {
            _log = log;
            _service = service;
        }

        #region Mensajes

        /// <summary>
        /// Obtener Mensajes de usuario
        /// </summary>
        [HttpGet("/mensaje/v1/obtener")]
        [ProducesResponseType(typeof(IEnumerable<Mensaje>), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerMensajesUsuario([FromHeader(Name = "DenunciaId")] Guid denunciaId)
        {
            using (_log.BeginScope("Obtener mensajes Usuario --> {request}", denunciaId))
            {
                var response = await _service.ObtenerMensajesUsuarioAsync(denunciaId);
                return Ok(response);
            }
        }

        /// <summary>
        /// Enviar Mensaje
        /// </summary>
        [HttpPost("/mensaje/v1/enviar")]
        [ProducesResponseType(typeof(Mensaje), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EnviarMensaje([FromBody] EnviarMensajeInput body)
        {
            using (_log.BeginScope("Enviar Mensaje --> {request}", body))
            {
                var response = await _service.EnviarMensajeAsync(body);
                return Ok(response);
            }
        }

        #endregion Mensajes

    }
}
