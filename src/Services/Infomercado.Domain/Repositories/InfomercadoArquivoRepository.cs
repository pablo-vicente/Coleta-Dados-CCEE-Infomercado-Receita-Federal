using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class InfomercadoArquivoRepository : IRepository<InfoMercadoArquivo, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public InfomercadoArquivoRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;
        
        public IEnumerable<InfoMercadoArquivo> ReadAll() => _infoMercadoDbContext.InfoMercadoArquivos.ToList();

        public InfoMercadoArquivo Read(int id) => _infoMercadoDbContext.InfoMercadoArquivos.Find(id);
        
        public void Create(params InfoMercadoArquivo[] entity)
        {
            _infoMercadoDbContext.InfoMercadoArquivos.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params InfoMercadoArquivo[] entity)
        {
            _infoMercadoDbContext.InfoMercadoArquivos.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params InfoMercadoArquivo[] entity)
        {
            _infoMercadoDbContext.InfoMercadoArquivos.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public InfoMercadoArquivo? ReadLatest() => _infoMercadoDbContext.InfoMercadoArquivos.LastOrDefault();

        public IEnumerable<InfoMercadoArquivo> ReadByName(string nome) =>
            _infoMercadoDbContext.InfoMercadoArquivos
                .Where(x => x.Nome.Equals(nome))
                .ToList();

        public IEnumerable<InfoMercadoArquivo> ListarInfoMercadoArquivoNaoImportados() =>
            _infoMercadoDbContext.InfoMercadoArquivos
                .Where(x => !x.Lido)
                .OrderBy(x => x.DataUltimaAtualizacao)
                .ToList();
    }
}