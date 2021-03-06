using System.Collections.Generic;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class PerfilAgente
    {
        public PerfilAgente(int codigo, string sigla, Classe classe, Status? status, Submercado? submercado, bool varejista)
        {
            Codigo = codigo;
            Sigla = sigla;
            Classe = classe;
            Status = status;
            Submercado = submercado;
            Varejista = varejista;
            Contratos = new List<Contrato>();
            DadosGeracaoUsinas = new List<DadosGeracaoUsina>();
            Contabilizacaes = new List<Contabilizacao>();
            Encargos = new List<Encargo>();
            ReceitaVendaComercializacaoEnergiaNucleares = new List<ReceitaVendaComercializacaoEnergiaNuclear>();
            ReceitaVendaDistribuidoraCotistaEnergiaNucleares = new List<ReceitaVendaDistribuidoraCotistaEnergiaNuclear>();
            ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres = new List<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre>();
            ConsumoParcelaCargas = new List<ConsumoParcelaCarga>();
            ConsumoPerfilAgentes = new List<ConsumoPerfilAgente>();
            ConsumoGeracaoPerfilAgentes = new List<ConsumoGeracaoPerfilAgente>();
            RepasseRiscoHidrologicos = new List<RepasseRiscoHidrologico>();
        }

        public int Id { get; private set; }
        
        public int Codigo { get; private set; }
        public string Sigla { get; private set; }
        
        public Classe Classe { get; private set; }

        public Status? Status { get; private set; }

        public Submercado? Submercado { get; private set; }

        public bool Varejista { get; private set; }

        public int IdAgente { get; private set; }

        public virtual Agente Agente { get; private set; }
        
        public virtual ICollection<Contrato> Contratos { get; }
        public virtual ICollection<DadosGeracaoUsina> DadosGeracaoUsinas { get; }
        public virtual ICollection<Contabilizacao> Contabilizacaes { get; }
        public virtual ICollection<Encargo> Encargos { get; }
        public virtual ICollection<DadoMre> DadosMres { get; }
        public virtual ICollection<ReceitaVendaDistribuidoraCotistaGarantiaFisica> ReceitaVendaDistribuidoraCotistaGarantiaFisicas { get; }
        public virtual ICollection<ReceitaVendaComercializacaoEnergiaNuclear> ReceitaVendaComercializacaoEnergiaNucleares { get; }
        public virtual ICollection<ReceitaVendaDistribuidoraCotistaEnergiaNuclear> ReceitaVendaDistribuidoraCotistaEnergiaNucleares { get; }
        public virtual ICollection<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre> ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres { get; }
        public virtual ICollection<ConsumoParcelaCarga> ConsumoParcelaCargas { get; }
        public virtual ICollection<ConsumoPerfilAgente> ConsumoPerfilAgentes { get; }
        public virtual ICollection<ConsumoGeracaoPerfilAgente> ConsumoGeracaoPerfilAgentes { get; }
        public virtual ICollection<RepasseRiscoHidrologico> RepasseRiscoHidrologicos { get; }

        public void AtualizarSigla(string sigla) => Sigla = sigla;
        public void AtualizarClasse(Classe classe) => Classe = classe;
        public void AtualizarStatus(Status? status) => Status = status;
        public void AtualizarSubmercado(Submercado? submercado) => Submercado = submercado;
        public void AtualizarVarejista(bool varejista) => Varejista = varejista;
    }
}