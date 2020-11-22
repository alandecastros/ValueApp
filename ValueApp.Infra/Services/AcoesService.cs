using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValueApp.Domain.Interfaces;
using ValueApp.Domain.Models;

namespace ValueApp.Infra.Services
{
    public class AcoesService : IAcoesService
    {
        private readonly string googleAppName;
        private readonly string googleAppApiKey;
        private readonly ILogger<AcoesService> logger;

        public AcoesService(IConfiguration configuration, ILogger<AcoesService> logger)
        {
            googleAppName = configuration["GOOGLE_APP_NAME"];
            googleAppApiKey = configuration["GOOGLE_APP_API_KEY"];
            this.logger = logger;
        }

        public async Task<IList<Acao>> ObterListaAcoes()
        {
            var acoes = new List<Acao>();

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                ApiKey = googleAppApiKey,
                ApplicationName = googleAppName,
            });

            var spreadsheetId = "1drWOfRu1-ZxEfTYUvGKyzYRq_B7l88gu-l_1jK3PYSw";
            var range = "ACOES!A2:C100";
            var request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            var response = await request.ExecuteAsync();
            var values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    try
                    {
                        var preco = Convert.ToDouble(row[1]);
                        var lpa = Convert.ToDouble(row[2]);
                        acoes.Add(new Acao
                        {
                            Simbolo = (string)row[0],
                            Preco = preco,
                            Lpa = lpa
                        });
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"Erro na linha: {row[0]}, {row[1]}, {row[2]}");
                        logger.LogError(e.Message, e);
                    }
                }
            }

            return acoes;
        }
    }
}
