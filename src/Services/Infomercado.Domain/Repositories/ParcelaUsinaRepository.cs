using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class ParcelaUsinaRepository : IRepository<ParcelaUsina, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ParcelaUsinaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ParcelaUsina> ReadAll() => _infoMercadoDbContext.ParcelaUsinas.ToList();

        public ParcelaUsina Read(int id) => _infoMercadoDbContext.ParcelaUsinas.Find(id);
        public void Create(params ParcelaUsina[] entity)
        {
            _infoMercadoDbContext.ParcelaUsinas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ParcelaUsina[] entity)
        {
            _infoMercadoDbContext.ParcelaUsinas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ParcelaUsina[] entity)
        {
            _infoMercadoDbContext.ParcelaUsinas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}