using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class DadosGeracaoUsina
    {
        public DadosGeracaoUsina() {}
        
        public DadosGeracaoUsina(string? cegempreendimento, TipoDespacho tipoDespacho, bool participanteRateioPerdas, FonteEnergiaPrimaria fonteEnergiaPrimaria, Combustivel? combustivel, bool participanteMre, bool participanteRegimeCotas, double taxaDescontoUsina, double capacidadeUsinaMw, double garantiaFisicaMWm, double fatorOperacaoComercial, double fatorPerdasInternas, DateTime mes, double geracaoCentroGravidadeMWm, double geracaoTesteCentroGravidadeMWm, double garantiaFisicaCentroGravidadeMWm, double geracaoSegurancaEnergeticaMWm, double geracaoRestricaoOperacaoConstrainedOnMwm, double geracaoManutencaoReserveOperativaMWm, int idParcelaUsina, int idPerfilAgente)
        {
            Cegempreendimento = cegempreendimento;
            TipoDespacho = tipoDespacho;
            ParticipanteRateioPerdas = participanteRateioPerdas;
            FonteEnergiaPrimaria = fonteEnergiaPrimaria;
            Combustivel = combustivel;
            ParticipanteMre = participanteMre;
            ParticipanteRegimeCotas = participanteRegimeCotas;
            TaxaDescontoUsina = taxaDescontoUsina;
            CapacidadeUsinaMW = capacidadeUsinaMw;
            GarantiaFisicaMWm = garantiaFisicaMWm;
            FatorOperacaoComercial = fatorOperacaoComercial;
            FatorPerdasInternas = fatorPerdasInternas;
            Mes = mes;
            GeracaoCentroGravidadeMWm = geracaoCentroGravidadeMWm;
            GeracaoTesteCentroGravidadeMWm = geracaoTesteCentroGravidadeMWm;
            GarantiaFisicaCentroGravidadeMWm = garantiaFisicaCentroGravidadeMWm;
            GeracaoSegurancaEnergeticaMWm = geracaoSegurancaEnergeticaMWm;
            GeracaoRestricaoOperacaoConstrainedOnMwm = geracaoRestricaoOperacaoConstrainedOnMwm;
            GeracaoManutencaoReserveOperativaMWm = geracaoManutencaoReserveOperativaMWm;
            IdParcelaUsina = idParcelaUsina;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }
        
        public string? Cegempreendimento { get; private set; }

        public TipoDespacho TipoDespacho { get; private set; }

        public bool ParticipanteRateioPerdas { get; private set; }

        public FonteEnergiaPrimaria FonteEnergiaPrimaria { get; private set; }

        public Combustivel? Combustivel { get; private set; }

        public bool ParticipanteMre { get; private set; }

        public bool ParticipanteRegimeCotas { get; private set; }

        public double TaxaDescontoUsina { get; private set; }
        
        public double CapacidadeUsinaMW { get; private set; }
        
        public double GarantiaFisicaMWm { get; private set; }
        
        public double FatorOperacaoComercial { get; private set; }

        public double FatorPerdasInternas { get; private set; }

        public DateTime Mes { get; private set; }

        public double GeracaoCentroGravidadeMWm { get; private set; }
        
        public double GeracaoTesteCentroGravidadeMWm { get; private set; }

        public double GarantiaFisicaCentroGravidadeMWm { get; private set; }

        public double GeracaoSegurancaEnergeticaMWm { get; private set; }
        
        public double GeracaoRestricaoOperacaoConstrainedOnMwm { get; private set; }
        
        public double GeracaoManutencaoReserveOperativaMWm { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarCegempreendimento(string cegempreendimento)
        {
            if (!string.IsNullOrEmpty(cegempreendimento))
                Cegempreendimento = cegempreendimento;
        }

        public void AtualizarTipoDespacho(TipoDespacho tipoDespacho) => TipoDespacho = tipoDespacho;
        public void AtualizarParticipanteRateioPerdas(bool participanteRateioPerdas) => ParticipanteRateioPerdas = participanteRateioPerdas;
        public void AtualizaFonteEnergiaPrimaria(FonteEnergiaPrimaria fonteEnergiaPrimaria) => FonteEnergiaPrimaria = fonteEnergiaPrimaria;

        public void AtualizarCombustivel(Combustivel? combustivel)
        {
            if (combustivel is not null)
                Combustivel = combustivel;
        }
        
        public void AtualizarParticipanteMre(bool participanteMre) => ParticipanteMre = participanteMre;
        public void AtualizarParticipanteRegimeCotas(bool participanteRegimeCotas) => ParticipanteRegimeCotas = participanteRegimeCotas;
        public void AtualizarTaxaDescontoUsina(double taxaDescontoUsina) => TaxaDescontoUsina = taxaDescontoUsina;
        public void AtualizarCapacidadeUsinaMW(double capacidadeUsinaMW) => CapacidadeUsinaMW = capacidadeUsinaMW;
        public void AtualizarGarantiaFisicaMWm(double garantiaFisicaMWm) => GarantiaFisicaMWm = garantiaFisicaMWm;
        public void AtualizarFatorOperacaoComercial(double fatorOperacaoComercial) => FatorOperacaoComercial = fatorOperacaoComercial;
        public void AtualizarFatorPerdasInternas(double fatorPerdasInternas) => FatorPerdasInternas = fatorPerdasInternas;

        public void AtualizarGeracaoCentroGravidadeMWm(double geracaoCentroGravidadeMWm) => GeracaoCentroGravidadeMWm = geracaoCentroGravidadeMWm;
        public void AtualizarGeracaoTesteCentroGravidadeMWm (double geracaoTesteCentroGravidadeMWm) => GeracaoTesteCentroGravidadeMWm = geracaoTesteCentroGravidadeMWm;
        public void AtualizarGarantiaFisicaCentroGravidadeMWm (double garantiaFisicaCentroGravidadeMWm) => GarantiaFisicaCentroGravidadeMWm = garantiaFisicaCentroGravidadeMWm;
        public void AtualizarGeracaoSegurancaEnergeticaMWm (double geracaoSegurancaEnergeticaMWm) =>  GeracaoSegurancaEnergeticaMWm = geracaoSegurancaEnergeticaMWm;
        public void AtualizarGeracaoRestricaoOperacaoConstrainedOnMwm (double geracaoRestricaoOperacaoConstrainedOnMwm) => GeracaoRestricaoOperacaoConstrainedOnMwm = geracaoRestricaoOperacaoConstrainedOnMwm;
        public void AtualizarGeracaoManutencaoReserveOperativaMWm (double geracaoManutencaoReserveOperativaMWm) => GeracaoManutencaoReserveOperativaMWm = geracaoManutencaoReserveOperativaMWm;
    }
}