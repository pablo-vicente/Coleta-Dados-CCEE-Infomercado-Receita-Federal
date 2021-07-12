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
        private readonly Infomercado002Usinas _infomercado002Usinas;
        private readonly Infomercado004Contabilizacao _infomercado004Contabilizacao;
        private readonly Infomercado005Encargos _infomercado005Encargos;
        private readonly Infomercado006Mre _infomercado006Mre;
        private readonly Infomercado007PerfisAgentes _infomercado007PerfisAgentes;
        private readonly Infomercado008Cotista _infomercado008Cotista;

        public InfomercadoArquivoService(
            ILogger<InfomercadoArquivoService> logger, 
            IConfiguration configuration, 
            InfomercadoArquivoRepository infomercadoArquivoRepository,
            Infomercado001Contratos infomercado001Contratos,
            Infomercado002Usinas infomercado002Usinas,
            Infomercado004Contabilizacao infomercado004Contabilizacao,
            Infomercado005Encargos infomercado005Encargos,
            Infomercado006Mre infomercado006Mre,
            Infomercado007PerfisAgentes infomercado007PerfisAgentes, 
            Infomercado008Cotista infomercado008Cotista)
        {
            _logger = logger;
            _infomercadoArquivoRepository = infomercadoArquivoRepository;
            
            _infomercado001Contratos = infomercado001Contratos;
            _infomercado007PerfisAgentes = infomercado007PerfisAgentes;
            _infomercado008Cotista = infomercado008Cotista;
            _infomercado006Mre = infomercado006Mre;
            _infomercado005Encargos = infomercado005Encargos;
            _infomercado004Contabilizacao = infomercado004Contabilizacao;
            _infomercado002Usinas = infomercado002Usinas;

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

                _infomercado007PerfisAgentes.ImportarPlanilha(excelPackage);
                
                _infomercado001Contratos.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                _infomercado002Usinas.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                // _infomercado003Comsumo(excelPackage, infoMercadoArquivo.Ano);
                _infomercado004Contabilizacao.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                _infomercado005Encargos.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                _infomercado006Mre.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                _infomercado008Cotista.ImportarPlanilha(excelPackage, infoMercadoArquivo.Ano);
                
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