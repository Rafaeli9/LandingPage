using LandingProject.Enums;
using LandingProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LandingProject.Models
{
    public class CursoModel
    {
        [Key]
        public int CursoId { get; set; }
        [Required]
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<AlunoModel> Alunos { get; set; } 
    }
}


