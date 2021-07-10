using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum Categoria
    {
        [Display(Name = "Distribuição")]
        Distribuicao = 1,
        [Display(Name = "Geração")]
        Geracao = 2,
        [Display(Name = "Comercialização")]
        Comercializacao = 3,
        [Display(Name = "Transmissão")]
        Transmissao = 4
    }
}