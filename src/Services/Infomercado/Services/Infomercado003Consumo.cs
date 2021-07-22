using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Infomercado.Domain.Enums;
using Infomercado.Domain.Helpers;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Infomercado.Helpers;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace InfoMercado.Services
{
    public class Infomercado003Consumo
    {
        private const string NomePlanilha = "003 Consumo";
        
        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ConsumoParcelaCargaRepository _consumoParcelaCargaRepository;
        private readonly ConsumoPerfilAgenteRepository _consumoPerfilAgenteRepository;
        private readonly ConsumoGeracaoPerfilAgenteRepository _consumoGeracaoPerfilAgenteRepository;
        
        public Infomercado003Consumo(ILogger<Infomercado003Consumo> logger, 
            PerfilAgenteRepository perfilAgenteRepository, 
            ConsumoParcelaCargaRepository consumoParcelaCargaRepository, 
            ConsumoPerfilAgenteRepository consumoPerfilAgenteRepository, 
            ConsumoGeracaoPerfilAgenteRepository consumoGeracaoPerfilAgenteRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _consumoParcelaCargaRepository = consumoParcelaCargaRepository;
            _consumoPerfilAgenteRepository = consumoPerfilAgenteRepository;
            _consumoGeracaoPerfilAgenteRepository = consumoGeracaoPerfilAgenteRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            _logger.LogInformation($"Importando {NomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[NomePlanilha];
            
            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            
            try
            {
                const string primeiraColunaTabela1 = "Mês";
                var primeiraLinhaTabela1 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela1);
                ImportarConsumoParcelaCarga(ano, worksheet, primeiraLinhaTabela1, perfisCadatrados);
                    
                const string primeiraColunaTabela2 = "Cód. Perfil";
                var primeiraLinhaTabela2 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela2, primeiraLinhaTabela1);
                ImportarConsumoPerfilAgenteRepository(ano, worksheet, primeiraLinhaTabela2, perfisCadatrados);
                
                const string primeiraColunaTabela3 = "Cód. Perfil";
                var primeiraLinhaTabela3 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela3, primeiraLinhaTabela2);
                ImportarConsumoGeracaoPerfilAgenteRepository(ano, worksheet, primeiraLinhaTabela3, perfisCadatrados);

            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{NomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarConsumoParcelaCarga(int ano, ExcelWorksheet worksheet, int primeiraLinha, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var consumoParcelaCargaCadastrados = _consumoParcelaCargaRepository.ReadByYear(ano).ToList();
            var consumoParcelaCargaNovos = new List<ConsumoParcelaCarga>();
            
            var linha = primeiraLinha;
            
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");
                var codigoPerfilAgente = int.Parse(worksheet.Cells[linha, 3].Value?.ToString());
                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato linha {linha} ignorada");
                
                var codigoCarga = int.Parse(worksheet.Cells[linha, 6].Value.ToString());
                var carga = worksheet.Cells[linha, 7].Value.ToString();
                
                var coluna = worksheet.Cells[primeiraLinha - 1, 8].Value.ToString()
                    .Equals("Cidade", StringComparison.InvariantCultureIgnoreCase)
                    ? 8
                    : 9;
                
                var cidade = worksheet.Cells[linha, coluna].Value?.ToString();
                coluna++;
                var estado = worksheet.Cells[linha, coluna].Value?.ToString();
                coluna++;
                var ramoAtividade = worksheet.Cells[linha, coluna].Value?.ToString();
                coluna++;
                var submercado = EnumHelper<Submercado>.GetValueFromName(worksheet.Cells[linha, coluna].Value?.ToString());
                coluna++;
                var dataTemp = worksheet.Cells[linha, coluna].Value?.ToString();
                DateTime? dataMigracao;
                if (string.IsNullOrEmpty(dataTemp) || dataTemp.Equals("0"))
                    dataMigracao = null;
                else if (DateTime.TryParse(dataTemp, CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.None, out var dtParse))
                    dataMigracao = dtParse;
                else
                    dataMigracao = DateTime.FromOADate(Convert.ToDouble(dataTemp));
                
                coluna++;
                var codigoPerfilDistribuidora = (int?) InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, coluna].Value?.ToString());
                coluna++;
                var siglaPerfilDistribuidora = worksheet.Cells[linha, coluna].Value?.ToString();
                coluna++;
                var capacidadeCarga = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, coluna].Value?.ToString());
                coluna++;
                var consumoAmbienteLivre = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, coluna].Value?.ToString());
                coluna++;
                var consumoAjustadoCativa = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, coluna].Value?.ToString());
                coluna++;
                var consumoAjustadoParcelaCarga = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, coluna].Value?.ToString());

                var consumoParcelaCarga =
                    consumoParcelaCargaCadastrados.FirstOrDefault(x =>
                        x.Mes == mes && x.IdPerfilAgente == perfilAgente.Id && x.CodigoCarga == codigoCarga) ??
                    consumoParcelaCargaNovos.FirstOrDefault(x =>
                        x.Mes == mes && x.IdPerfilAgente == perfilAgente.Id && x.CodigoCarga == codigoCarga);

                if (consumoParcelaCarga is null)
                {
                    consumoParcelaCarga = new ConsumoParcelaCarga(
                       mes,
                       codigoCarga,
                       carga,
                       cidade,
                       estado,
                       ramoAtividade,
                       submercado,
                       dataMigracao,
                       codigoPerfilDistribuidora,
                       siglaPerfilDistribuidora,
                       capacidadeCarga,
                       consumoAmbienteLivre,
                       consumoAjustadoCativa,
                       consumoAjustadoParcelaCarga,
                       perfilAgente.Id
                    );
                
                    consumoParcelaCargaNovos.Add(consumoParcelaCarga);
                }
                else
                {
                    consumoParcelaCarga.AtualizarCodigoCarga(codigoCarga);
                    consumoParcelaCarga.AtualizarCarga(carga);
                    consumoParcelaCarga.AtualizarCidade(cidade);
                    consumoParcelaCarga.AtualizarEstado(estado);
                    consumoParcelaCarga.AtualizarRamoAtividade(ramoAtividade);
                    consumoParcelaCarga.AtualizarSubmercado(submercado);
                    consumoParcelaCarga.AtualizarDataMigracao(dataMigracao);
                    consumoParcelaCarga.AtualizarCodigoPerfilDistribuidora(codigoPerfilDistribuidora);
                    consumoParcelaCarga.AtualizarSiglaPerfilDistribuidora(siglaPerfilDistribuidora);
                    consumoParcelaCarga.AtualizarCapacidadeCargaMW(capacidadeCarga);
                    consumoParcelaCarga.AtualizarConsumoAmbienteLivreMWh(consumoAmbienteLivre);
                    consumoParcelaCarga.AtualizarConsumoAjustadoParcelaCativaCargaLivreMWh(consumoAjustadoCativa);
                    consumoParcelaCarga.AtualizarConsumoAjustadoParcelaCargaMWh(consumoAjustadoParcelaCarga);
                    
                }
                
                linha++;
            }
            
            _consumoParcelaCargaRepository.Update(consumoParcelaCargaCadastrados.ToArray());
            _consumoParcelaCargaRepository.Create(consumoParcelaCargaNovos.ToArray());
            
        }
        
        private void ImportarConsumoPerfilAgenteRepository(int ano, ExcelWorksheet worksheet, int primeiraLinha, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var consumoPerfilAgenteCadastrados = _consumoPerfilAgenteRepository.ReadByYear(ano).ToList();
            var consumoPerfilAgenteNovos = new List<ConsumoPerfilAgente>();
            
            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
            while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
            {
                var colunaPatamar = worksheet.Cells[primeiraLinha - 1, 5].Value.ToString()
                    .Equals("Patamar", StringComparison.InvariantCultureIgnoreCase)
                    ? 5
                    : 6;
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato linha {linha} ignorada");
                
                var patamar = EnumHelper<Patamar>.GetValueFromName(worksheet.Cells[linha, colunaPatamar].Value.ToString());

                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var consumo = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var consumoPerfilAgente =
                        consumoPerfilAgenteCadastrados.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.Mes == dataInicioMes && x.Patamar == patamar) ??
                        consumoPerfilAgenteNovos.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.Mes == dataInicioMes && x.Patamar == patamar);

                    if (consumoPerfilAgente is null)
                    {
                        consumoPerfilAgente = new ConsumoPerfilAgente(
                           patamar,
                           dataInicioMes,
                           consumo,
                           perfilAgente.Id
                        );
                    
                        consumoPerfilAgenteNovos.Add(consumoPerfilAgente);
                    }
                    else
                        consumoPerfilAgente.AtualizarConsumo(consumo);
                }
                
                linha++;
            }
            
            _consumoPerfilAgenteRepository.Update(consumoPerfilAgenteCadastrados.ToArray());
            _consumoPerfilAgenteRepository.Create(consumoPerfilAgenteNovos.ToArray());
            
        }
        
        private void ImportarConsumoGeracaoPerfilAgenteRepository(int ano, ExcelWorksheet worksheet, int primeiraLinha, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var consumoGeracaoPerfilAgenteCadastrados = _consumoGeracaoPerfilAgenteRepository.ReadByYear(ano).ToList();
            var consumoGeracaoPerfilAgenteNovos = new List<ConsumoGeracaoPerfilAgente>();
            
            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
            while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
            {
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato linha {linha} ignorada");

                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var consumo = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var consumoGeracaoPerfilAgente =
                        consumoGeracaoPerfilAgenteCadastrados.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.Mes == dataInicioMes) ??
                        consumoGeracaoPerfilAgenteNovos.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.Mes == dataInicioMes);

                    if (consumoGeracaoPerfilAgente is null)
                    {
                        consumoGeracaoPerfilAgente = new ConsumoGeracaoPerfilAgente(
                           dataInicioMes,
                           consumo,
                           perfilAgente.Id
                        );
                    
                        consumoGeracaoPerfilAgenteNovos.Add(consumoGeracaoPerfilAgente);
                    }
                    else
                        consumoGeracaoPerfilAgente.AtualizarConsumoMWh(consumo);
                }
                
                linha++;
            }
            
            _consumoGeracaoPerfilAgenteRepository.Update(consumoGeracaoPerfilAgenteCadastrados.ToArray());
            _consumoGeracaoPerfilAgenteRepository.Create(consumoGeracaoPerfilAgenteNovos.ToArray());
            
        }
        
    }
}