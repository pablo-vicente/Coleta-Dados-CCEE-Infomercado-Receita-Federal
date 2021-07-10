using System;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Infomercado.Helpers
{
    public class InfomercadoHelper
    {
        public static string FormatarCnpj(string cnpj) => Convert.ToUInt64(cnpj, CultureInfo.CurrentCulture)
            .ToString(@"00\.000\.000\/0000\-00", CultureInfo.CurrentCulture);
        
        public static int ObterPrimeiraLinhaDados(ExcelWorksheet worksheet, string primeiraColuna, int linhaInicial = 1)
        {
            var valorLinha = worksheet.Cells[linhaInicial, 2].Value?.ToString();

            while (string.IsNullOrEmpty(valorLinha) || !valorLinha.Equals(primeiraColuna))
            {
                valorLinha = worksheet.Cells[linhaInicial, 2].Value?.ToString();
                linhaInicial++;
            }
                

            return linhaInicial;
        }

        public static int ObterPrimeiraColunaMes(int linnha, ExcelWorksheet planilha)
        {
            linnha--;
            var col = 1;
            while (!DateTime.TryParse(planilha.Cells[linnha, col].Value?.ToString(), out var mes))
                col++;
            return col;
        }
        
        public static double ConverteDouble(string? valor) =>
            string.IsNullOrEmpty(valor)
                ? 0
                : valor.Contains('%')
                    ? double.Parse(valor.TrimEnd('%'), CultureInfo.CurrentCulture) / 100
                    : double.Parse(valor, CultureInfo.CurrentCulture);
    }
}