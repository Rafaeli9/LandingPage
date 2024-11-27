using LandingProject.DTOs;
using LandingProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LandingProject.Services.AlunoService
{
    public interface IAlunoInterface
    {
        Task<ServiceResponse<List<AlunoModel>>> GetAlunos();
        Task<ServiceResponse<AlunoModel>> GetAlunoById(int id);
        Task<ServiceResponse<List<AlunoModel>>> CreateAluno(AlunoCriacaoDto aluno);
        Task<ServiceResponse<List<TurmaModel>>> GetTurmasByCurso(int cursoId);
        Task<ServiceResponse<List<CursoModel>>> GetCursos();
        Task<ServiceResponse<CursoModel>> GetCursoId(int cursoId);

    }
}
