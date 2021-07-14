using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ConsumoGeracaoPerfilAgenteRepository : IRepository<ConsumoGeracaoPerfilAgente, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ConsumoGeracaoPerfilAgenteRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ConsumoGeracaoPerfilAgente> ReadAll() => _infoMercadoDbContext.ConsumoGeracaoPerfilAgentes.ToList();

        public IEnumerable<ConsumoGeracaoPerfilAgente> ReadByYear(int ano) => _infoMercadoDbContext
            .ConsumoGeracaoPerfilAgentes
            .Where(x => x.Mes.Year == ano)
            .ToList();

        public ConsumoGeracaoPerfilAgente Read(int id) => _infoMercadoDbContext.ConsumoGeracaoPerfilAgentes.Find(id);
        public void Create(params ConsumoGeracaoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoGeracaoPerfilAgentes.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ConsumoGeracaoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoGeracaoPerfilAgentes.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ConsumoGeracaoPerfilAgente[] entity)
        {
            _infoMercadoDbContext.ConsumoGeracaoPerfilAgentes.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}