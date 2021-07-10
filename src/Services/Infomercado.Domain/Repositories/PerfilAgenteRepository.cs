using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class PerfilAgenteRepository : IRepository<PerfilAgente, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public PerfilAgenteRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<PerfilAgente> ReadAll() => _infoMercadoDbContext.PerfilAgentes
            .Include(x => x.Agente)
            .ToList();

        public PerfilAgente Read(int id) => _infoMercadoDbContext.PerfilAgentes.Find(id);
        public void Create(params PerfilAgente[] entity)
        {
            _infoMercadoDbContext.PerfilAgentes.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params PerfilAgente[] entity)
        {
            _infoMercadoDbContext.PerfilAgentes.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params PerfilAgente[] entity)
        {
            _infoMercadoDbContext.PerfilAgentes.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}