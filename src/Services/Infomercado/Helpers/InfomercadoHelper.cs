using System;
using System.Globalization;
using OfficeOpenXml;

namespace Infomercado.Helpers
{
    public class InfomercadoHelper
    {
        public static string FormatarCnpj(string cnpj) => Convert.ToUInt64(cnpj, CultureInfo.CurrentCulture)
            .ToString(@"00\.000\.000\/0000\-00", CultureInfo.CurrentCulture);
        
        public static int ObterPrimeiraLinhaDados(ExcelWorksheet worksheet, string primeiraColuna)
        {
            var linha = 1;

            var valorLinha = worksheet.Cells[linha, 2].Value?.ToString();

            while (string.IsNullOrEmpty(valorLinha) || !valorLinha.Equals(primeiraColuna))
            {
                valorLinha = worksheet.Cells[linha, 2].Value?.ToString();
                linha++;
            }
                

            return linha;
        }
    }
}