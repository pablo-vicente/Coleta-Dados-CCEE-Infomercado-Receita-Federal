using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum Status
    {
        [Display(Name = "Ativo")]
        Ativo = 1,
        [Display(Name = "Encerrado")]
        Encerrado = 2,
        [Display(Name = "Em Adesão")]
        EmAdesao = 3, 
        [Display(Name = "Perfil Específico")]
        PerfilEspecifico = 4
    }
}