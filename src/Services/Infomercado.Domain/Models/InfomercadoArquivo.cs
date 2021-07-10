using System;
using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Models
{
    public class InfoMercadoArquivo
    {
        public InfoMercadoArquivo(string nome, int ano, DateTime dataUltimaAtualizacao)
        {
            Nome = nome;
            Ano = ano;
            DataUltimaAtualizacao = dataUltimaAtualizacao;
            Lido = false;
        }
        
        public int Id { get; private set; }

        public string Nome { get; private set; }
        
        public int Ano { get; private set; }
        /// <summary>
        /// Data fonecida pelo site da CCEE
        /// </summary>
        public DateTime DataUltimaAtualizacao { get; private set; }
        /// <summary>
        /// Verifica se todas as abas da planilha InfoMercado foram importadas com sucesso.
        /// </summary>
        public bool Lido { get; private set; }

        public void AtualizarLido(bool lido)
        {
            Lido = lido;
        }
    }
}