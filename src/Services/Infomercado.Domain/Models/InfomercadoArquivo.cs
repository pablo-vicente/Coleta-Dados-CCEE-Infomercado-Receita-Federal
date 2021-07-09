using System;
using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Models
{
    public class InfoMercadoArquivo
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        
        public int Ano { get; set; }
        /// <summary>
        /// Data fonecida pelo site da CCEE
        /// </summary>
        public DateTime DataUltimaAtualizacao { get; set; }
        /// <summary>
        /// Verifica se todas as abas da planilha InfoMercado foram importadas com sucesso.
        /// </summary>
        public bool Lido { get; set; }
    }
}