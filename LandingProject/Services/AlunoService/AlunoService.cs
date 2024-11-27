using LandingProject.DataContext;
using LandingProject.DTOs;
using LandingProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


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

                var ultimoAluno = await _context.Alunos.OrderByDescending(a => a.AlunoId).FirstOrDefaultAsync();
                var codigoMatricula = (ultimoAluno != null ? ultimoAluno.AlunoId + 1 : 1);

                var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Descricao == aluno.Curso.Descricao);
                if (curso == null)
                {
                    curso = new CursoModel { Descricao = aluno.Curso.Descricao };
                    _context.Cursos.Add(curso);
                    await _context.SaveChangesAsync(); // Garante que CursoId seja gerado
                }

                var turma = await _context.Turmas.FirstOrDefaultAsync(t => t.Descricao == aluno.Turma.Descricao);
                if (turma == null)
                {
                    turma = new TurmaModel { Descricao = aluno.Turma.Descricao };
                    _context.Turmas.Add(turma);
                    await _context.SaveChangesAsync(); // Garante que TurmaId seja gerado
                }

                var novoAluno = new AlunoModel
                {
                    Nome = aluno.Nome,
                    Telefone = aluno.Telefone,
                    Email = aluno.Email,
                    CodigoMatricula = codigoMatricula,
                    CursoId = curso.CursoId, // Associa o CursoId
                    TurmaId = turma.TurmaId, // Associa o TurmaId
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
