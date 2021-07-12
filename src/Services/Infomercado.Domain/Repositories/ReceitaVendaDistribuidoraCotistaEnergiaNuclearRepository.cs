using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ReceitaVendaDistribuidoraCotistaEnergiaNuclearRepository : IRepository<ReceitaVendaDistribuidoraCotistaEnergiaNuclear, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ReceitaVendaDistribuidoraCotistaEnergiaNuclearRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ReceitaVendaDistribuidoraCotistaEnergiaNuclear> ReadAll() => _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaEnergiaNucleares.ToList();

        public IEnumerable<ReceitaVendaDistribuidoraCotistaEnergiaNuclear> ReadByYear(int ano) => _infoMercadoDbContext
            .ReceitaVendaDistribuidoraCotistaEnergiaNucleares
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public ReceitaVendaDistribuidoraCotistaEnergiaNuclear Read(int id) => _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaEnergiaNucleares.Find(id);
        public void Create(params ReceitaVendaDistribuidoraCotistaEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaEnergiaNucleares.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ReceitaVendaDistribuidoraCotistaEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaEnergiaNucleares.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ReceitaVendaDistribuidoraCotistaEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaDistribuidoraCotistaEnergiaNucleares.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}