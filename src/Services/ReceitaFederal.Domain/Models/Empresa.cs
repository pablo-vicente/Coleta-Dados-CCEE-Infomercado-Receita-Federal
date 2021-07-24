using System;
using System.Collections.Generic;
using System.Globalization;
using ReceitaFederal.Domain.Enums;

namespace ReceitaFederal.Domain.Models
{
    public class Empresa
    {
        public Empresa(string cnpj, string razaoSocial, string nomeFantasia, SituacaoCadastral situacaoCadastral, int? motivoSituacaoCadastralId, DateTime? inicioAtividade, decimal capitalSocial, DateTime atualizacao)
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            SituacaoCadastral = situacaoCadastral;
            MotivoSituacaoCadastralId = motivoSituacaoCadastralId;
            InicioAtividade = inicioAtividade;
            CapitalSocial = capitalSocial;
            Atualizacao = atualizacao;
        }

        public int Id { get; private set; }
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public SituacaoCadastral SituacaoCadastral { get; private set; }
        public int? MotivoSituacaoCadastralId { get; private set; }
        public virtual MotivoSituacaoCadastral MotivoSituacaoCadastral { get; private set; }
        public DateTime? InicioAtividade { get; private set; }
        public decimal CapitalSocial { get; private set; }
        public DateTime Atualizacao { get; private set; }
        
        public void AtualizarSituacaoCadastral(SituacaoCadastral situacaoCadastral) => SituacaoCadastral = situacaoCadastral;

        public void AtualizarMotivoSituacaoCadastral(int motivoSituacaoCadastralId) => MotivoSituacaoCadastralId = motivoSituacaoCadastralId;
        public void AtualizarRazaoSocial(string razaoSocial) => RazaoSocial = razaoSocial;
        public void AtualizarNomeFantasia(string nomeFantasia) => NomeFantasia = nomeFantasia;

        public void AtualizarInicioAtividade(DateTime inicioAtividade)
        {
            if (inicioAtividade != DateTime.MinValue)
                InicioAtividade = inicioAtividade;
        }
        
        public void AtualizarCapitalSocial(decimal capitalSocial) => CapitalSocial = capitalSocial;
        public void AtualizarAtualizacao(DateTime atualizacao) => Atualizacao = atualizacao;
        
    }
}