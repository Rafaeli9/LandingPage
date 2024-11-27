using LandingProject.DataContext;
using LandingProject.Models;
using Microsoft.EntityFrameworkCore;

namespace LandingProject.Services.FiltroService

{
    public class FiltroService : IFiltroInterface
    {
        private readonly ApplicationDbContext _context;

        public FiltroService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<LeadModel>>> GetFiltro(string nome, string email, string curso)
        {
            var serviceResponse = new ServiceResponse<List<LeadModel>>();

            try
            {
                var query = _context.Leads.AsQueryable();

                if (!string.IsNullOrEmpty(nome))
                    query = query.Where(x => x.Nome.Contains(nome));

                if (!string.IsNullOrEmpty(email))
                    query = query.Where(x => x.Email.Contains(email));

                if (!string.IsNullOrEmpty(curso))
                    query = query.Where(x => x.Curso.Contains(curso));

                serviceResponse.Dados = await query.ToListAsync();
                serviceResponse.Mensagem = serviceResponse.Dados.Count > 0 ? "Leads encontrados." : "Nenhum lead encontrado.";
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<FiltroModel>> GetLeadById(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<ServiceResponse<FiltroModel>> GetFiltroById(int id)
        //{
        //    ServiceResponse<FiltroModel> serviceResponse = new ServiceResponse<FiltroModel>();

        //    try
        //    {
        //        //FiltroModel filtro = _context.Filtro.FirstOrDefault(x => x.id == id);

        //        if (filtro == null)
        //        {
        //            serviceResponse.Dados = null;
        //            serviceResponse.Mensagem = "Usuário não encontrado";
        //            serviceResponse.Sucesso = false;

        //        }
        //        serviceResponse.Dados = filtro;

        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.Mensagem = ex.Message;
        //        serviceResponse.Sucesso = false;
        //    }

        //    return serviceResponse;
        //}
    }
}

