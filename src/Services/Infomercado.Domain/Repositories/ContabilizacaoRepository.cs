using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ContabilizacaoRepository : IRepository<Contabilizacao, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ContabilizacaoRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<Contabilizacao> ReadAll() => _infoMercadoDbContext.Contabilizacaes.ToList();
        public IEnumerable<Contabilizacao> ReadByYear(int ano) => _infoMercadoDbContext.Contabilizacaes
            .Where(x=>x.MesAno.Year == ano)
            .ToList();

        public Contabilizacao Read(int id) => _infoMercadoDbContext.Contabilizacaes.Find(id);
       
        public void Create(params Contabilizacao[] entity)
        {
            _infoMercadoDbContext.Contabilizacaes.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params Contabilizacao[] entity)
        {
            _infoMercadoDbContext.Contabilizacaes.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params Contabilizacao[] entity)
        {
            _infoMercadoDbContext.Contabilizacaes.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        
    }
}