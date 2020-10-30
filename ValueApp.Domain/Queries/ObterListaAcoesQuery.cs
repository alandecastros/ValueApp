using Asc.MediatR.CQRS;
using System.Collections.Generic;
using ValueApp.Domain.Models;

namespace ValueApp.Domain.Queries
{
    public class ObterListaAcoesQuery : IQuery<ObterListaAcoesQueryResult>
    {
    }

    public class ObterListaAcoesQueryResult
    {
        public ObterListaAcoesQueryResult()
        {
            Acoes = new List<Acao>();
        }

        public IList<Acao> Acoes { get; set; }
    }
}
