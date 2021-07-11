using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class DadoMre
    {
        public DadoMre(Patamar patamar, DateTime data, double garantiaFisicaMWh, TipoMre tipoMre, int idPerfilAgente)
        {
            Patamar = patamar;
            Data = data;
            GarantiaFisicaMWh = garantiaFisicaMWh;
            TipoMre = tipoMre;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }

        public Patamar Patamar { get; private set; }

        public DateTime Data { get; private set; }

        public double GarantiaFisicaMWh { get; private set; }

        public TipoMre TipoMre { get; private set; }

        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarGarantiaFisicaMWh(double garantiaFisicaMWh) => GarantiaFisicaMWh = garantiaFisicaMWh;
    }
}