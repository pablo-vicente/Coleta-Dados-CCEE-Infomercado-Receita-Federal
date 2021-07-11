using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class EncargoRepository : IRepository<Encargo, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public EncargoRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<Encargo> ReadAll() => _infoMercadoDbContext.Encargos.ToList();
        public IEnumerable<Encargo> ReadByYear(int ano) => _infoMercadoDbContext.Encargos
            .Where(x=>x.Mes.Year == ano)
            .ToList();

        public Encargo Read(int id) => _infoMercadoDbContext.Encargos.Find(id);
        public void Create(params Encargo[] entity)
        {
            _infoMercadoDbContext.Encargos.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params Encargo[] entity)
        {
            _infoMercadoDbContext.Encargos.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params Encargo[] entity)
        {
            _infoMercadoDbContext.Encargos.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}