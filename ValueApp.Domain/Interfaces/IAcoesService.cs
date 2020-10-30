using System.Collections.Generic;
using System.Threading.Tasks;
using ValueApp.Domain.Models;

namespace ValueApp.Domain.Interfaces
{
    public interface IAcoesService
    {
        Task<IList<Acao>> ObterListaAcoes();
    }
}
