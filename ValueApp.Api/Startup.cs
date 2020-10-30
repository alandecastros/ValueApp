using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;
using ValueApp.Domain.Interfaces;
using ValueApp.Infra.Services;

[assembly: FunctionsStartup(typeof(ValueApp.Api.Startup))]
namespace ValueApp.Api
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; private set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var serviceProvider = builder.Services.BuildServiceProvider();
            Configuration = serviceProvider.GetService<IConfiguration>();

            builder.Services.AddMediatR(Assembly.Load("ValueApp.Domain"));

            // Services
            builder.Services.AddScoped<IAcoesService>(s => new AcoesService(
                googleAppName: Configuration["GOOGLE_APP_NAME"],
                googleAppApiKey: Configuration["GOOGLE_APP_API_KEY"]));
        }
    }
}
