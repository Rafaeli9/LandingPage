using LandingProject.Models;
using LandingProject.Services.FiltroService;
using Microsoft.AspNetCore.Mvc;

namespace LandingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltroController : ControllerBase
    {
        private readonly IFiltroInterface _filtroInterface;

        public FiltroController(IFiltroInterface filtroInterface)
        {
            _filtroInterface = filtroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<LeadModel>>>> GetFiltro(
            [FromQuery] string? nome = null,
            [FromQuery] string? email = null,
            [FromQuery] string? curso = null)
        {
            var response = await _filtroInterface.GetFiltro(nome, email, curso);
            return Ok(response);
        }


        //[HttpGet]
        //public async Task<ActionResult<ServiceResponse<List<LeadModel>>>> GetFiltro(
        //    [FromQuery] string nome,
        //    [FromQuery] string email,
        //    [FromQuery] string curso)
        //{
        //    var response = await _filtroInterface.GetFiltro(nome, email, curso);
        //    return Ok(response);
        //}


        //[HttpGet("{id}")]
        //public async Task<ActionResult<ServiceResponse<LeadModel>>> GetLeadById(int id)
        //{
        //    var serviceResponse = await _leadInterface.GetLeadById(id);
        //    return Ok(serviceResponse);
        //}
    }
}

