using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository : IRepository<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade> ReadAll() => _infoMercadoDbContext.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades.ToList();

        public IEnumerable<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade> ReadByYear(int ano) => _infoMercadoDbContext
            .GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades
            .Where(x => x.MesAno.Year == ano)
            .ToList();

        public GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade Read(int id) => _infoMercadoDbContext.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades.Find(id);
        public void Create(params GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade[] entity)
        {
            _infoMercadoDbContext.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade[] entity)
        {
            _infoMercadoDbContext.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade[] entity)
        {
            _infoMercadoDbContext.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}