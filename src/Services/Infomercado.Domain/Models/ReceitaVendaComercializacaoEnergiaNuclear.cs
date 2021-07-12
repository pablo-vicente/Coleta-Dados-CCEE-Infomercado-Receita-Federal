using System;

namespace Infomercado.Domain.Models
{
    public class ReceitaVendaComercializacaoEnergiaNuclear
    {
        public ReceitaVendaComercializacaoEnergiaNuclear(DateTime mesAno, double fatorAdequacao, double receitaFixaPreliminarRs, double receitaFixaAjustadaRs, double receitaVendaTotalRs, double custoAdministrativosRs, int idPerfilAgente)
        {
            MesAno = mesAno;
            FatorAdequacao = fatorAdequacao;
            ReceitaFixaPreliminarRs = receitaFixaPreliminarRs;
            ReceitaFixaAjustadaRs = receitaFixaAjustadaRs;
            ReceitaVendaTotalRs = receitaVendaTotalRs;
            CustoAdministrativosRs = custoAdministrativosRs;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }
        
        public DateTime MesAno { get; private set; }

        public double FatorAdequacao { get; private set; }
        
        public double ReceitaFixaPreliminarRs { get; private set; }

        public double ReceitaFixaAjustadaRs { get; private set; }

        public double ReceitaVendaTotalRs { get; private set; }

        public double CustoAdministrativosRs { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarFatorAdequacao(double fatorAdequacao) => FatorAdequacao = fatorAdequacao;
        public void AtualizarReceitaFixaPreliminarRS(double receitaFixaPreliminarRS) => ReceitaFixaPreliminarRs = receitaFixaPreliminarRS;
        public void AtualizarReceitaFixaAjustadaRS(double receitaFixaAjustadaRS) => ReceitaFixaAjustadaRs = receitaFixaAjustadaRS;
        public void AtualizarReceitaVendaTotalRS(double receitaVendaTotalRS) => ReceitaVendaTotalRs = receitaVendaTotalRS;
        public void AtualizarCustoAdministrativosRS(double custoAdministrativosRS) => CustoAdministrativosRs = custoAdministrativosRS;
    }
}