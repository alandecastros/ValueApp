using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;
using ValueApp.Domain.Queries;

namespace ValueApp.Api.Functions
{
    public class ObterListaAcoesFunction
    {
        private readonly IMediator mediator;

        public ObterListaAcoesFunction(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [FunctionName("ObterListaAcoes")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            try
            {
                var result = await mediator.Send(new ObterListaAcoesQuery());
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new { error = e.Message });
            }
        }
    }
}
