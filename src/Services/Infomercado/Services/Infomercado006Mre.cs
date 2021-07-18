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
    public class Infomercado006Mre
    {
        private const string PrimeiraColuna = "Cód. Perfil de Agente";

        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly DadoMreRepository _dadoMreRepository;
        
        private List<DadoMre> _dadoMreCadastrados = new ();
        private List<DadoMre> _dadoMreNovos = new();
        private List<PerfilAgente> _perfisCadatrados = new ();

        public Infomercado006Mre(ILogger<Infomercado006Mre> logger, PerfilAgenteRepository perfilAgenteRepository, DadoMreRepository dadoMreRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _dadoMreRepository = dadoMreRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            const string nomePlanilha = "006 MRE";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            _perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            _dadoMreCadastrados = _dadoMreRepository.ReadByYear(ano).ToList();
            
            try
            {
                var primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);
                ImportarTipoMre(worksheet, primeiraLinha, nomePlanilha, TipoMre.PorFatorDisponibilidade);
                    
                primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinha);
                ImportarTipoMre(worksheet, primeiraLinha, nomePlanilha, TipoMre.ParaMre);
                
                _dadoMreRepository.Update(_dadoMreCadastrados.ToArray());
                _dadoMreRepository.Create(_dadoMreNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarTipoMre(ExcelWorksheet worksheet, int primeiraLinha, string nomePlanilha, TipoMre tipoMre)
        {
            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
           
            while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
            {
                _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");
                var perfilAgente = _perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);

                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");
                    
                var patamar = EnumHelper<Patamar>.GetValueFromName(worksheet.Cells[linha, 5].Value.ToString());

                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var valor = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var dadoMre =
                        _dadoMreCadastrados.FirstOrDefault(x =>
                            x.Data == dataInicioMes && x.IdPerfilAgente == perfilAgente.Id && x.TipoMre == tipoMre &&
                            x.Data == dataInicioMes && x.Patamar == patamar) ??
                        _dadoMreNovos.FirstOrDefault(x =>
                            x.Data == dataInicioMes && x.IdPerfilAgente == perfilAgente.Id && x.TipoMre == tipoMre &&
                            x.Data == dataInicioMes && x.Patamar == patamar);

                    if (dadoMre is null)
                    {
                        dadoMre = new DadoMre(patamar, dataInicioMes, valor, tipoMre, perfilAgente.Id);
                        _dadoMreNovos.Add(dadoMre);
                    }
                    else
                        dadoMre.AtualizarGarantiaFisicaMWh(valor);
                        
                }

                linha++;
            }
            
        }
    }
}