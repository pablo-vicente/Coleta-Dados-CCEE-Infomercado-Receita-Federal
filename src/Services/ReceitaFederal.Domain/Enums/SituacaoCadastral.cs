using System.ComponentModel.DataAnnotations;

namespace ReceitaFederal.Domain.Enums
{
    public enum SituacaoCadastral
    {
        [Display(Name = "Pessoa jurídica")]
        Nula = 1,
        [Display(Name = "Ativa")]
        Ativa = 2,
        [Display(Name = "Suspensa")]
        Suspensa = 3,
        [Display(Name = "Inapta")]
        Inapta = 4,
        [Display(Name = "Baixada")]
        Baixada = 8
    }
}