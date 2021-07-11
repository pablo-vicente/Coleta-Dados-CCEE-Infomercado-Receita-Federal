using System.ComponentModel.DataAnnotations;

namespace Infomercado.Domain.Enums
{
    public enum FonteEnergiaPrimaria
    {
        [Display(Name = "Hidráulica")]
        Hidraulica = 1,
        [Display(Name = "Térmica bi-Combustível - gás/óleo")]
        TérmicaBiCombustivel = 2,
        [Display(Name = "Térmica a Óleo")]
        TérmicaOleo = 3,
        [Display(Name = "Hidráulica CGH")]
        HidraulicaCGH = 4,
        [Display(Name = "Térmica a Carvão Mineral")]
        TérmicaCarvaoMineral = 5,
        [Display(Name = "Hidráulica PCH")]
        HidraulicaPCH = 6,
        [Display(Name = "Térmica a Gás")]
        TérmicaGas = 7,
        [Display(Name = "Térmica a Biomassa")]
        TérmicaBiomassa = 8,
        [Display(Name = "Térmica - Outros")]
        TérmicaOutros = 9,
        [Display(Name = "Térmica Nuclear")]
        TérmicaNuclear = 10,
        [Display(Name = "Eólica")]
        Eolica = 11,
        [Display(Name = "Térmica a GNL")]
        TérmicaGNL = 12,
        [Display(Name = "Solar Fotovoltaica")]
        SolarFotovoltaica = 13,
        [Display(Name = "Térmica Reação Exotérmica")]
        TermicaReacaoExotermica = 14
    }
}