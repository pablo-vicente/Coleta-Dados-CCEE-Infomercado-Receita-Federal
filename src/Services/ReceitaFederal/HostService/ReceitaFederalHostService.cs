using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReceitaFederal.Domain.Models;
using ReceitaFederal.Services;

namespace ReceitaFederal.HostService
{
    internal class ReceitaFederalHostService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly ReceitaFederalService _receitaFederalService;
        
        public ReceitaFederalHostService(ILogger<ReceitaFederalHostService> logger, IServiceProvider serviceProvider, ReceitaFederalService receitaFederalService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _receitaFederalService = receitaFederalService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var infoMercadoDbContext = scope.ServiceProvider.GetRequiredService<ReceitaFederalDbContext>();
                infoMercadoDbContext.Database.Migrate();
                infoMercadoDbContext.SaveChanges();
            }
            
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(15));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            _receitaFederalService.ImportarArquivosReceitaFederal();
            
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