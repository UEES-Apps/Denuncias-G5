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



    }
}
