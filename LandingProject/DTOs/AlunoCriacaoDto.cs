using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LandingProject.DataContext;
using LandingProject.Models;
using LandingProject.Services.AlunoService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LandingProject.DTOs
{
    public class AlunoCriacaoDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public int CodigoMatricula { get; set; }
        public CursoCriacaoDto Curso { get; set; }
        public TurmaCriacaoDto Turma { get; set; }
    }
}
