using System;

namespace ReceitaFederal.Domain.Models
{
    public class ReceitaFederalArquivo
    {
        public ReceitaFederalArquivo(string nome, DateTime atualizacao)
        {
            Nome = nome;
            Atualizacao = atualizacao;
            Lido = false;
        }
        
        public int Id { get; private set; }

        public string Nome { get; private set; }
        
        public DateTime Atualizacao { get; private set; }
        
        public bool Lido { get; private set; }

        public void AtualizarLido(bool lido)
        {
            Lido = lido;
        }
    }
}