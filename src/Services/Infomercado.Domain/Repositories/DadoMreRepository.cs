using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class DadoMreRepository : IRepository<DadoMre, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public DadoMreRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<DadoMre> ReadAll() => _infoMercadoDbContext
            .DadosMres
            .ToList();
        public IEnumerable<DadoMre> ReadByYear(int ano) => _infoMercadoDbContext
            .DadosMres
            .Where(x=>x.Data.Year == ano)
            .ToList();

        public DadoMre Read(int id) => _infoMercadoDbContext.DadosMres.Find(id);
        public void Create(params DadoMre[] entity)
        {
            _infoMercadoDbContext.DadosMres.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params DadoMre[] entity)
        {
            _infoMercadoDbContext.DadosMres.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params DadoMre[] entity)
        {
            _infoMercadoDbContext.DadosMres.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}