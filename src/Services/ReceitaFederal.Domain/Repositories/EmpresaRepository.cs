using System;
using System.Collections.Generic;
using System.Linq;
using ReceitaFederal.Domain.Models;

namespace ReceitaFederal.Domain.Repositories
{
    public class EmpresaRepository
    {
        private readonly ReceitaFederalDbContext _receitaFederalDbContext;
        
        public EmpresaRepository(ReceitaFederalDbContext receitaFederalDbContext) => _receitaFederalDbContext = receitaFederalDbContext;

        public IEnumerable<Empresa> ReadAll() => _receitaFederalDbContext.Empresas.ToList();

        public Empresa Read(int id) => _receitaFederalDbContext.Empresas.Find(id);
        public void Create(params Empresa[] entity)
        {
            _receitaFederalDbContext.Empresas.AddRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Update(params Empresa[] entity)
        {
            _receitaFederalDbContext.Empresas.UpdateRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public void Delete(params Empresa[] entity)
        {
            _receitaFederalDbContext.Empresas.RemoveRange(entity);
            _receitaFederalDbContext.SaveChanges();
        }

        public IEnumerable<string> ListarCnpjs()
        {
            return _receitaFederalDbContext
                .Empresas
                .Select(x => x.Cnpj)
                .Distinct()
                .ToList();
        }
        
        public IEnumerable<string> ListarCnpjsAtualizados(DateTime atualizacao) => _receitaFederalDbContext.Empresas
            .Where(empresa => empresa.Atualizacao >= atualizacao)
            .Select(empresa => empresa.Cnpj)
            .ToList();

        public IEnumerable<Empresa> ReadAllByCnpj(List<string> cnpjsComBarra)
        {
            return _receitaFederalDbContext
                .Empresas
                .Where(x => cnpjsComBarra.Contains(x.Cnpj))
                .ToList();
        }
    }
}