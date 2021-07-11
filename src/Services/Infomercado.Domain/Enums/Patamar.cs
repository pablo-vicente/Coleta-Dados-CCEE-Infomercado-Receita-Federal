using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum Patamar
    {
        [Display(Name = "PESADO")]
        Pesado = 1,
        [Display(Name = "MEDIO")]
        Medio = 2,
        [Display(Name = "LEVE")]
        Leve = 3
    }
}