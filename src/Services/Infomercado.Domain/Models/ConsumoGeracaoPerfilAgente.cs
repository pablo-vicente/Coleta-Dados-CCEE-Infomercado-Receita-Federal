using System;

namespace Infomercado.Domain.Models
{
    public class ConsumoGeracaoPerfilAgente
    {
        public ConsumoGeracaoPerfilAgente(DateTime mes, double consumoMWh, int idPerfilAgente)
        {
            Mes = mes;
            ConsumoMWh = consumoMWh;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }

        public DateTime Mes { get; private set; }
        
        public double ConsumoMWh { get; private set; }
        
        public int IdPerfilAgente { get; private set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizarConsumoMWh(double consumoMWh) => ConsumoMWh = consumoMWh;
    }
}