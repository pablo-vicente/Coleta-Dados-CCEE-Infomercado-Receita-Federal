using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class AgenteRepository : IRepository<Agente, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public AgenteRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<Agente> ReadAll() => _infoMercadoDbContext.Agentes
            .Include(x => x.PerfisAgente)
            .ToList();

        public Agente Read(int id) => _infoMercadoDbContext.Agentes.Find(id);
        public void Create(params Agente[] entity)
        {
            _infoMercadoDbContext.Agentes.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params Agente[] entity)
        {
            _infoMercadoDbContext.Agentes.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params Agente[] entity)
        {
            _infoMercadoDbContext.Agentes.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}