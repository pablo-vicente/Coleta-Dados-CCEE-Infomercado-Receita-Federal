using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ProinfaInformacoesConformeResolucao1833UsinaRepository : IRepository<ProinfaInformacoesConformeResolucao1833Usina, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ProinfaInformacoesConformeResolucao1833UsinaRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ProinfaInformacoesConformeResolucao1833Usina> ReadAll() => _infoMercadoDbContext.ProinfaInformacoesConformeResolucao1833Usinas.ToList();

        public IEnumerable<ProinfaInformacoesConformeResolucao1833Usina> ReadByYear(int ano) => _infoMercadoDbContext
            .ProinfaInformacoesConformeResolucao1833Usinas
            .Where(x => x.Data.Year == ano)
            .ToList();

        public ProinfaInformacoesConformeResolucao1833Usina Read(int id) => _infoMercadoDbContext.ProinfaInformacoesConformeResolucao1833Usinas.Find(id);
        public void Create(params ProinfaInformacoesConformeResolucao1833Usina[] entity)
        {
            _infoMercadoDbContext.ProinfaInformacoesConformeResolucao1833Usinas.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ProinfaInformacoesConformeResolucao1833Usina[] entity)
        {
            _infoMercadoDbContext.ProinfaInformacoesConformeResolucao1833Usinas.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ProinfaInformacoesConformeResolucao1833Usina[] entity)
        {
            _infoMercadoDbContext.ProinfaInformacoesConformeResolucao1833Usinas.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}