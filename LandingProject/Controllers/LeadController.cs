using LandingProject.Models;
using LandingProject.Services.LeadService;
using Microsoft.AspNetCore.Mvc;

namespace LandingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadInterface _leadInterface;
        public LeadController(ILeadInterface leadInterface)
        {
            _leadInterface = leadInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<LeadModel>>>> GetLead()
        {
            return Ok(await _leadInterface.GetLead());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<LeadModel>>> GetLeadById(int id)
        {
            ServiceResponse<LeadModel> serviceResponse = await _leadInterface.GetLeadById(id);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<LeadModel>>>> CreateLead([FromBody] LeadModel novoLead)
        {
            return Ok(await _leadInterface.CreateLead(novoLead));
        }

    }
}
