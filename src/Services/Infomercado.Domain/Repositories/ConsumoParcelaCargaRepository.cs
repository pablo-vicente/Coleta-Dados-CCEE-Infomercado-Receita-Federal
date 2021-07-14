using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ConsumoParcelaCargaRepository : IRepository<ConsumoParcelaCarga, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ConsumoParcelaCargaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ConsumoParcelaCarga> ReadAll() => _infoMercadoDbContext.ConsumoParcelaCargas.ToList();

        public IEnumerable<ConsumoParcelaCarga> ReadByYear(int ano) => _infoMercadoDbContext
            .ConsumoParcelaCargas
            .Where(x => x.Mes.Year == ano)
            .ToList();

        public ConsumoParcelaCarga Read(int id) => _infoMercadoDbContext.ConsumoParcelaCargas.Find(id);
        public void Create(params ConsumoParcelaCarga[] entity)
        {
            _infoMercadoDbContext.ConsumoParcelaCargas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ConsumoParcelaCarga[] entity)
        {
            _infoMercadoDbContext.ConsumoParcelaCargas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ConsumoParcelaCarga[] entity)
        {
            _infoMercadoDbContext.ConsumoParcelaCargas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}