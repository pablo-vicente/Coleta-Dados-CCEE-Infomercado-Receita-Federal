using System;
using System.Threading;
using System.Threading.Tasks;
using Infomercado.Domain.Models;
using InfoMercado.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfoMercado.HostService
{
    internal class InfoMercadoHostService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private Timer _timer;
        private InfomercadoArquivoService _infomercadoArquivoService;

        public InfoMercadoHostService(ILogger<InfoMercadoHostService> logger, IServiceProvider serviceProvider, InfomercadoArquivoService infomercadoArquivoService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _infomercadoArquivoService = infomercadoArquivoService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var infoMercadoDbContext = scope.ServiceProvider.GetRequiredService<InfoMercadoDbContext>();
                infoMercadoDbContext.Database.Migrate();
                infoMercadoDbContext.SaveChanges();
            }
            
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(15));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            _infomercadoArquivoService.ImportarArquivosInfomercado();
            
            // try
            // {
            //     var tempo = new Stopwatch();
            //     tempo.Start();
            //     
            //     _logger.LogInformation($"Processamento iniciado em {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            //     
            //     // _downloadCceeInfoMercadoService.BaixarCCEEInfoMercado();
            //     _infoMercadoService.ImportarCCEEInfoMercado();
            //     
            //     tempo.Stop();
            //     _logger.LogInformation($"Processamento finalizado em {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - Tempo Total: {tempo.Elapsed.TotalMinutes}min");
            //     _logger.LogInformation($"Processamento será reiniciando em { DateTime.Now.AddDays(15).ToString("dd/MM/yyyy HH:mm:ss")} | 15 dias.");
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogInformation($"Ocorreu um erro ao tentar executar o serviço...");
            //     _logger.LogError(ex.Message);
            //     _logger.LogError(ex.StackTrace);
            //     _timer.Change(Timeout.Infinite, Timeout.Infinite);
            //     _logger.LogInformation($"O serviço será reiniciando em { TimeSpan.FromMinutes(10)} minutos.");
            //     _timer = new Timer(DoWork, null, TimeSpan.FromMinutes(10), TimeSpan.FromDays(15));
            // }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}