using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum TipoMre
    {
        [Display(Name = "Garantia física modulada ajustada pelo fator de disponibilidade")]
        PorFatorDisponibilidade = 1,
        [Display(Name = "Garantia física modulada ajustada para o MRE")]
        ParaMre = 2
    }
}