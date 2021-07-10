using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace InfoMercado.Services
{
    public class InfomercadoArquivoService
    {
        private readonly string _caminhoDownload;
        private readonly ILogger _logger;
        private readonly InfomercadoArquivoRepository _infomercadoArquivoRepository;
        private readonly Infomercado001Contratos _infomercado001Contratos;
        private readonly Infomercado007PerfisAgentes _infomercado007PerfisAgentes;

        public InfomercadoArquivoService(
            ILogger<InfomercadoArquivoService> logger, 
            IConfiguration configuration, 
            InfomercadoArquivoRepository infomercadoArquivoRepository,
            Infomercado001Contratos infomercado001Contratos,
            Infomercado007PerfisAgentes infomercado007PerfisAgentes)
        {
            _logger = logger;
            _infomercadoArquivoRepository = infomercadoArquivoRepository;
            
            _infomercado001Contratos = infomercado001Contratos;
            _infomercado007PerfisAgentes = infomercado007PerfisAgentes;
            
            _caminhoDownload = configuration["Planilhas_InfoMercado"];;
        }

        /// <summary>
        /// ADICIONA NO BANCO ARQUIVOS QUE FORAM BAIXADOS ANTES DA CCEE ATUALIZÁ-LOS
        /// </summary>
        private void IncluirArquivosSalvosRepositorioBanco()
        {
            _logger.LogInformation("Salvando arquivos no banco de dados...");
            
            var arquivos = new DirectoryInfo(_caminhoDownload).GetFiles("*.xlsx").OrderBy(x=>x.Name).ToList();
            var infoMercadosArquivos = _infomercadoArquivoRepository.ReadAll().ToList();

            var infoMercadoArquivos = new HashSet<InfoMercadoArquivo>();
            foreach (var arquivo in arquivos)
            {
                var infoMercadoArquivo = infoMercadosArquivos
                    .FirstOrDefault(x => x.Nome.Equals(arquivo.Name, StringComparison.InvariantCultureIgnoreCase));

                if (infoMercadoArquivo is not null || arquivo.Name.StartsWith("~$"))
                    continue;

                // Primeiro data entre [], segundo ano (4 digitos)
                var dadosArquivo = arquivo.Name.Split("InfoMercado Dados Individuais ");

                var ano = int.Parse(dadosArquivo[1][..4], CultureInfo.CurrentCulture);
                var dataAtualizacao = DateTime.Parse(dadosArquivo[0][..10], CultureInfo.CurrentCulture);
                infoMercadoArquivos.Add(new InfoMercadoArquivo(arquivo.Name, ano, dataAtualizacao));
            }

            _infomercadoArquivoRepository.Create(infoMercadoArquivos.ToArray());
        }

        public void ImportarArquivosInfomercado()
        {
            IncluirArquivosSalvosRepositorioBanco();
            var infoMercadoArquivos = _infomercadoArquivoRepository.ListarInfoMercadoArquivoNaoImportados().ToList();
            
            foreach (var infoMercadoArquivo in infoMercadoArquivos)
            {
                var path = Path.Combine(_caminhoDownload, infoMercadoArquivo.Nome);
                var fileInfo = new FileInfo(path);
                
                _logger.LogInformation($"Lendo arquivo \"{fileInfo.Name}\" CCEE. Tamanho: {fileInfo.Length / 1024 / 1024} MB");
                using var excelPackage = new ExcelPackage(fileInfo);

                // _infomercado007PerfisAgentes.ImportarPlanilha(excelPackage);
                _infomercado001Contratos.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                
                // Primeiro Importa os Perfis devido as referências
                // Importar007ListaPerfis(excelPackage, infoMercadoArquivo);

                // Importar001Contratos(excelPackage, infoMercadoArquivo);
                // Importar002Usinas(excelPackage, infoMercadoArquivo);
                // Importar003Consumo(excelPackage, infoMercadoArquivo);
                // Importar004Contabilizacao(excelPackage, infoMercadoArquivo);
                // Importar005Encargos(excelPackage, infoMercadoArquivo);
                // Importar006MRE(excelPackage, infoMercadoArquivo);
                //
                // Importar008Cotista(excelPackage, infoMercadoArquivo);
                // Importar009Proinfa(excelPackage, infoMercadoArquivo);
                // Importar010MCSD(excelPackage, infoMercadoArquivo);
                // Importar011RRH(excelPackage, infoMercadoArquivo);
                // Importar012DisponibilidadeLeilao(excelPackage, infoMercadoArquivo);

                // Registra o Arquivo InfoMercado como lido
                // infoMercadoArquivo.AtualizarLido(true);
                _infomercadoArquivoRepository.Update(infoMercadoArquivo);
                _logger.LogInformation($"Arquivo \"{fileInfo.Name}\" importado com sucesso.");
            }
        }

    }
}