using Microsoft.AspNetCore.Mvc;
using G5.Denuncias.BE.Api.Services.Interface;
using G5.Denuncias.BE.Api.Services.Models;
using G5.Denuncias.BE.Domain.Models.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Dtos;
using G5.Denuncias.BE.Domain.Denuncias.Entities;

namespace G5.Denuncias.BE.Api.Controllers
{

    [ApiController]
    [Tags("Denuncias")]
    
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DenunciasController : Controller
    {
        private readonly ILogger<DenunciasController> _log;
        private readonly IDenunciasService _service;

        public DenunciasController(ILogger<DenunciasController> log, IDenunciasService service)
        {
            _log = log;
            _service = service;
        }

        #region Denuncias

        /// <summary>
        /// Crear denuncia
        /// </summary>
        [HttpPost("/denuncia/v1/crear")]
        [ProducesResponseType(typeof(Denuncia), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearDenuncia([FromBody] CrearDenunciaInput body)
        {
            using (_log.BeginScope("Crear denuncia --> {request}", body))
            {
                var response = await _service.CrearDenunciaAsync(body);
                return Ok(response);
            }
        }

        /// <summary>
        /// Obtener denuncia
        /// </summary>
        [HttpGet("/denuncia/v1/obtener")]
        [ProducesResponseType(typeof(IEnumerable<Denuncia>), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerDenuncias()
        {
            using (_log.BeginScope("Obtener denuncias"))
            {
                var response = await _service.ObtenerDenunciasAsync();
                return Ok(response);
            }
        }

        /// <summary>
        /// Obtener denuncias publicas
        /// </summary>
        [HttpGet("/denuncia/v1/denunciaspublicas/obtener")]
        [ProducesResponseType(typeof(IEnumerable<Denuncia>), StatusCodes.Status200OK)]

        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErroresDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObtenerDenunciasPublicas()
        {
            using (_log.BeginScope("Obtener denuncias publicas"))
            {
                var response = await _service.ObtenerDenunciasPublicasAsync();
                return Ok(response);
            }
        }

        #endregion Denuncias

    }
}
