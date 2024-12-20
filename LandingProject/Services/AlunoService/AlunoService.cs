using LandingProject.DataContext;
using LandingProject.DTOs;
using LandingProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LandingProject.Services.AlunoService
{
    public class AlunoService : IAlunoInterface
    {
        private readonly ApplicationDbContext _context;

        public AlunoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<AlunoModel>>> CreateAluno(AlunoCriacaoDto aluno)
        {
            var serviceResponse = new ServiceResponse<List<AlunoModel>>();

            try
            {
                if (aluno == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Gera o código de matrícula
                var codigoMatricula = await GerarCodigoMatricula();

                // Verifica o curso
                var curso = await ObterCurso(aluno.Curso.Descricao, aluno.Curso.CursoId);
                if (curso == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Curso não encontrado.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Verifica a turma
                var turma = await ObterTurma(aluno.Turma.Descricao, curso.CursoId);
                if (turma == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Turma não encontrada para o curso.";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                // Cria o aluno e associa o CursoId e o TurmaId
                var novoAluno = new AlunoModel
                {
                    Nome = aluno.Nome,
                    Telefone = aluno.Telefone,
                    Email = aluno.Email,
                    CodigoMatricula = codigoMatricula,
                    CursoId = curso.CursoId,
                    TurmaId = turma.TurmaId,
                    DataCadastro = DateTime.UtcNow,
                };

                _context.Alunos.Add(novoAluno);
                await _context.SaveChangesAsync();

                // Retorna todos os alunos com cursos e turmas relacionados
                serviceResponse.Dados = await _context.Alunos
                    .Include(a => a.Cursos)
                    .Include(a => a.Turmas)
                    .ToListAsync();

                serviceResponse.Mensagem = "Aluno cadastrado com sucesso!";
                serviceResponse.Sucesso = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        private async Task<int> GerarCodigoMatricula()
        {
            var ultimoAluno = await _context.Alunos
                .OrderByDescending(a => a.AlunoId)
                .FirstOrDefaultAsync();

            return (ultimoAluno != null ? ultimoAluno.CodigoMatricula + 1 : 1);
        }


        private async Task<CursoModel?> ObterCurso(string descricaoCurso, int cursoId)
        {
            return await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == descricaoCurso && c.CursoId == cursoId);
        }

        private async Task<TurmaModel?> ObterTurma(string descricaoTurma, int cursoId)
        {
            return await _context.Turmas
                .FirstOrDefaultAsync(t => t.Descricao == descricaoTurma && t.CursoId == cursoId);
        }

        public async Task<ServiceResponse<List<AlunoModel>>> GetAlunos()
        {
            ServiceResponse<List<AlunoModel>> serviceResponse = new ServiceResponse<List<AlunoModel>>();

            try
            {
                serviceResponse.Dados = _context.Alunos.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum aluno encontrado!";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<AlunoModel>> GetAlunoById(int id)
        {
            ServiceResponse<AlunoModel> serviceResponse = new ServiceResponse<AlunoModel>();

            try
            {
                var aluno = await _context.Alunos
                    .Include(a => a.Cursos)
                    .Include(a => a.Turmas)
                    .FirstOrDefaultAsync(a => a.AlunoId == id);

                if (aluno == null)
                {
                    serviceResponse.Mensagem = "Aluno não encontrado!";
                    serviceResponse.Sucesso = false;
                    serviceResponse.Dados = null;
                }
                else
                {
                    serviceResponse.Dados = aluno;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<TurmaModel>>> GetTurmasByCurso(int cursoId)
        {
            var serviceResponse = new ServiceResponse<List<TurmaModel>>();

            try
            {
                var turmas = await _context.Turmas
                    .Where(t => t.Curso.CursoId == cursoId)
                    .ToListAsync();

                if (turmas == null || !turmas.Any())
                {
                    serviceResponse.Mensagem = "Nenhuma turma encontrada para o curso especificado.";
                    serviceResponse.Sucesso = false;
                    serviceResponse.Dados = null;
                }
                else
                {
                    serviceResponse.Mensagem = "Turmas encontradas com sucesso.";
                    serviceResponse.Sucesso = true;
                    serviceResponse.Dados = turmas;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = $"Erro ao buscar turmas: {ex.Message}";
                serviceResponse.Sucesso = false;
                serviceResponse.Dados = null;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CursoModel>>> GetCursos()
        {
            ServiceResponse<List<CursoModel>> serviceResponse = new ServiceResponse<List<CursoModel>>();

            try
            {
                serviceResponse.Dados = _context.Cursos.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum aluno encontrado!";

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<CursoModel>> GetCursoId(int cursoId)
        {
            ServiceResponse<CursoModel> serviceResponse = new ServiceResponse<CursoModel>();

            try
            {
                CursoModel curso = _context.Cursos.FirstOrDefault(x => x.CursoId == cursoId);

                if (curso == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não encontrado";
                    serviceResponse.Sucesso = false;

                }
                serviceResponse.Dados = curso;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }    

    }
}
