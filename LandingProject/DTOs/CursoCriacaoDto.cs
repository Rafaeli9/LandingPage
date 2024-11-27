using System.ComponentModel.DataAnnotations;
using LandingProject.Models;
using LandingProject.Services.AlunoService;
using Microsoft.AspNetCore.Mvc;

namespace LandingProject.DTOs
{
    public class CursoCriacaoDto
    {
        [Required]
        public string Descricao { get; set; }

        public int CursoId { get; set; }
    }
}
