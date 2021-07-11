using System;

namespace Infomercado.Domain.Models
{
    public class Contabilizacao
    {
        public Contabilizacao(DateTime mesAno, double resultadoMercadoCurtoPrazo, double compensacaoMre,
            double encargosConsolidados, double ajusteExposicaoFinanceira, double ajusteAlivioRetroativo,
            double efeitoContracaoPorDisponibilidade, double efeitoContratacaoCotaGarantiaFisica,
            double efeitoContratacaoComercializacaoEnergiaNuclear, double ajusteRecontabilizacoes, double ajusteMcsd,
            double excedenteFinanceiroEnergiaReserva, double efeitoCcearUsinasAptas, double efeitoContratacaoItaipu,
            double efeitoRepasseRiscoHidrologico, double efeitoCustosDeslocamentoPldeCmo, double resultadoFinal,
            int idPerfilAgente)
        {
            MesAno = mesAno;
            ResultadoMercadoCurtoPrazo = resultadoMercadoCurtoPrazo;
            CompensacaoMre = compensacaoMre;
            EncargosConsolidados = encargosConsolidados;
            AjusteExposicaoFinanceira = ajusteExposicaoFinanceira;
            AjusteAlivioRetroativo = ajusteAlivioRetroativo;
            EfeitoContracaoPorDisponibilidade = efeitoContracaoPorDisponibilidade;
            EfeitoContratacaoCotaGarantiaFisica = efeitoContratacaoCotaGarantiaFisica;
            EfeitoContratacaoComercializacaoEnergiaNuclear = efeitoContratacaoComercializacaoEnergiaNuclear;
            AjusteRecontabilizacoes = ajusteRecontabilizacoes;
            AjusteMcsd = ajusteMcsd;
            ExcedenteFinanceiroEnergiaReserva = excedenteFinanceiroEnergiaReserva;
            EfeitoCcearUsinasAptas = efeitoCcearUsinasAptas;
            EfeitoContratacaoItaipu = efeitoContratacaoItaipu;
            EfeitoRepasseRiscoHidrologico = efeitoRepasseRiscoHidrologico;
            EfeitoCustosDeslocamentoPldeCmo = efeitoCustosDeslocamentoPldeCmo;
            ResultadoFinal = resultadoFinal;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }

        public DateTime MesAno { get; private set; }

        public double ResultadoMercadoCurtoPrazo { get; private set; }
        
        public double CompensacaoMre { get; private set; }
        
        public double EncargosConsolidados { get; private set; }
        
        public double AjusteExposicaoFinanceira { get; private set; }
        
        public double AjusteAlivioRetroativo { get; private set; }

        public double EfeitoContracaoPorDisponibilidade { get; private set; }

        public double EfeitoContratacaoCotaGarantiaFisica { get; private set; }

        public double EfeitoContratacaoComercializacaoEnergiaNuclear { get; private set; }

        public double AjusteRecontabilizacoes { get; private set; }
        
        public double AjusteMcsd { get; private set; }
        
        public double ExcedenteFinanceiroEnergiaReserva { get; private set; }

        public double EfeitoCcearUsinasAptas { get; private set; }

        public double EfeitoContratacaoItaipu { get; private set; }

        public double EfeitoRepasseRiscoHidrologico { get; private set; }
        
        public double EfeitoCustosDeslocamentoPldeCmo { get; private set; }
        
        public double ResultadoFinal { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }
        
        
        //
        
        public void AtualizarResultadoMercadoCurtoPrazo(double resultadoMercadoCurtoPrazo) => ResultadoMercadoCurtoPrazo = resultadoMercadoCurtoPrazo;
        public void AtualizarCompensacaoMre(double encargosConsolidados) => EncargosConsolidados = encargosConsolidados;
        public void AtualizarEncargosConsolidados(double encargosConsolidados) => EncargosConsolidados = encargosConsolidados;
        public void AtualizarAjusteExposicaoFinanceira(double ajusteExposicaoFinanceira) => AjusteExposicaoFinanceira = ajusteExposicaoFinanceira;
        public void AtualizarAjusteAlivioRetroativo(double ajusteAlivioRetroativo) => AjusteAlivioRetroativo = ajusteAlivioRetroativo;
        public void AtualizarEfeitoContracaoPorDisponibilidade(double efeitoContracaoPorDisponibilidade) => EfeitoContracaoPorDisponibilidade = efeitoContracaoPorDisponibilidade;
        public void AtualizarEfeitoContratacaoCotaGarantiaFisica(double efeitoContratacaoCotaGarantiaFisica) => EfeitoContratacaoCotaGarantiaFisica = efeitoContratacaoCotaGarantiaFisica;
        public void AtualizarEfeitoContratacaoComercializacaoEnergiaNuclear(double efeitoContratacaoComercializacaoEnergiaNuclear) => EfeitoContratacaoComercializacaoEnergiaNuclear = efeitoContratacaoComercializacaoEnergiaNuclear;
        public void AtualizarAjusteRecontabilizacoes(double ajusteRecontabilizacoes) => AjusteRecontabilizacoes = ajusteRecontabilizacoes;
        public void AtualizarAjusteMcsd (double ajusteMcsd) => AjusteMcsd = ajusteMcsd;
        public void AtualizarExcedenteFinanceiroEnergiaReserva(double excedenteFinanceiroEnergiaReserva) => ExcedenteFinanceiroEnergiaReserva = excedenteFinanceiroEnergiaReserva;
        public void AtualizarEfeitoCcearUsinasAptas(double efeitoCcearUsinasAptas) => EfeitoCcearUsinasAptas = efeitoCcearUsinasAptas;
        public void AtualizarEfeitoContratacaoItaipu(double efeitoContratacaoItaipu) => EfeitoContratacaoItaipu = efeitoContratacaoItaipu;
        public void AtualizarEfeitoRepasseRiscoHidrologico(double efeitoRepasseRiscoHidrologico) => EfeitoRepasseRiscoHidrologico = efeitoRepasseRiscoHidrologico;
        public void AtualizarEfeitoCustosDeslocamentoPldeCmo(double efeitoCustosDeslocamentoPldeCmo) => EfeitoCustosDeslocamentoPldeCmo = efeitoCustosDeslocamentoPldeCmo;
        public void AtualizarResultadoFinal(double resultadoFinal) => ResultadoFinal = resultadoFinal;
    }
}