using System;

namespace ReceitaFederal.Domain.Models
{
    public class MotivoSituacaoCadastral
    {

        public MotivoSituacaoCadastral(int id, string descricaoMotivo)
        {
            Id = id;
            DescricaoMotivo = descricaoMotivo;
        }

        public int Id { get; private set; }
        public string DescricaoMotivo { get; private set; }

        public void AtribuirDescricaoMotivo(string descricaoMotivo) => DescricaoMotivo = descricaoMotivo;
    }
}