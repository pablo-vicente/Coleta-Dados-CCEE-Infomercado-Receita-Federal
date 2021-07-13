using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade
    {
        public MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade(DateTime mesAno, string cegeEmpreendimento, string leilao, string produto, string siglaLeilao, FonteEnergiaPrimaria fonteEnergiaPrimaria, DateTime dataLeilao, double garantiaFisicaComprometidaMWm, double contratosMWh, double geracaoDestinadaLeilaoMWm, int idParcelaUsina)
        {
            MesAno = mesAno;
            CegeEmpreendimento = cegeEmpreendimento;
            Leilao = leilao;
            Produto = produto;
            SiglaLeilao = siglaLeilao;
            FonteEnergiaPrimaria = fonteEnergiaPrimaria;
            DataLeilao = dataLeilao;
            GarantiaFisicaComprometidaMWm = garantiaFisicaComprometidaMWm;
            ContratosMWh = contratosMWh;
            GeracaoDestinadaLeilaoMWm = geracaoDestinadaLeilaoMWm;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }

        public DateTime MesAno { get; private set; }

        public string CegeEmpreendimento { get; private set; }

        public string Leilao { get; private set; }

        public string Produto { get; private set; }

        public string SiglaLeilao { get; private set; }

        public FonteEnergiaPrimaria FonteEnergiaPrimaria { get; private set; }

        public DateTime DataLeilao { get; private set; }

        public double GarantiaFisicaComprometidaMWm { get; private set; }

        public double ContratosMWh { get; private set; }

        public double GeracaoDestinadaLeilaoMWm { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarCegeEmpreendimento(string cegeEmpreendimento) => CegeEmpreendimento = cegeEmpreendimento;
        public void AtualizarLeilao(string leilao) => Leilao = leilao;
        public void AtualizarProduto(string produto) => Produto = produto;
        public void AtualizarSiglaLeilao(string siglaLeilao) => SiglaLeilao = siglaLeilao;
        public void AtualizarFonteEnergiaPrimaria(FonteEnergiaPrimaria fonteEnergiaPrimaria) => FonteEnergiaPrimaria = fonteEnergiaPrimaria;
        public void AtualizarDataLeilao(DateTime dataLeilao) => DataLeilao = dataLeilao;
        public void AtualizarGarantiaFisicaComprometidaMWm(double garantiaFisicaComprometidaMWm) => GarantiaFisicaComprometidaMWm = garantiaFisicaComprometidaMWm;
        public void AtualizarContratosMWh(double contratosMWh) => ContratosMWh = contratosMWh;
        public void AtualizarGeracaoDestinadaLeilaoMWm(double geracaoDestinadaLeilaoMWm) => GeracaoDestinadaLeilaoMWm = geracaoDestinadaLeilaoMWm;
    }
}