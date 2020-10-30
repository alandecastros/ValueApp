using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ValueApp.Domain.Interfaces;
using ValueApp.Domain.Queries;

namespace ValueApp.Domain.QueryHandlers
{
    public class ObterListaAcoesQueryHandler : IRequestHandler<ObterListaAcoesQuery, ObterListaAcoesQueryResult>
    {
        private readonly IAcoesService acoesService;

        public ObterListaAcoesQueryHandler(IAcoesService acoesService)
        {
            this.acoesService = acoesService;
        }

        public async Task<ObterListaAcoesQueryResult> Handle(ObterListaAcoesQuery request, CancellationToken cancellationToken)
        {
            var acoes = await acoesService.ObterListaAcoes();

            return new ObterListaAcoesQueryResult
            {
                Acoes = acoes
            };
        }
    }
}
