using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Enums;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class RepasseRiscoHidrologicoRepository : IRepository<RepasseRiscoHidrologico, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public RepasseRiscoHidrologicoRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<RepasseRiscoHidrologico> ReadAll() => _infoMercadoDbContext.RepasseRiscoHidrologicos.ToList();

        public IEnumerable<RepasseRiscoHidrologico> ReadByYear(int ano) => _infoMercadoDbContext
            .RepasseRiscoHidrologicos
            .Where(x => x.Mes.Year == ano)
            .ToList();

        public RepasseRiscoHidrologico Read(int id) => _infoMercadoDbContext.RepasseRiscoHidrologicos.Find(id);
        public void Create(params RepasseRiscoHidrologico[] entity)
        {
            _infoMercadoDbContext.RepasseRiscoHidrologicos.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params RepasseRiscoHidrologico[] entity)
        {
            _infoMercadoDbContext.RepasseRiscoHidrologicos.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params RepasseRiscoHidrologico[] entity)
        {
            _infoMercadoDbContext.RepasseRiscoHidrologicos.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}