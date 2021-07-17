using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Infomercado.Domain.Enums;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Infomercado.Helpers;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace InfoMercado.Services
{
    public class Infomercado001Contratos
    {
        private const string PrimeiraColuna = "Cód. Agente";

        private readonly ILogger _logger;
        private readonly ContratoRepository _contratoRepository;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        
        private List<Contrato> _contratosCadastrados = new ();
        private List<Contrato> _contratosNovos = new();
        private List<PerfilAgente> _perfisCadatrados = new ();

        public Infomercado001Contratos(ILogger<Infomercado001Contratos> logger, ContratoRepository contratoRepository, PerfilAgenteRepository perfilAgenteRepository)
        {
            _logger = logger;
            _contratoRepository = contratoRepository;
            _perfilAgenteRepository = perfilAgenteRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            const string nomePlanilha = "001 Contratos";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            _perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            _contratosCadastrados = _contratoRepository.ReadByYear(ano).ToList();
            
            try
            {
                var primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);
                ImportarTipoContrato(worksheet, primeiraLinha, nomePlanilha, TipoContrato.Venda);
                    
                primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinha);
                ImportarTipoContrato(worksheet, primeiraLinha, nomePlanilha, TipoContrato.Compra);
                
                _contratoRepository.Update(_contratosCadastrados.ToArray());
                _contratoRepository.Create(_contratosNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarTipoContrato(ExcelWorksheet worksheet, int primeiraLinha, string nomePlanilha, TipoContrato tipoContrato)
        {
            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
            _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");
            while (int.TryParse(worksheet.Cells[linha, 4].Value?.ToString(), out var codigoPerfilAgente))
            {
                var perfilAgente = _perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);

                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");
                    
                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);

                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var valor = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var contrato =
                        _contratosCadastrados.FirstOrDefault(x =>
                            x.Data == dataInicioMes && x.IdPerfilAgente == perfilAgente.Id && x.Tipo == tipoContrato) ??
                        _contratosNovos.FirstOrDefault(x =>
                            x.Data == dataInicioMes && x.IdPerfilAgente == perfilAgente.Id && x.Tipo == tipoContrato);

                    if (contrato is null)
                    {
                        contrato = new Contrato(dataInicioMes, valor, tipoContrato, perfilAgente.Id);
                        _contratosNovos.Add(contrato);
                    }
                    else
                        contrato.AtualizaContratacaoMWm(valor);
                        
                }

                linha++;
            }
            
        }
    }
}