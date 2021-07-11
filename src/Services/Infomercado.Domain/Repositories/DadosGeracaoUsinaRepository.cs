using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class DadosGeracaoUsinaRepository : IRepository<DadosGeracaoUsina, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public DadosGeracaoUsinaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<DadosGeracaoUsina> ReadAll() => _infoMercadoDbContext.DadosGeracaoUsinas.ToList();
        public IEnumerable<DadosGeracaoUsina> ReadByYear(int ano) => _infoMercadoDbContext.DadosGeracaoUsinas
            .Where(x=>x.Mes.Year == ano)
            .ToList();

        public DadosGeracaoUsina Read(int id) => _infoMercadoDbContext.DadosGeracaoUsinas.Find(id);
        public void Create(params DadosGeracaoUsina[] entity)
        {
            _infoMercadoDbContext.DadosGeracaoUsinas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params DadosGeracaoUsina[] entity)
        {
            _infoMercadoDbContext.DadosGeracaoUsinas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params DadosGeracaoUsina[] entity)
        {
            _infoMercadoDbContext.DadosGeracaoUsinas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}