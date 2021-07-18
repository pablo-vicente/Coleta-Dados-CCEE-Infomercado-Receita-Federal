using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Infomercado.Helpers;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace InfoMercado.Services
{
    public class Infomercado004Contabilizacao
    {
        private const string PrimeiraColuna = "Cód. Perfil";

        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ContabilizacaoRepository _contabilizacaoRepository;

        public Infomercado004Contabilizacao(ILogger<Infomercado004Contabilizacao> logger,
            PerfilAgenteRepository perfilAgenteRepository, 
            ContabilizacaoRepository contabilizacaoRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _contabilizacaoRepository = contabilizacaoRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            const string nomePlanilha = "004 Contabilização";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            var contabilizacoesCadastrados = _contabilizacaoRepository.ReadByYear(ano).ToList();
            var contabilizacoesNovos = new List<Contabilizacao>();

            var linha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);

            try
            {
               
                while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
                {
                    _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");
                    var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                    
                    if (perfilAgente is null)
                        throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");
                    
                    var mesAno = DateTime.Parse(worksheet.Cells[linha, 6].Value?.ToString(), CultureInfo.CurrentCulture);
                    var resultadoCurtoPrazo = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value?.ToString());
                    var compensacaoMre = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value?.ToString());
                    var encargosConsolidados = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value?.ToString());
                    var ajusteExposicaoFinanceira = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 10].Value?.ToString());
                    var ajusteAlivioRetroativo = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 11].Value?.ToString());
                    var efeitoContrPorDispon = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 12].Value?.ToString());
                    var efeitoContCotaGaranFisica = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 13].Value?.ToString());
                    var efeitoContrComeEnNuclear = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 14].Value?.ToString());
                    var ajusteRecontabilizacoes = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 15].Value?.ToString());
                    var ajusteMcsd = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 16].Value?.ToString());
                    var excedenteFinaEnReserva = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 17].Value?.ToString());
                    var efeitoCcearUsinasAptas = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 18].Value?.ToString());
                    var efeitoContratacaoItaipu = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 19].Value?.ToString());
                    var efeitoRRHidrologico = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 20].Value?.ToString());
                    var efeitoDeslocPlDeCmo = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 21].Value?.ToString());
                    var resultadoFinal = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 22].Value?.ToString());


                    var contabilizacao =
                        contabilizacoesCadastrados.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mesAno) ??
                        contabilizacoesNovos.FirstOrDefault(x =>
                            x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mesAno);

                    if (contabilizacao is null)
                    {
                        contabilizacao = new Contabilizacao(
                            mesAno,
                            resultadoCurtoPrazo,
                            compensacaoMre,
                            encargosConsolidados,
                            ajusteExposicaoFinanceira,
                            ajusteAlivioRetroativo,
                            efeitoContrPorDispon,
                            efeitoContCotaGaranFisica,
                            efeitoContrComeEnNuclear,
                            ajusteRecontabilizacoes,
                            ajusteMcsd,
                            excedenteFinaEnReserva,
                            efeitoCcearUsinasAptas,
                            efeitoContratacaoItaipu,
                            efeitoRRHidrologico,
                            efeitoDeslocPlDeCmo,
                            resultadoFinal,
                            perfilAgente.Id);
                        
                        contabilizacoesNovos.Add(contabilizacao);
                    }
                    else
                    {
                        contabilizacao.AtualizarResultadoMercadoCurtoPrazo(resultadoCurtoPrazo);
                        contabilizacao.AtualizarCompensacaoMre(compensacaoMre);
                        contabilizacao.AtualizarEncargosConsolidados(encargosConsolidados);
                        contabilizacao.AtualizarAjusteExposicaoFinanceira(ajusteExposicaoFinanceira);
                        contabilizacao.AtualizarAjusteAlivioRetroativo(ajusteAlivioRetroativo);
                        contabilizacao.AtualizarEfeitoContracaoPorDisponibilidade(efeitoContrPorDispon);
                        contabilizacao.AtualizarEfeitoContratacaoCotaGarantiaFisica(efeitoContCotaGaranFisica);
                        contabilizacao.AtualizarEfeitoContratacaoComercializacaoEnergiaNuclear(efeitoContrComeEnNuclear);
                        contabilizacao.AtualizarAjusteRecontabilizacoes(ajusteRecontabilizacoes);
                        contabilizacao.AtualizarAjusteMcsd(ajusteMcsd);
                        contabilizacao.AtualizarExcedenteFinanceiroEnergiaReserva(excedenteFinaEnReserva);
                        contabilizacao.AtualizarEfeitoCcearUsinasAptas(efeitoCcearUsinasAptas);
                        contabilizacao.AtualizarEfeitoContratacaoItaipu(efeitoContratacaoItaipu);
                        contabilizacao.AtualizarEfeitoRepasseRiscoHidrologico(efeitoRRHidrologico);
                        contabilizacao.AtualizarEfeitoCustosDeslocamentoPldeCmo(efeitoDeslocPlDeCmo);
                        contabilizacao.AtualizarResultadoFinal(resultadoFinal);
                    }
                    

                    linha++;
                }
                
                _contabilizacaoRepository.Update(contabilizacoesCadastrados.ToArray());
                _contabilizacaoRepository.Create(contabilizacoesNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }
    }
}