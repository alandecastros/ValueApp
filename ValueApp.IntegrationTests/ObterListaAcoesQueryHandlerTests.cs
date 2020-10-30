using MediatR;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using ValueApp.Api;
using ValueApp.Domain.Interfaces;
using ValueApp.Domain.Queries;
using ValueApp.Infra.Services;
using Xunit;

namespace ValueApp.IntegrationTests
{
    public class ObterListaAcoesQueryHandlerTests : IDisposable
    {
        private const string GOOGLE_APP_NAME = "YOUR APP NAME";
        private const string GOOGLE_APP_API_KEY = "YOUR APP API KEY";

        private IHost host;
        private IRequestHandler<ObterListaAcoesQuery, ObterListaAcoesQueryResult> handler;

        public ObterListaAcoesQueryHandlerTests()
        {
            var startup = new Startup();
            host = new HostBuilder()
                .ConfigureWebJobs(builder =>
                {
                    builder.Services.Configure<ExecutionContextOptions>(o => o.AppDirectory = Directory.GetCurrentDirectory());
                    startup.Configure(builder);
                })
                .ConfigureServices(services =>
                {
                    var acaoServiceDescriptor = new ServiceDescriptor(
                        typeof(IAcoesService),
                        s => new AcoesService(googleAppName: GOOGLE_APP_NAME, googleAppApiKey: GOOGLE_APP_API_KEY),
                        ServiceLifetime.Scoped);

                    services.Replace(acaoServiceDescriptor);
                })
                .Build();

            handler = host.Services.GetRequiredService<IRequestHandler<ObterListaAcoesQuery, ObterListaAcoesQueryResult>>();
        }

        public void Dispose()
        {
            host.Dispose();
        }

        [Fact]
        public async Task Ok()
        {
            // Act
            var result = await handler.Handle(new ObterListaAcoesQuery(), default);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Acoes.Count > 0);
        }
    }
}
