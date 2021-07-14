using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ConsumoPerfilAgenteRepository : IRepository<ConsumoPerfilAgente, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ConsumoPerfilAgenteRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ConsumoPerfilAgente> ReadAll() => _infoMercadoDbContext.ConsumoPerfilAgentes.ToList();

        public IEnumerable<ConsumoPerfilAgente> ReadByYear(int ano) => _infoMercadoDbContext
            .ConsumoPerfilAgentes
            .Where(x => x.Mes.Year == ano)
            .ToList();

        public ConsumoPerfilAgente Read(int id) => _infoMercadoDbContext.ConsumoPerfilAgentes.Find(id);
        public void Create(params ConsumoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoPerfilAgentes.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ConsumoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoPerfilAgentes.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ConsumoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoPerfilAgentes.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}