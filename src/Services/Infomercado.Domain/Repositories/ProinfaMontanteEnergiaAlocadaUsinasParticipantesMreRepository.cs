using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Interfaces;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Repositories
{
    public class ProinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository : IRepository<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre, int>
    {
        private readonly InfoMercadoDbContext _infoMercadoDbContext;
        
        public ProinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository(InfoMercadoDbContext infoMercadoDbContext) => _infoMercadoDbContext = infoMercadoDbContext;

        public IEnumerable<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre> ReadAll() => _infoMercadoDbContext.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres.ToList();

        public IEnumerable<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre> ReadByYear(int ano) => _infoMercadoDbContext
            .ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres
            .Where(x => x.Data.Year == ano)
            .ToList();

        public ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre Read(int id) => _infoMercadoDbContext.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres.Find(id);
        public void Create(params ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre[] entity)
        {
            _infoMercadoDbContext.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres.AddRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Update(params ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre[] entity)
        {
            _infoMercadoDbContext.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres.UpdateRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }

        public void Delete(params ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre[] entity)
        {
            _infoMercadoDbContext.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres.RemoveRange(entity);
            _infoMercadoDbContext.SaveChanges();
        }
    }
}