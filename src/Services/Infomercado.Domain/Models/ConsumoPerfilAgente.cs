using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class ConsumoPerfilAgente
    {
        public ConsumoPerfilAgente(Patamar patamar, DateTime mes, double consumo, int idPerfilAgente)
        {
            Patamar = patamar;
            Mes = mes;
            Consumo = consumo;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }

        public Patamar Patamar { get; private set; }
        
        public DateTime Mes { get; private set; }

        public double Consumo { get; private set; }
        
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarConsumo(double consumo) => Consumo = consumo;
    }
}