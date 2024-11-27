using LandingProject.Models;

namespace LandingProject.Services.FiltroService
{
    public interface IFiltroInterface
    {
        Task<ServiceResponse<List<LeadModel>>> GetFiltro(string? nome, string? email, string? curso);
        Task<ServiceResponse<FiltroModel>> GetLeadById(int id);
    }
}
