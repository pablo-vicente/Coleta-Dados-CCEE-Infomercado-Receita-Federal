using System.Collections.Generic;
using System.Linq;
using ReceitaFederal.Domain.Models;

namespace ReceitaFederal.Domain.Repositories
{
    public class ReceitaFederalArquivoRepository
    {
        private readonly ReceitaFederalDbContext _receitaFederalDbContext;
        
        public ReceitaFederalArquivoRepository(ReceitaFederalDbContext receitaFederalDbContext) => _receitaFederalDbContext = receitaFederalDbContext;

        public IEnumerable<ReceitaFederalArquivo> ReadAll() => _receitaFederalDbContext.ReceitaFederalArquivos.ToList();

        public ReceitaFederalArquivo Read(int id) => _receitaFederalDbContext.ReceitaFederalArquivos.Find(id);
        public void Create(params ReceitaFederalArquivo[] entity)
        {
            _receitaFederalDbContext.ReceitaFederalArquivos.AddRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Update(params ReceitaFederalArquivo[] entity)
        {
            _receitaFederalDbContext.ReceitaFederalArquivos.UpdateRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Delete(params ReceitaFederalArquivo[] entity)
        {
            _receitaFederalDbContext.ReceitaFederalArquivos.RemoveRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public IEnumerable<ReceitaFederalArquivo> ListarArquivosNaoImportados() =>
            _receitaFederalDbContext
                .ReceitaFederalArquivos
                .Where(x => !x.Lido)
                .OrderBy(x => x.Atualizacao)
                .ToList();
    }
}