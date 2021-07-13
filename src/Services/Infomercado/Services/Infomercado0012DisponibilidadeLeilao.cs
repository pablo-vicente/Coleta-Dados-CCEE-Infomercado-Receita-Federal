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
    public class Infomercado0012DisponibilidadeLeilao
    {
        private const string PrimeiraColuna = "Mês/Ano";
        private const string NomePlanilha = "012 Disponibilidade Leilão";

        private readonly ILogger _logger;
        private readonly ParcelaUsinaRepository _parcelaUsinaRepository;
        private readonly MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository _montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository;
        private readonly GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository _geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository;

        public Infomercado0012DisponibilidadeLeilao(ILogger<Infomercado0012DisponibilidadeLeilao> logger,
            ParcelaUsinaRepository parcelaUsinaRepository,
            MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository,
            GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository)
        {
            _logger = logger;
            _parcelaUsinaRepository = parcelaUsinaRepository;
            _montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository = montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository;
            _geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository = geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            
            _logger.LogInformation($"Importando {NomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[NomePlanilha];

            var perfisCadatrados = _parcelaUsinaRepository.ReadAll().ToList();
            
            
            try
            {
                var primeiraLinhaTabel1 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);
                ImportarMontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade(ano, worksheet, primeiraLinhaTabel1, perfisCadatrados);
                    
                var primeiraLinhaTabelea2 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna, primeiraLinhaTabel1);
                ImportarGeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade(ano, worksheet, primeiraLinhaTabelea2, perfisCadatrados);
                
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{NomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarMontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados)
        {
            var montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeCadastrados = _montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository.ReadByYear(ano).ToList();

            var montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeNovos = new List<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>();
            
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 4].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);

                if (parcelaUsina is null)
                    throw new ApplicationException("Parcela usina {codigoParcelaUsina} não encontrado linha {linha} ignorada");

                var cegEmpreendimento = worksheet.Cells[linha, 3].Value.ToString();
                var leilao = worksheet.Cells[linha, 6].Value.ToString();
                var produto = worksheet.Cells[linha, 7].Value.ToString();
                var siglaLeilao = worksheet.Cells[linha, 8].Value.ToString();
                var fonteEnergiaPrimaria = EnumHelper<FonteEnergiaPrimaria>.GetValueFromName(worksheet.Cells[linha, 9].Value.ToString());
        
                var dataleilao = DateTime.Parse(worksheet.Cells[linha, 10].Value.ToString());
                var garantiaComprometida = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 11].Value?.ToString());
                var contratos = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 12].Value?.ToString());
                var geracaoDestinadaLeilao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 13].Value?.ToString());

                var montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade =
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeCadastrados
                        .FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes && x.Leilao == leilao &&
                            x.Produto == produto && x.CegeEmpreendimento == cegEmpreendimento) ??
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeNovos
                        .FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes && x.Leilao == leilao &&
                            x.Produto == produto && x.CegeEmpreendimento == cegEmpreendimento);
                
                if (montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade is null)
                {
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade = new MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade(
                        mes,
                        cegEmpreendimento,
                        leilao,
                        produto,
                        siglaLeilao,
                        fonteEnergiaPrimaria,
                        dataleilao,
                        garantiaComprometida,
                        contratos,
                        geracaoDestinadaLeilao,
                        parcelaUsina.Id
                    );
                    
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeNovos.Add(montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade);
                }
                else
                {
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarCegeEmpreendimento(cegEmpreendimento);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarLeilao(leilao);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarProduto(produto);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarSiglaLeilao(siglaLeilao);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarFonteEnergiaPrimaria(fonteEnergiaPrimaria);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarDataLeilao(dataleilao);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarGeracaoDestinadaLeilaoMWm(garantiaComprometida);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarContratosMWh(contratos);
                    montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade.AtualizarGeracaoDestinadaLeilaoMWm(geracaoDestinadaLeilao);
                }
                
                linha++;
            }
            
            _montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository.Update(montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeCadastrados.ToArray());
            _montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeRepository.Create(montanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidadeNovos.ToArray());
        }
        
        private void ImportarGeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade(int ano, ExcelWorksheet worksheet, int linha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados)
        {
            var geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeCadastrados = _geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository.ReadByYear(ano).ToList();

            var geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeNovos = new List<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade>();
            
            while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
            {
                _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");

                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 4].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);

                if (parcelaUsina is null)
                    throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado linha {linha} ignorada");

                var cegEmpreendimento = worksheet.Cells[linha, 3].Value.ToString();
                var garantiaFisica = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 6].Value?.ToString());
                var garantiaFisicaComprometida = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 7].Value.ToString());
                var geracao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value.ToString());
                var geracaoDestinadaLeilao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value.ToString());
                var geracaoLivre = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 10].Value.ToString());

                var geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade =
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeCadastrados.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes) ??
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeNovos.FirstOrDefault(x =>
                        x.IdParcelaUsina == parcelaUsina.Id && x.MesAno == mes);
                
                if (geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade is null)
                {
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade = new GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade(
                        mes,
                        cegEmpreendimento,
                        garantiaFisica,
                        garantiaFisicaComprometida,
                        geracao,
                        geracaoDestinadaLeilao,
                        geracaoLivre,
                        parcelaUsina.Id
                    );
                    
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeNovos.Add(geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade);
                }
                else
                {
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarCegeEmpreendimento(cegEmpreendimento);
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarGarantiaFisicaMWm(garantiaFisica);
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarGarantiaFisicaComprometidaMWm(garantiaFisicaComprometida);
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarGeracaoMWm(geracao);
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarGeracaoDestinadaLeilaoMWm(geracaoDestinadaLeilao);
                    geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade.AtualizarGeracaoLivreMWm(geracaoLivre);
                }
                
                linha++;
            }
            
            _geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository.Update(geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeCadastrados.ToArray());
            _geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeRepository.Create(geracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidadeNovos.ToArray());
        }
        
    }
}