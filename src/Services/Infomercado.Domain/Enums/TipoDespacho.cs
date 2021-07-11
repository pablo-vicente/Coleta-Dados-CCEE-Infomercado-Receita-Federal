using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum TipoDespacho
    {
        [Display(Name = "I - Programada e Despachada")]
        I = 1,
        [Display(Name = "IA - Programada e Despachada com CVU")]
        IA = 2,
        [Display(Name = "III - Não Programada e Não Despachada")]
        III = 3,
        [Display(Name = "IB - Programada e Despachada sem CVU")]
        IB = 4,
        [Display(Name = "IIA - Programada e Não Despachada com CVU")]
        IIA = 5,
        [Display(Name = "IIB - Programada e Não Despachada sem CVU")]
        IIB = 6,
        [Display(Name = "IIC - Programada e Não Despachada em Conjunto")]
        IIC = 7,
        [Display(Name = "II - Programada e Não Despachada")]
        II = 8
    }
}