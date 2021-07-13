using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository : IRepository<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade> ReadAll() => _infoMercadoDbContext.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades.ToList();

        public IEnumerable<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade> ReadByYear(int ano) => _infoMercadoDbContext
            .MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade Read(int id) => _infoMercadoDbContext.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades.Find(id);
        public void Create(params MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade[] entity)
        {
            _infoMercadoDbContext.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade[] entity)
        {
            _infoMercadoDbContext.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade[] entity)
        {
            _infoMercadoDbContext.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}