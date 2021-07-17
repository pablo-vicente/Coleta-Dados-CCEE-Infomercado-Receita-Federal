using System;
using System.Collections.Generic;
using System.Linq;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Infomercado.Helpers;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace InfoMercado.Services
{
    public class Infomercado008Cotista
    {
        private const string NomePlanilha = "008 Cotista";
        private const string PrimeiraColuna = "Mês/Ano";

        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ParcelaUsinaRepository _parcelaUsinaRepository;
        private readonly LiquidacaoDistribuidoraCotistaGarantiaFisicaRepository _liquidacaoDistribuidoraCotistaGarantiaFisicaRepository;
        private readonly ReceitaVendaDistribuidoraCotistaGarantiaFisicaRepository _receitaVendaDistribuidoraCotistaGarantiaFisicaRepository;
        private readonly ReceitaVendaComercializacaoEnergiaNuclearRepository _receitaVendaComercializacaoEnergiaNuclearRepository;
        private readonly ReceitaVendaDistribuidoraCotistaEnergiaNuclearRepository _receitaVendaDistribuidoraCotistaEnergiaNuclearRepository;
        
        public Infomercado008Cotista(ILogger<Infomercado008Cotista> logger, 
            PerfilAgenteRepository perfilAgenteRepository, 
            ParcelaUsinaRepository parcelaUsinaRepository, 
            LiquidacaoDistribuidoraCotistaGarantiaFisicaRepository liquidacaoDistribuidoraCotistaGarantiaFisicaRepository, 
            ReceitaVendaDistribuidoraCotistaGarantiaFisicaRepository receitaVendaDistribuidoraCotistaGarantiaFisicaRepository, 
            ReceitaVendaDistribuidoraCotistaEnergiaNuclearRepository receitaVendaDistribuidoraCotistaEnergiaNuclearRepository, 
            ReceitaVendaComercializacaoEnergiaNuclearRepository receitaVendaComercializacaoEnergiaNuclearRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _parcelaUsinaRepository = parcelaUsinaRepository;
            _liquidacaoDistribuidoraCotistaGarantiaFisicaRepository = liquidacaoDistribuidoraCotistaGarantiaFisicaRepository;
            _receitaVendaDistribuidoraCotistaGarantiaFisicaRepository = receitaVendaDistribuidoraCotistaGarantiaFisicaRepository;
            _receitaVendaComercializacaoEnergiaNuclearRepository = receitaVendaComercializacaoEnergiaNuclearRepository;
            _receitaVendaDistribuidoraCotistaEnergiaNuclearRepository = receitaVendaDistribuidoraCotistaEnergiaNuclearRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            
            _logger.LogInformation($"Importando {NomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[NomePlanilha];

            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            var parcelaUsinasCadastrados = _parcelaUsinaRepository.ReadAll().ToList();
            
            try
            {
                var primeiraLinhaTabela1 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);
                ImportarLiquidacaoPoDistribuidoraCotistaGarantiaFisica(ano, worksheet, primeiraLinhaTabela1, parcelaUsinasCadastrados);
                    
                var primeiraLinhaTabela2 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinhaTabela1);
                ImportarReceitaVendaDistribuidoraCotistaGarantiaFisica(ano, worksheet, primeiraLinhaTabela2, parcelaUsinasCadastrados, perfisCadatrados);
                
                var primeiraLinhaTabela3 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinhaTabela2);
                ImportarReceitaVendaComercializacaoEnergiaNuclear(ano, worksheet, primeiraLinhaTabela3, perfisCadatrados);
                
                var primeiraLinhaTabela4 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinhaTabela3);
                ImportarReceitaVendaDistribuidoraCotistaEnergiaNuclear(ano, worksheet, primeiraLinhaTabela4, perfisCadatrados);

            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{NomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarLiquidacaoPoDistribuidoraCotistaGarantiaFisica(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados)
        {
            var liquidacaoDistribuidoraCotistaGarantiaFisicaCadastrados =
                _liquidacaoDistribuidoraCotistaGarantiaFisicaRepository
                    .ReadByYear(ano)
                    .ToList();

            var liquidacaoDistribuidoraCotistaGarantiaFisicaNovos = new List<LiquidacaoDistribuidoraCotistaGarantiaFisica>();
            
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                // _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 3].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);
                    
                if (parcelaUsina is null)
                    throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado");
                
                var fatorAdequacao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 5].Value?.ToString());
                var receitaFixaPreliminar = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 6].Value?.ToString());
                var receitaFixaAjustada = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value?.ToString());
                var receitaFixaTotal = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value?.ToString());
                var custosAdministrativos = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value?.ToString());

                var liquidacaoDistribuidoraCotistaGarantiaFisica =
                    liquidacaoDistribuidoraCotistaGarantiaFisicaCadastrados.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes) ??
                    liquidacaoDistribuidoraCotistaGarantiaFisicaNovos.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes);
                
                if (liquidacaoDistribuidoraCotistaGarantiaFisica is null)
                {
                    liquidacaoDistribuidoraCotistaGarantiaFisica = new LiquidacaoDistribuidoraCotistaGarantiaFisica(
                        mes,
                        fatorAdequacao,
                        receitaFixaPreliminar,
                        receitaFixaAjustada,
                        receitaFixaTotal,
                        custosAdministrativos,
                        parcelaUsina.Id
                    );
                    
                    liquidacaoDistribuidoraCotistaGarantiaFisicaNovos.Add(liquidacaoDistribuidoraCotistaGarantiaFisica);
                }
                else
                {
                    liquidacaoDistribuidoraCotistaGarantiaFisica.AtualizarFatorAdequacao(fatorAdequacao);
                    liquidacaoDistribuidoraCotistaGarantiaFisica.AtualizarReceitaFixaPreliminarRS(receitaFixaPreliminar);
                    liquidacaoDistribuidoraCotistaGarantiaFisica.AtualizarReceitaFixaAjustadaRS(receitaFixaAjustada);
                    liquidacaoDistribuidoraCotistaGarantiaFisica.AtualizarReceitaFixaTotalRS(receitaFixaTotal);
                    liquidacaoDistribuidoraCotistaGarantiaFisica.AtualizarCustosAdministrativosRS(custosAdministrativos);
                }
                
                linha++;
            }
            
            _liquidacaoDistribuidoraCotistaGarantiaFisicaRepository.Update(liquidacaoDistribuidoraCotistaGarantiaFisicaCadastrados.ToArray());
            _liquidacaoDistribuidoraCotistaGarantiaFisicaRepository.Create(liquidacaoDistribuidoraCotistaGarantiaFisicaNovos.ToArray());
        }
        
        private void ImportarReceitaVendaDistribuidoraCotistaGarantiaFisica(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var receitaVendaDistribuidoraCotistaGarantiaFisicaCadastrados =
                _receitaVendaDistribuidoraCotistaGarantiaFisicaRepository
                    .ReadByYear(ano)
                    .ToList();

            var receitaVendaDistribuidoraCotistaGarantiaFisicaNovos = new List<ReceitaVendaDistribuidoraCotistaGarantiaFisica>();
            
            _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                var codigoPerfilAgente = int.Parse(worksheet.Cells[linha, 3].Value?.ToString());
                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);

                if (perfilAgente is null)
                {
                    _logger.LogWarning($"Pefil de agente {codigoPerfilAgente} não encontrato linha {linha} ignorada");
                    linha++;
                    continue;
                }
                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 5].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);

                if (parcelaUsina is null)
                {
                    _logger.LogWarning($"Parcela usina {codigoParcelaUsina} não encontrado linha {linha} ignorada");
                    linha++;
                    continue;
                }

                var ajustes = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value?.ToString());
                var fatorRateioCotas = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value?.ToString());
                var receitaVenda = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value?.ToString());

                var receitaVendaDistribuidoraCotistaGarantiaFisica =
                    receitaVendaDistribuidoraCotistaGarantiaFisicaCadastrados.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes) ??
                    receitaVendaDistribuidoraCotistaGarantiaFisicaNovos.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes);
                
                if (receitaVendaDistribuidoraCotistaGarantiaFisica is null)
                {
                    receitaVendaDistribuidoraCotistaGarantiaFisica = new ReceitaVendaDistribuidoraCotistaGarantiaFisica(
                        mes,
                        ajustes,
                        fatorRateioCotas,
                        receitaVenda,
                        perfilAgente.Id,
                        parcelaUsina.Id
                    );
                    
                    receitaVendaDistribuidoraCotistaGarantiaFisicaNovos.Add(receitaVendaDistribuidoraCotistaGarantiaFisica);
                }
                else
                {
                    receitaVendaDistribuidoraCotistaGarantiaFisica.AtualizarAjustes(ajustes);
                    receitaVendaDistribuidoraCotistaGarantiaFisica.AtualizarFatorRateioCotas(fatorRateioCotas);
                    receitaVendaDistribuidoraCotistaGarantiaFisica.AtualizarReceitaVenda(receitaVenda);
                }
                
                linha++;
            }
            
            _receitaVendaDistribuidoraCotistaGarantiaFisicaRepository.Update(receitaVendaDistribuidoraCotistaGarantiaFisicaCadastrados.ToArray());
            _receitaVendaDistribuidoraCotistaGarantiaFisicaRepository.Create(receitaVendaDistribuidoraCotistaGarantiaFisicaNovos.ToArray());
        }
        
        private void ImportarReceitaVendaComercializacaoEnergiaNuclear(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var receitaVendaComercializacaoEnergiaNuclearCadastrados =
                _receitaVendaComercializacaoEnergiaNuclearRepository
                    .ReadByYear(ano)
                    .ToList();

            var receitaVendaComercializacaoEnergiaNuclearNovos = new List<ReceitaVendaComercializacaoEnergiaNuclear>();
            
            _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                var codigoPerfilAgente = int.Parse(worksheet.Cells[linha, 3].Value?.ToString());
                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                    
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");

                var fatorAdequacao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 5].Value?.ToString());
                var receitaFixaPreliminar = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 6].Value?.ToString());
                var receitaFixaAjustada = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value?.ToString());
                var receitaVendaTotal = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value?.ToString());
                var custosAdministrativos = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value?.ToString());


                var receitaVendaComercializacaoEnergiaNuclear =
                    receitaVendaComercializacaoEnergiaNuclearCadastrados.FirstOrDefault(x =>
                        x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes) ??
                    receitaVendaComercializacaoEnergiaNuclearNovos.FirstOrDefault(x =>
                        x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes);
                
                if (receitaVendaComercializacaoEnergiaNuclear is null)
                {
                    receitaVendaComercializacaoEnergiaNuclear = new ReceitaVendaComercializacaoEnergiaNuclear(
                        mes,
                        fatorAdequacao,
                        receitaFixaPreliminar,
                        receitaFixaAjustada,
                        receitaVendaTotal,
                        custosAdministrativos,
                        perfilAgente.Id
                    );
                    
                    receitaVendaComercializacaoEnergiaNuclearNovos.Add(receitaVendaComercializacaoEnergiaNuclear);
                }
                else
                {
                    receitaVendaComercializacaoEnergiaNuclear.AtualizarFatorAdequacao(fatorAdequacao);
                    receitaVendaComercializacaoEnergiaNuclear.AtualizarReceitaFixaPreliminarRS(receitaFixaPreliminar);
                    receitaVendaComercializacaoEnergiaNuclear.AtualizarReceitaFixaAjustadaRS(receitaFixaAjustada);
                    receitaVendaComercializacaoEnergiaNuclear.AtualizarReceitaVendaTotalRS(receitaVendaTotal);
                    receitaVendaComercializacaoEnergiaNuclear.AtualizarCustoAdministrativosRS(custosAdministrativos);
                }
                
                linha++;
            }
            
            _receitaVendaComercializacaoEnergiaNuclearRepository.Update(receitaVendaComercializacaoEnergiaNuclearCadastrados.ToArray());
            _receitaVendaComercializacaoEnergiaNuclearRepository.Create(receitaVendaComercializacaoEnergiaNuclearNovos.ToArray());
        }
        
        private void ImportarReceitaVendaDistribuidoraCotistaEnergiaNuclear(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var receitaVendaDistribuidoraCotistaEnergiaNuclearCadastrados =
                _receitaVendaDistribuidoraCotistaEnergiaNuclearRepository
                    .ReadByYear(ano)
                    .ToList();

            var receitaVendaDistribuidoraCotistaEnergiaNuclearNovos = new List<ReceitaVendaDistribuidoraCotistaEnergiaNuclear>();
            
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var codigoPerfilAgente = int.Parse(worksheet.Cells[linha, 3].Value?.ToString());
                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                    
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");

                var ajustes = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 5].Value?.ToString());
                var fatorRateioCotas = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 6].Value?.ToString());
                var receitaVenda = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value?.ToString());

                var receitaVendaDistribuidoraCotistaEnergiaNuclear =
                    receitaVendaDistribuidoraCotistaEnergiaNuclearCadastrados.FirstOrDefault(x =>
                        x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes) ??
                    receitaVendaDistribuidoraCotistaEnergiaNuclearNovos.FirstOrDefault(x =>
                        x.IdPerfilAgente == perfilAgente.Id && x.MesAno == mes);
                
                if (receitaVendaDistribuidoraCotistaEnergiaNuclear is null)
                {
                    receitaVendaDistribuidoraCotistaEnergiaNuclear = new ReceitaVendaDistribuidoraCotistaEnergiaNuclear(
                        mes,
                        ajustes,
                        fatorRateioCotas,
                        receitaVenda,
                        perfilAgente.Id
                    );
                    
                    receitaVendaDistribuidoraCotistaEnergiaNuclearNovos.Add(receitaVendaDistribuidoraCotistaEnergiaNuclear);
                }
                else
                {
                    receitaVendaDistribuidoraCotistaEnergiaNuclear.AtualizarAjustes(ajustes);
                    receitaVendaDistribuidoraCotistaEnergiaNuclear.AtualizarFatorRateiroCotas(fatorRateioCotas);
                    receitaVendaDistribuidoraCotistaEnergiaNuclear.AtualizarReceitaVenda(receitaVenda);
                }
                
                linha++;
            }
            
            _receitaVendaDistribuidoraCotistaEnergiaNuclearRepository.Update(receitaVendaDistribuidoraCotistaEnergiaNuclearCadastrados.ToArray());
            _receitaVendaDistribuidoraCotistaEnergiaNuclearRepository.Create(receitaVendaDistribuidoraCotistaEnergiaNuclearNovos.ToArray());
        }

    }
}