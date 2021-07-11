using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using InfoMercado.HostService;
using InfoMercado.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfoMercado
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: false);
                    configApp.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<InfoMercadoHostService>();

                    services.AddSingleton<InfomercadoArquivoService>();
                    services.AddSingleton<Infomercado001Contratos>();
                    services.AddSingleton<Infomercado002Usinas>();
                    services.AddSingleton<Infomercado007PerfisAgentes>();
                    
                    services.AddTransient<InfomercadoArquivoRepository>();
                    services.AddTransient<ContratoRepository>();
                    services.AddTransient<AgenteRepository>();
                    services.AddTransient<PerfilAgenteRepository>();
                    services.AddTransient<UsinaRepository>();
                    services.AddTransient<ParcelaUsinaRepository>();
                    services.AddTransient<DadosGeracaoUsinaRepository>();

                    services.AddDbContext<InfoMercadoDbContext>(options =>
                    {
                        options.UseLazyLoadingProxies(useLazyLoadingProxies: false);
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("InfoMercado"),
                            m => m.MigrationsAssembly(typeof(InfoMercadoDbContext)
                                .GetTypeInfo().Assembly.GetName().Name));
                    }, ServiceLifetime.Transient);

                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }
    }
}