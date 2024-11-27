using LandingProject.Models;

namespace LandingProject.Services.LeadService
{
    public interface ILeadInterface
    {
        Task<ServiceResponse<List<LeadModel>>> GetLead();
        Task<ServiceResponse<List<LeadModel>>> GetFiltro(string nome, string email, string curso);
        Task<ServiceResponse<List<LeadModel>>> CreateLead(LeadModel novoLead);
        Task<ServiceResponse<LeadModel>> GetLeadById(int id);
    }
}
