using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum Classe
    {
        [Display( Name = "Distribuidor")]
        Distribuidor = 1,
        
        [Display( Name = "Gerador")]
        Gerador = 2,
        
        [Display( Name = "Importador")]
        Importador = 3,
        
        [Display( Name = "Comercializador")]
        Comercializador = 4,
        
        [Display( Name = "Transmissor")]
        Transmissor = 5,
        
        [Display( Name = "Produtor Independente")]
        ProdutorIndependente = 6,
        
        [Display( Name = "Autoprodutor")]
        Autoprodutor = 7,
        
        [Display( Name = "Consumidor Livre")]
        ConsumidorLivre = 8,
        
        [Display( Name = "Consumidor Especial")]
        ConsumidorEspecial = 9,
        
        [Display( Name = "Exportador")]
        Exportador = 10
    }
}