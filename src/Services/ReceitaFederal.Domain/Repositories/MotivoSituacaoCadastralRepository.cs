using System.Collections.Generic;
using System.Linq;
using ReceitaFederal.Domain.Models;

namespace ReceitaFederal.Domain.Repositories
{
    public class MotivoSituacaoCadastralRepository
    {
        private readonly ReceitaFederalDbContext _receitaFederalDbContext;
        
        public MotivoSituacaoCadastralRepository(ReceitaFederalDbContext receitaFederalDbContext) => _receitaFederalDbContext = receitaFederalDbContext;

        public IEnumerable<MotivoSituacaoCadastral> ReadAll() => _receitaFederalDbContext.MotivosSituacaoCadastral.ToList();

        public MotivoSituacaoCadastral Read(int id) => _receitaFederalDbContext.MotivosSituacaoCadastral.Find(id);
        public void Create(params MotivoSituacaoCadastral[] entity)
        {
            _receitaFederalDbContext.MotivosSituacaoCadastral.AddRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Update(params MotivoSituacaoCadastral[] entity)
        {
            _receitaFederalDbContext.MotivosSituacaoCadastral.UpdateRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Delete(params MotivoSituacaoCadastral[] entity)
        {
            _receitaFederalDbContext.MotivosSituacaoCadastral.RemoveRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }
    }
}