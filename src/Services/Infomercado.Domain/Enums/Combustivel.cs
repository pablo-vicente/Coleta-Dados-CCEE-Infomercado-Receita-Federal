using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum Combustivel
    {
        [Display(Name = "Potencial hidráulico")]
        Potencialhidraulico = 1,
        
        [Display(Name = "Gás Natural")]
        GasNatural = 2,
        
        [Display(Name = "Óleo Diesel")]
        OleoDiesel = 3,
        
        [Display(Name = "Óleo Combustível")]
        OleoCombustivel = 4,
        
        [Display(Name = "Carvão Mineral")]
        CarvaoMineral = 5,
        
        [Display(Name = "Bagaço de Cana de Açúcar")]
        BagacodeCanadeAcucar = 6,
        
        [Display(Name = "Outros Energéticos de Petróleo")]
        OutrosEnergeticosdePetroleo = 7,
        
        [Display(Name = "Urânio")]
        Uranio = 8,
        
        [Display(Name = "Licor Negro")]
        LicorNegro = 9,

        [Display(Name = "Carvão Vegetal")]
        CarvaoVegetal = 10,
        
        [Display(Name = "Resíduos Florestais")]
        ResiduosFlorestais = 11,
        
        [Display(Name = "Gás de Refinaria")]
        GasdeRefinaria = 12,
        
        [Display(Name = "Calor de Processo - CM")]
        CalordeProcessoCM = 13,
        
        [Display(Name = "Casca de Arroz")]
        CascadeArroz = 14,
        
        [Display(Name = "Cinética do vento")]
        Cineticadovento = 15,
        
        [Display(Name = "Calor de Processo - OF")]
        CalordeProcessoOF = 16,
        
        [Display(Name = "Biogás - RU")]
        BiogasRU = 17,
        
        [Display(Name = "Gás de Alto Forno - Biomassa")]
        GasdeAltoFornoBiomassa = 18,
        
        [Display(Name = "Capim Elefante")]
        CapimElefante = 19,
        
        [Display(Name = "Radiação solar")]
        RadiacaoSolar = 20,
        
        [Display(Name = "Gás de Alto Forno - CM")]
        GasdeAltoFornoCM = 21,
        
        [Display(Name = "Biogás-AGR")]
        BiogasAGR = 22,
        
        [Display(Name = "Lenha")]
        Lenha = 23,
        
    }
}