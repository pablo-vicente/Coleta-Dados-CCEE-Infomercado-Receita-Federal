using System.Collections.Generic;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class Usina
    {
        public Usina(int codigoAtivo, string siglaAtivo, Submercado submercado, string uf)
        {
            CodigoAtivo = codigoAtivo;
            SiglaAtivo = siglaAtivo;
            Submercado = submercado;
            Uf = uf;
            ParcelasUsina = new List<ParcelaUsina>();
        }

        public int Id { get; private set; }
        
        public int CodigoAtivo { get; private set; }
        
        public string SiglaAtivo { get; private set; }
        
        public Submercado Submercado { get; private set; }
        
        public string Uf { get; private set; }

        public virtual ICollection<ParcelaUsina> ParcelasUsina { get; }
        
        public void AtualizarSigla(string sigla)
        {
            if (!string.IsNullOrEmpty(sigla))
                SiglaAtivo = sigla;
        }

        public void AtualizarSubmercado(Submercado submercado) => Submercado = submercado;
        public void AtualizarUf(string uf) => Uf = uf;

    }
}