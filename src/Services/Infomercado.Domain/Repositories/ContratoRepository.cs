using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Repositories
{
    public class ContratoRepository : IRepository<Contrato, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ContratoRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<Contrato> ReadAll() => _infoMercadoDbContext.Contratos.ToList();
        public IEnumerable<Contrato> ReadByYear(int ano) => _infoMercadoDbContext.Contratos
            .Where(x=>x.Data.Year == ano)
            .ToList();

        public Contrato Read(int id) => _infoMercadoDbContext.Contratos.Find(id);
        
        public void Create(params Contrato[] entity)
        {
            _infoMercadoDbContext.Contratos.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params Contrato[] entity)
        {
            _infoMercadoDbContext.Contratos.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params Contrato[] entity)
        {
            _infoMercadoDbContext.Contratos.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}