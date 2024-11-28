
using LandingProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LandingProject.Models
{
    public class AlunoModel
    {
        [Key]
        public int AlunoId { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int CursoId { get; set; }
        [Required]
        public int TurmaId { get; set; }
        public int CodigoMatricula { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<CursoModel> Cursos { get; set; }
        public List<TurmaModel> Turmas { get; set; }
    }


}
