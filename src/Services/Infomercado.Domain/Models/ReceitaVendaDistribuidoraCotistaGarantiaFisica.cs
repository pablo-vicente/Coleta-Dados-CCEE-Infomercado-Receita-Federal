using System;

namespace Infomercado.Domain.Models
{
    public class ReceitaVendaDistribuidoraCotistaGarantiaFisica
    {
        public ReceitaVendaDistribuidoraCotistaGarantiaFisica(DateTime mesAno, double ajustes, double fatorRateioCotas, double receitaVenda, int idPerfilAgente, int idParcelaUsina)
        {
            MesAno = mesAno;
            Ajustes = ajustes;
            FatorRateioCotas = fatorRateioCotas;
            ReceitaVenda = receitaVenda;
            IdPerfilAgente = idPerfilAgente;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }
        
        public DateTime MesAno { get; private set; }

        public double Ajustes { get; private set; }

        public double FatorRateioCotas { get; private set; }

        public double ReceitaVenda { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        public virtual PerfilAgente PerfilAgente { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarAjustes(double ajustes) => Ajustes = ajustes;
        public void AtualizarFatorRateioCotas(double fatorRateioCotas) => FatorRateioCotas = fatorRateioCotas;
        public void AtualizarReceitaVenda(double receitaVenda) => ReceitaVenda = receitaVenda;
    }
}