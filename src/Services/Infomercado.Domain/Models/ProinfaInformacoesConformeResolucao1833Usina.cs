using System;
using Infomercado.Domain.Enums;

namespace Infomercado.Domain.Models
{
    public class ProinfaInformacoesConformeResolucao1833Usina
    {
        public ProinfaInformacoesConformeResolucao1833Usina(string ccve, FonteEnergiaPrimaria fonteEnergiaPrimaria, DateTime data, double geracaoCentroGravidadeMWm, int idParcelaUsina)
        {
            Ccve = ccve;
            FonteEnergiaPrimaria = fonteEnergiaPrimaria;
            Data = data;
            GeracaoCentroGravidadeMWm = geracaoCentroGravidadeMWm;
            IdParcelaUsina = idParcelaUsina;
        }

        public int Id { get; private set; }

        public string Ccve { get; private set; }
        
        public FonteEnergiaPrimaria FonteEnergiaPrimaria { get; private set; }

        public DateTime Data { get; private set; }

        public double GeracaoCentroGravidadeMWm { get; private set; }
        
        public int IdParcelaUsina { get; private set; }
        
        public virtual ParcelaUsina ParcelaUsina { get; private set; }

        public void AtualizarCcve(string ccve) => Ccve = ccve;
        public void AtualizarFonteEnergiaPrimaria(FonteEnergiaPrimaria fonteEnergiaPrimaria) => FonteEnergiaPrimaria = fonteEnergiaPrimaria;
        public void AtualizarGeracaoCentroGravidadeMWm(double geracaoCentroGravidadeMWm) => GeracaoCentroGravidadeMWm = geracaoCentroGravidadeMWm;
    }
}