using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class LiquidacaoDistribuidoraCotistaGarantiaFisicaRepository : IRepository<LiquidacaoDistribuidoraCotistaGarantiaFisica, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public LiquidacaoDistribuidoraCotistaGarantiaFisicaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<LiquidacaoDistribuidoraCotistaGarantiaFisica> ReadAll() => _infoMercadoDbContext.CotistasLiquidacaoDistribuidoraGarantiaFisicas.ToList();

        public IEnumerable<LiquidacaoDistribuidoraCotistaGarantiaFisica> ReadByYear(int ano) => _infoMercadoDbContext
            .CotistasLiquidacaoDistribuidoraGarantiaFisicas
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public LiquidacaoDistribuidoraCotistaGarantiaFisica Read(int id) => _infoMercadoDbContext.CotistasLiquidacaoDistribuidoraGarantiaFisicas.Find(id);
        public void Create(params LiquidacaoDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.CotistasLiquidacaoDistribuidoraGarantiaFisicas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params LiquidacaoDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.CotistasLiquidacaoDistribuidoraGarantiaFisicas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params LiquidacaoDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.CotistasLiquidacaoDistribuidoraGarantiaFisicas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}