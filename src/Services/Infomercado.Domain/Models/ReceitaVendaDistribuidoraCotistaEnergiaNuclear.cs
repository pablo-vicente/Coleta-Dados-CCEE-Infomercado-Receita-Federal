using System;

namespace Infomercado.Domain.Models
{
    public class ReceitaVendaDistribuidoraCotistaEnergiaNuclear
    {
        public ReceitaVendaDistribuidoraCotistaEnergiaNuclear(DateTime mesAno, double ajustes, double fatorRateiroCotas, double receitaVenda, int idPerfilAgente)
        {
            MesAno = mesAno;
            Ajustes = ajustes;
            FatorRateiroCotas = fatorRateiroCotas;
            ReceitaVenda = receitaVenda;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }
        
        public DateTime MesAno { get; private set; }

        public double Ajustes { get; private set; }

        public double FatorRateiroCotas { get; private set; }

        public double ReceitaVenda { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarAjustes(double ajustes) => Ajustes = ajustes;
        public void AtualizarFatorRateiroCotas(double fatorRateiroCotas) => FatorRateiroCotas = fatorRateiroCotas;
        public void AtualizarReceitaVenda(double receitaVenda) => ReceitaVenda = receitaVenda;
    }
}