using System.ComponentModel.DataAnnotations;

namespace ReceitaFederal.Domain.Enums
{
    public enum TipoSocio
    {
        [Display(Name = "Pessoa jurídica")]
        PessoalJuridica = 1,
        [Display(Name = "Pessoa física")]
        PessoaFisica = 2,
        [Display(Name = "Estrangeiro")]
        Estrangeiro = 3
    }
}