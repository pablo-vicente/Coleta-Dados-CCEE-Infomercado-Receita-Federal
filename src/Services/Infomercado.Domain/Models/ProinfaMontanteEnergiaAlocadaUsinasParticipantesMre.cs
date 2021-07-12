using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre
    {
        public ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre(bool participanteMreEUsina, FonteEnergiaPrimaria fonteEnergiaPrimaria, DateTime data, double fluxoEnergiaMreMWm, int idPerfilAgente, int idParcelaUsina)
        {
            ParticipanteMreEUsina = participanteMreEUsina;
            FonteEnergiaPrimaria = fonteEnergiaPrimaria;
            Data = data;
            FluxoEnergiaMreMWm = fluxoEnergiaMreMWm;
            IdPerfilAgente = idPerfilAgente;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }

        public bool ParticipanteMreEUsina { get; private set; }

        public FonteEnergiaPrimaria FonteEnergiaPrimaria { get; private set; }

        public DateTime Data { get; private set; }

        public double FluxoEnergiaMreMWm { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarParticipanteMreUsina(bool farticipanteMreEUsina) => ParticipanteMreEUsina = farticipanteMreEUsina;
        public void AtualizarFonteEnergiaPrimaria(FonteEnergiaPrimaria fonteEnergiaPrimaria) => FonteEnergiaPrimaria = fonteEnergiaPrimaria;
        public void AtualizarFluxoEnergiaMreMWm(double fluxoEnergiaMreMWm) => FluxoEnergiaMreMWm = fluxoEnergiaMreMWm;
    }
}