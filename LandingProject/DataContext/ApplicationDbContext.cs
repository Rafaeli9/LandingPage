using LandingProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LandingProject.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<LeadModel> Leads { get; set; }
    }
}
