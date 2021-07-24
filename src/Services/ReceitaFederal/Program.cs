using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReceitaFederal.Domain.Models;
using ReceitaFederal.Domain.Repositories;
using ReceitaFederal.HostService;
using ReceitaFederal.Services;
using Serilog;

namespace ReceitaFederal
{
    class Program
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
                    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
                    
                    services.AddHostedService<ReceitaFederalHostService>();
                    services.AddTransient<ReceitaFederalService>();
                    services.AddTransient<SituacaoCadastralService>();
                    
                    services.AddTransient<EmpresaRepository>();
                    services.AddTransient<SocioRepository>();
                    services.AddTransient<MotivoSituacaoCadastralRepository>();
                    services.AddTransient<ReceitaFederalArquivoRepository>();
                    
                    services.AddTransient<AgenteRepository>();

                    services.AddDbContext<ReceitaFederalDbContext>(options =>
                    {
                        options.UseLazyLoadingProxies(useLazyLoadingProxies: false);
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("ReceitaFederal"),
                            m => m.MigrationsAssembly(typeof(ReceitaFederalDbContext)
                                .GetTypeInfo().Assembly.GetName().Name));
                    }, ServiceLifetime.Transient);
                    
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
                    configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                    configLogging.AddSerilog();

                    var logs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "logs-{Date}.txt");
                    configLogging.AddFile(logs);
                })
                .UseConsoleLifetime()
                .Build();

            await host.RunAsync();
        }
    }
}