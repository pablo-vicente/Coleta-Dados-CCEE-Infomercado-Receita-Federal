using System.Collections.Generic;
using System.Linq;
using ReceitaFederal.Domain.Models;

namespace ReceitaFederal.Domain.Repositories
{
    public class SocioRepository
    {
        private readonly ReceitaFederalDbContext _receitaFederalDbContext;
        
        public SocioRepository(ReceitaFederalDbContext receitaFederalDbContext) => _receitaFederalDbContext = receitaFederalDbContext;

        public IEnumerable<Socio> ReadAll() => _receitaFederalDbContext.Socios.ToList();

        public Socio Read(int id) => _receitaFederalDbContext.Socios.Find(id);
        public void Create(params Socio[] entity)
        {
            _receitaFederalDbContext.Socios.AddRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Update(params Socio[] entity)
        {
            _receitaFederalDbContext.Socios.UpdateRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Delete(params Socio[] entity)
        {
            _receitaFederalDbContext.Socios.RemoveRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void ExcluirSocios(int empresaId)
        {
            var socios = _receitaFederalDbContext.Socios.Where(x => x.IdEmpresa == empresaId).ToList();
            _receitaFederalDbContext.Socios.RemoveRange(socios);
            _receitaFederalDbContext.SaveChanges();
        }
    }
}