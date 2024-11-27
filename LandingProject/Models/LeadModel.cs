using System.ComponentModel.DataAnnotations;

namespace LandingProject.Models
{
    public class LeadModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Telefone é obrigatório.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo Curso é obrigatório.")]
        public string Curso { get; set; }
        public string Turma { get; set; }
     
    }

}
