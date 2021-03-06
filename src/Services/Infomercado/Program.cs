using System;
using System.Globalization;
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
using Serilog;

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
                    CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
                    
                    services.AddHostedService<InfoMercadoHostService>();

                    services.AddSingleton<InfomercadoArquivoService>();
                    services.AddSingleton<Infomercado001Contratos>();
                    services.AddSingleton<Infomercado002Usinas>();
                    services.AddSingleton<Infomercado003Consumo>();
                    services.AddSingleton<Infomercado004Contabilizacao>();
                    services.AddSingleton<Infomercado005Encargos>();
                    services.AddSingleton<Infomercado006Mre>();
                    services.AddSingleton<Infomercado007PerfisAgentes>();
                    services.AddSingleton<Infomercado008Cotista>();
                    services.AddSingleton<Infomercado009Proinfa>();
                    services.AddSingleton<Infomercado011RRH>();
                    services.AddSingleton<Infomercado0012DisponibilidadeLeilao>();
                    
                    services.AddTransient<InfomercadoArquivoRepository>();
                    services.AddTransient<ContratoRepository>();
                    services.AddTransient<AgenteRepository>();
                    services.AddTransient<PerfilAgenteRepository>();
                    services.AddTransient<UsinaRepository>();
                    services.AddTransient<ParcelaUsinaRepository>();
                    services.AddTransient<DadosGeracaoUsinaRepository>();
                    services.AddTransient<ContabilizacaoRepository>();
                    services.AddTransient<EncargoRepository>();
                    services.AddTransient<DadoMreRepository>();
                    services.AddTransient<LiquidacaoDistribuidoraCotistaGarantiaFisicaRepository>();
                    services.AddTransient<ReceitaVendaDistribuidoraCotistaGarantiaFisicaRepository>();
                    services.AddTransient<ReceitaVendaComercializacaoEnergiaNuclearRepository>();
                    services.AddTransient<ReceitaVendaDistribuidoraCotistaEnergiaNuclearRepository>();
                    services.AddTransient<ProinfaInformacoesConformeResolucao1833UsinaRepository>();
                    services.AddTransient<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository>();
                    services.AddTransient<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository>();
                    services.AddTransient<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository>();
                    services.AddTransient<ConsumoParcelaCargaRepository>();
                    services.AddTransient<ConsumoPerfilAgenteRepository>();
                    services.AddTransient<ConsumoGeracaoPerfilAgenteRepository>();
                    services.AddTransient<RepasseRiscoHidrologicoRepository>();

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