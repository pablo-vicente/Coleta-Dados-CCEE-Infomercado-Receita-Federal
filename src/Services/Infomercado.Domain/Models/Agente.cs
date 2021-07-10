using System.Collections.Generic;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class Agente
    {
        public Agente(int codigo, string sigla, string nomeEmpresarial, string cnpj, Categoria categoria)
        {
            Codigo = codigo;
            Sigla = sigla;
            NomeEmpresarial = nomeEmpresarial;
            Cnpj = cnpj;
            Categoria = categoria;
            PerfisAgente = new List<PerfilAgente>();
        }

        public int Id { get; private set; }

        public int Codigo { get; private set; }
        
        public string Sigla { get; private set; }
        
        public string NomeEmpresarial { get; private set; }
        
        public string Cnpj { get; private set; }
        
        public Categoria Categoria { get; private set; }
        
        public virtual ICollection<PerfilAgente> PerfisAgente { get; }
        
        public void AtualizarSigla(string sigla) => Sigla = sigla;
        public void AtualizarNomeEmpresarial(string nomeEmpresarial) => NomeEmpresarial = nomeEmpresarial;
        public void AtualizarCnpj(string cnpj) => Cnpj = cnpj;
        public void AtualizarCategoria(Categoria categoria) => Categoria = categoria;

    }
}