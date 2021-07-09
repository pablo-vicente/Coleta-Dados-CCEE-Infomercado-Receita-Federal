using System.Collections.Generic;
using Infomercado.Domain.Models;

namespace Infomercado.Domain.Interfaces
{
    public interface IInfomercadoArquivoRepository : IRepository<InfoMercadoArquivo, int>
    {
        InfoMercadoArquivo? ReadLatest();
        IEnumerable<InfoMercadoArquivo> ReadByName(string nome);
        IEnumerable<InfoMercadoArquivo> ListarInfoMercadoArquivoNaoImportados();
    }
}