using Google.Apis.Services;
using Google.Apis.Sheets.v4;
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

        public AcoesService(string googleAppName, string googleAppApiKey)
        {
            this.googleAppName = googleAppName;
            this.googleAppApiKey = googleAppApiKey;
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
                    var preco = Convert.ToDouble(row[1]);
                    var lpa = Convert.ToDouble(row[2]);
                    acoes.Add(new Acao
                    {
                        Simbolo = (string)row[0],
                        Preco = preco,
                        Lpa = lpa
                    });
                }
            }

            return acoes;
        }
    }
}
