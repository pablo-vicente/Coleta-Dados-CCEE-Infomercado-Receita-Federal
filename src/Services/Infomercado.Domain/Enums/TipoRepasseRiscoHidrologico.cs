using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum TipoRepasseRiscoHidrologico
    {
        [Display(Name = "Garantia Física de Repasse do Risco Hidrológico Modulada e Ajustada da parcela de usina")]
        GFRRHModEAjuParUsina = 1,
        
        [Display(Name = "Garantia Física Modulada Ajustada de Repasse do Risco Hidrológico da parcela de usina")]
        GFModAjuRRHParUsina = 2,
        
        [Display(Name = "Valor do Risco Hidrológico do ACR da parcela de usina (VRHp,r,w)")]
        ValorRisHidroACRParUsina = 3,
        
        [Display(Name = "Quantidade Alocada do Próprio Submercado de Energia Secundária de Repasse do Risco Hidrológico para a parcela de usina")]
        QntAlocProprioSubEnerSecunRRHParcUsina = 4,
        
        [Display(Name = "Direito à Energia Secundária para Repasse do Risco Hidrológico")]
        DireitoEnerSecunRRH = 5,
        
        [Display(Name = "Quantidade Alocada de Outros Submercados de Energia Secundária de Repasse do Risco Hidrológico para a parcela de usina")]
        QntAlocOutrosSubEnerSecunRRHParcUsina = 6,
        
        [Display(Name = "Energia Secundária para fins de Repasse do Risco Hidrológico")]
        EnerSecunFinsRRH = 7,
        
        
        [Display(Name = "Montante de Contratos do Ambiente Regulado de Repasse do Risco Hidrológico da parcela de usina")]
        MontContrAmbRegRHParcUsina = 8,
        
        [Display(Name = "Quantidade de Garantia Física de Repasse do Risco Hidrológico")]
        QntGFRRH = 9,
        
        [Display(Name = "Valor de Repasse do Risco Hidrológico do ACR da parcela de usina")]
        ValorRRHACRParcUsina = 10,
        
        [Display(Name = "Resultado Final do Repasse do Risco Hidrológico")]
        ResulFinalRRH = 11,
        
        [Display(Name = "Fator de Rateio do Valor Total de Repasse do Risco Hidrológico do ACR")]
        FatorRateioValorTTRRHACR = 12,
        
        [Display(Name = "Resultado Final do Repasse do Risco Hidrológico do perfil do agente “a”, comprador de contratos do ACR sujeitos a repassae do risco hidrológico")]
        ResultFinalRRHPerfilAgenteACRSujRH = 13,
        
        [Display(Name = "Efeito de Repasse do Risco Hidrológico")]
        EfeitoRRH = 14,
    }
}