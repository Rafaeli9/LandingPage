using System.ComponentModel.DataAnnotations;

namespace LandingProject.DTOs
{
    public class TurmaCriacaoDto
    {
        [Required]
        public string Descricao { get; set; }
        public int TurmaId { get; set; }
    }
}
