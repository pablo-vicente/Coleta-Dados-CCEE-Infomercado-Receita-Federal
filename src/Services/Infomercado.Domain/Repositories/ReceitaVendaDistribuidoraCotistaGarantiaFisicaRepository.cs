using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ReceitaVendaDistribuidoraCotistaGarantiaFisicaRepository : IRepository<ReceitaVendaDistribuidoraCotistaGarantiaFisica, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ReceitaVendaDistribuidoraCotistaGarantiaFisicaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ReceitaVendaDistribuidoraCotistaGarantiaFisica> ReadAll() => _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaGarantiaFisicas.ToList();

        public IEnumerable<ReceitaVendaDistribuidoraCotistaGarantiaFisica> ReadByYear(int ano) => _infoMercadoDbContext
            .ReceitaVendaDistribuidoraCotistaGarantiaFisicas
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public ReceitaVendaDistribuidoraCotistaGarantiaFisica Read(int id) => _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaGarantiaFisicas.Find(id);
        public void Create(params ReceitaVendaDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaGarantiaFisicas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ReceitaVendaDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaGarantiaFisicas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ReceitaVendaDistribuidoraCotistaGarantiaFisica[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaGarantiaFisicas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}