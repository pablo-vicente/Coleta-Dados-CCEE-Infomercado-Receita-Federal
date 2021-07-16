using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class RepasseRiscoHidrologico
    {
        public RepasseRiscoHidrologico(TipoRepasseRiscoHidrologico tipoRepasseRiscoHidrologico, int? idParcelaUsina, int idPerfilAgente, int? semana, Patamar? patamar, DateTime mes, double riscoHidrologico)
        {
            TipoRepasseRiscoHidrologico = tipoRepasseRiscoHidrologico;
            IdParcelaUsina = idParcelaUsina;
            IdPerfilAgente = idPerfilAgente;
            Semana = semana;
            Patamar = patamar;
            Mes = mes;
            RiscoHidrologico = riscoHidrologico;
        }

        public int Id { get; private set; }
        
        public TipoRepasseRiscoHidrologico TipoRepasseRiscoHidrologico { get; private set; }

        public int? IdParcelaUsina { get; private set; }
        public ParcelaUsina? ParcelaUsina { get; private set; }

        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }
        
        public int? Semana { get; private set; }
        
        public Patamar? Patamar { get; private set; }

        public DateTime Mes { get; private set; }

        public double RiscoHidrologico { get; private set; }

        public void AtualizarRiscoHidrologico(double riscoHidrologico) => RiscoHidrologico = riscoHidrologico;

    }
}