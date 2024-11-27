using LandingProject.DataContext;
using LandingProject.Models;

namespace LandingProject.Services.LeadService
{
    public class LeadService : ILeadInterface
    {
        private readonly ApplicationDbContext _context;

        public LeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<LeadModel>>> CreateLead(LeadModel novoLead)
        {
            ServiceResponse<List<LeadModel>> serviceResponse = new ServiceResponse<List<LeadModel>>();

            try
            {
                if (novoLead == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Nenhum dado encontrado";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Add(novoLead);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Leads.ToList();
                serviceResponse.Mensagem = "Cadastro realizado com sucesso!";
                serviceResponse.Sucesso = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<List<LeadModel>>> GetFiltro(string nome, string email, string curso)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<LeadModel>>> GetLead()
        {
            ServiceResponse<List<LeadModel>> serviceResponse = new ServiceResponse<List<LeadModel>>();

            try
            {
                serviceResponse.Dados = _context.Leads.ToList();

                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }


        public async Task<ServiceResponse<LeadModel>> GetLeadById(int id)
        {
            ServiceResponse<LeadModel> serviceResponse = new ServiceResponse<LeadModel>();

            try
            {
                LeadModel lead = _context.Leads.FirstOrDefault(x => x.id == id);

                if (lead == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não encontrado";
                    serviceResponse.Sucesso = false;

                }
                serviceResponse.Dados = lead;

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
