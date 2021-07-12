using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ReceitaVendaComercializacaoEnergiaNuclearRepository : IRepository<ReceitaVendaComercializacaoEnergiaNuclear, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ReceitaVendaComercializacaoEnergiaNuclearRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ReceitaVendaComercializacaoEnergiaNuclear> ReadAll() => _infoMercadoDbContext.ReceitaVendaComercializacaoEnergiaNucleares.ToList();

        public IEnumerable<ReceitaVendaComercializacaoEnergiaNuclear> ReadByYear(int ano) => _infoMercadoDbContext
            .ReceitaVendaComercializacaoEnergiaNucleares
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public ReceitaVendaComercializacaoEnergiaNuclear Read(int id) => _infoMercadoDbContext.ReceitaVendaComercializacaoEnergiaNucleares.Find(id);
        public void Create(params ReceitaVendaComercializacaoEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaComercializacaoEnergiaNucleares.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ReceitaVendaComercializacaoEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaComercializacaoEnergiaNucleares.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ReceitaVendaComercializacaoEnergiaNuclear[] entity)
        {
            _infoMercadoDbContext.ReceitaVendaComercializacaoEnergiaNucleares.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}