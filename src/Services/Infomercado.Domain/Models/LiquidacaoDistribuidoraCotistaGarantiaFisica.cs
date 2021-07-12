using System;

namespace Infomercado.Domain.Models
{
    public class LiquidacaoDistribuidoraCotistaGarantiaFisica
    {
        public LiquidacaoDistribuidoraCotistaGarantiaFisica(DateTime mesAno, double fatorAdequacao,
            double receitaFixaPreliminarRs, double receitaFixaAjustadaRs, double receitaFixaTotalRs,
            double custosAdministrativosRs,
            int idParcelaUsina)
        {
            MesAno = mesAno;
            FatorAdequacao = fatorAdequacao;
            ReceitaFixaPreliminarRs = receitaFixaPreliminarRs;
            ReceitaFixaAjustadaRs = receitaFixaAjustadaRs;
            ReceitaFixaTotalRs = receitaFixaTotalRs;
            CustosAdministrativosRs = custosAdministrativosRs;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }
        
        public DateTime MesAno { get; private set; }

        public double FatorAdequacao { get; private set; }

        public double ReceitaFixaPreliminarRs { get; private set; }
        
        public double ReceitaFixaAjustadaRs { get; private set; }

        public double ReceitaFixaTotalRs { get; private set; }

        public double CustosAdministrativosRs { get; private set; }
        
        public int IdParcelaUsina { get; private set; }

        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarFatorAdequacao(double fatorAdequacao) => FatorAdequacao = fatorAdequacao;
        public void AtualizarReceitaFixaPreliminarRS(double receitaFixaPreliminarRS) => ReceitaFixaPreliminarRs = receitaFixaPreliminarRS;
        public void AtualizarReceitaFixaAjustadaRS(double receitaFixaAjustadaRS) => ReceitaFixaAjustadaRs = receitaFixaAjustadaRS;
        public void AtualizarReceitaFixaTotalRS(double receitaFixaTotalRS) => ReceitaFixaTotalRs = receitaFixaTotalRS;
        public void AtualizarCustosAdministrativosRS(double custosAdministrativosRS) => CustosAdministrativosRs = custosAdministrativosRS;
    }
}