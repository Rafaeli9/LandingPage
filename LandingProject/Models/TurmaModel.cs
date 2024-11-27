using LandingProject.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LandingProject.Models
{
    public class TurmaModel
    {
        [Key]
        public int TurmaId { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int CursoId { get; set; } 
        [JsonIgnore]
        public CursoModel Curso { get; set; }
        [JsonIgnore]
        public List<AlunoModel> Alunos { get; set; } 
    }

}

