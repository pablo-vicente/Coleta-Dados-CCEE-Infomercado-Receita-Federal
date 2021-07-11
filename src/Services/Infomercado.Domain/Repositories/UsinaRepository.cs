using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class UsinaRepository : IRepository<Usina, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public UsinaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<Usina> ReadAll() => _infoMercadoDbContext
            .Usinas
            .Include(x=>x.ParcelasUsina)
            .ThenInclude(x=>x.DadosGeracaoUsina)
            .ToList();

        public Usina Read(int id) => _infoMercadoDbContext.Usinas.Find(id);
        public void Create(params Usina[] entity)
        {
            _infoMercadoDbContext.Usinas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params Usina[] entity)
        {
            _infoMercadoDbContext.Usinas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params Usina[] entity)
        {
            _infoMercadoDbContext.Usinas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}