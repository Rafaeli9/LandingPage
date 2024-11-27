using LandingProject.DataContext;
using LandingProject.DTOs;
using LandingProject.Models;
using LandingProject.Services.AlunoService;
using LandingProject.Services.LeadService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LandingProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IAlunoInterface _alunoInterface;
   

        public AlunoController(IAlunoInterface alunoInterface)
        {
            _alunoInterface = alunoInterface;
        }


        [HttpPost]
        public async Task<ActionResult> CreateAluno(AlunoCriacaoDto aluno)
        {
            return Ok(await _alunoInterface.CreateAluno(aluno));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<AlunoModel>>>> GetAlunos()
        {
            return Ok(await _alunoInterface.GetAlunos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlunoById([FromBody] int id)
        {
            return Ok(await _alunoInterface.GetAlunoById(id));

        }

        [HttpGet("cursos/{cursoId}/turmas")]
        public async Task<ActionResult<List<TurmaModel>>> GetTurmasByCurso(int cursoId)
        {
            return Ok(await _alunoInterface.GetTurmasByCurso(cursoId));
        }

        [HttpGet("cursos")]
        public async Task<ActionResult<List<CursoModel>>> GetCursos()
        {
            return Ok(await _alunoInterface.GetCursos());
        }


        [HttpGet("cursos/{cursoId}")]
        public async Task<ActionResult<ServiceResponse<CursoModel>>> GetCursoId(int cursoId)
        {
            ServiceResponse<CursoModel> serviceResponse = await _alunoInterface.GetCursoId(cursoId);
            return Ok(serviceResponse);
        }

    }
}



