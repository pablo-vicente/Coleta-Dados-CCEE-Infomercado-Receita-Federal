using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class Contrato
    {
        public Contrato(){}
        
        public Contrato(DateTime data, double contratacaoMWm, TipoContrato tipo, int idPerfilAgente)
        {
            Data = data;
            ContratacaoMWm = contratacaoMWm;
            Tipo = tipo;
            IdPerfilAgente = idPerfilAgente;
        }

        public int Id { get; private set; }
        
        public DateTime Data { get;  private set; }
        
        public double ContratacaoMWm { get; private set; }
        
        public TipoContrato Tipo { get;  private set; }
        
        public int IdPerfilAgente { get; private  set; }
        
        public virtual PerfilAgente PerfilAgente { get; private set; }

        public void AtualizaContratacaoMWm(double contratacaoMWm) => ContratacaoMWm = contratacaoMWm;
    }
}