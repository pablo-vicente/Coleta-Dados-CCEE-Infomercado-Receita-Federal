using System;

namespace Infomercado.Domain.Models
{
    public class GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade
    {
        public GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade(DateTime mesAno, string cegeEmpreendimento, double garantiaFisicaMWm, double garantiaFisicaComprometidaMWm, double geracaoMWm, double geracaoDestinadaLeilaoMWm, double geracaoLivreMWm, int idParcelaUsina)
        {
            MesAno = mesAno;
            CegeEmpreendimento = cegeEmpreendimento;
            GarantiaFisicaMWm = garantiaFisicaMWm;
            GarantiaFisicaComprometidaMWm = garantiaFisicaComprometidaMWm;
            GeracaoMWm = geracaoMWm;
            GeracaoDestinadaLeilaoMWm = geracaoDestinadaLeilaoMWm;
            GeracaoLivreMWm = geracaoLivreMWm;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }

        public DateTime MesAno { get; private set; }

        public string CegeEmpreendimento { get; private set; }

        public double GarantiaFisicaMWm { get; private set; }

        public double GarantiaFisicaComprometidaMWm { get; private set; }
        
        public double GeracaoMWm { get; private set; }

        public double GeracaoDestinadaLeilaoMWm { get; private set; }
        
        public double GeracaoLivreMWm { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarCegeEmpreendimento(string cegeEmpreendimento) => CegeEmpreendimento = cegeEmpreendimento;
        public void AtualizarGarantiaFisicaMWm(double garantiaFisicaMWm) => GarantiaFisicaMWm = garantiaFisicaMWm;
        public void AtualizarGarantiaFisicaComprometidaMWm(double garantiaFisicaComprometidaMWm) => GarantiaFisicaComprometidaMWm = garantiaFisicaComprometidaMWm;
        public void AtualizarGeracaoMWm(double geracaoMWm) => GeracaoMWm = geracaoMWm;
        public void AtualizarGeracaoDestinadaLeilaoMWm(double geracaoDestinadaLeilaoMWm) => GeracaoDestinadaLeilaoMWm = geracaoDestinadaLeilaoMWm;
        public void AtualizarGeracaoLivreMWm(double geracaoLivreMWm) => GeracaoLivreMWm = geracaoLivreMWm;
    }
}