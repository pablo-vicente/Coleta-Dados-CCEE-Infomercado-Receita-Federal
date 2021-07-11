using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum CaracteristicaParcelaUsina
    {
        [Display(Name = "Operação Normal")]
        OperaçãoNormal = 1,
        
        [Display(Name = "Exportação")]
        Exportação = 2,
        
        [Display(Name = "Comercial")]
        Comercial = 3
    }
}