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
    public class Infomercado002Usinas
    {
        private const string PrimeiraColuna = "Código do Ativo";

        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly UsinaRepository _usinaRepository;
        
        public Infomercado002Usinas(ILogger<Infomercado002Usinas> logger, 
            PerfilAgenteRepository perfilAgenteRepository, 
            UsinaRepository usinaRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _usinaRepository = usinaRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            const string nomePlanilha = "002 Usinas";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();

            var usinasCadastrados = _usinaRepository.ReadAll().ToList();
            var usinasNovos = new List<Usina>();
            
            try
            {
                var primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);
                var linha = primeiraLinha;
            
                while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoAtivo))
                {
                    _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");

                    var siglaAtivo = worksheet.Cells[linha, 3].Value.ToString();
                    var submercado = EnumHelper<Submercado>.GetValueFromName(worksheet.Cells[linha, 11].Value.ToString());
                    var uf = worksheet.Cells[linha, 12].Value.ToString();
                    
                    var usina = usinasCadastrados.FirstOrDefault(x => x.CodigoAtivo == codigoAtivo) ??
                                usinasNovos.FirstOrDefault(x => x.CodigoAtivo == codigoAtivo);

                    if (usina is null)
                    {
                        usina = new Usina(codigoAtivo, siglaAtivo, submercado,uf);
                        usinasNovos.Add(usina);
                    }
                    else
                    {
                        usina.AtualizarSigla(siglaAtivo);
                        usina.AtualizarSubmercado(submercado);
                        usina.AtualizarUf(uf);
                    }
                    
                    var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 5].Value.ToString());
                    var siglaparcelaUsina = worksheet.Cells[linha, 6].Value.ToString();
                    var caracteristicaParcela = EnumHelper<CaracteristicaParcelaUsina>.GetValueFromName(worksheet.Cells[linha, 13].Value.ToString());
                    var dataTemp = worksheet.Cells[linha, 24].Value?.ToString();
                    var dataInicioOperacaoComercial = string.IsNullOrEmpty(dataTemp)
                        ? (DateTime?) null
                        : DateTime.Parse(dataTemp, CultureInfo.CurrentCulture);

                    var parcelaUsina = usina.ParcelasUsina.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);

                    if (parcelaUsina is null)
                    {
                        parcelaUsina = new ParcelaUsina(codigoParcelaUsina, siglaparcelaUsina, caracteristicaParcela, dataInicioOperacaoComercial);
                        usina.ParcelasUsina.Add(parcelaUsina);
                    }
                    else
                    {
                        parcelaUsina.AtualizarSigla(siglaparcelaUsina);
                        parcelaUsina.AtualizarcaracteristicaParcelaUsina(caracteristicaParcela);
                        parcelaUsina.AtualizarcaracteristicaDataInícioOperacaoComercialCcee(dataInicioOperacaoComercial);
                    }
                    
                    
                    var codigoPerfil = int.Parse(worksheet.Cells[linha, 21].Value.ToString());
                    var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfil);
                    
                    if (perfilAgente is null)
                        throw new ApplicationException($"Pefil de agente {codigoPerfil} não encontrato");
                    
                    var cegEmpreendimento = worksheet.Cells[linha, 4].Value?.ToString();
                    var tipoDespacho = EnumHelper<TipoDespacho>.GetValueFromName(worksheet.Cells[linha, 7].Value.ToString());
                    var participanteRateioPerda = worksheet.Cells[linha, 8].Value.ToString().Equals("Sim");
                    var fontePrimariaEnergia = EnumHelper<FonteEnergiaPrimaria>.GetValueFromName(worksheet.Cells[linha, 9].Value.ToString());
                    var combustivel = EnumHelper<Combustivel>.GetValueFromName(worksheet.Cells[linha, 10].Value.ToString());
                    var participanteMre = worksheet.Cells[linha, 14].Value.ToString().Equals("Sim");
                    var participanteRegimeCotas = worksheet.Cells[linha, 15].Value.ToString().Equals("Sim");
                    var taxaDescontoUsina = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 16].Value?.ToString());
                    var capacidadeUsinaMw = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 17].Value?.ToString());
                    var garantiaFisicaMWm = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 18].Value?.ToString());
                    var fatorOperacaoComercial = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 19].Value?.ToString());
                    var fatorPerdasInternas = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 20].Value?.ToString());
                   
                    var dataColuna = worksheet.Cells[linha, 25].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var mes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    var geracaoCentroGravidadeMwm = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 26].Value?.ToString());
                    var geracaoTesteCentroGravidade = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 27].Value?.ToString());
                    var garantiaFisicaCentroGravidadeMWm = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 28].Value?.ToString());
                    var geracaoSegurancaEnergetica = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 29].Value?.ToString());
                    var geracaoRestricaoOperacaoConstrainedOn = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 30].Value?.ToString());
                    var geracaoManutencaoReservaOperativa = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 31].Value?.ToString());


                    var dadosGeracaoUsina = parcelaUsina.DadosGeracaoUsina
                        .FirstOrDefault(x => x.IdPerfilAgente == perfilAgente.Id && x.Mes == mes);

                    if (dadosGeracaoUsina is null)
                    {
                        dadosGeracaoUsina = new DadosGeracaoUsina(
                            cegEmpreendimento,
                            tipoDespacho, 
                            participanteRateioPerda, 
                            fontePrimariaEnergia,
                            combustivel,
                            participanteMre,
                            participanteRegimeCotas,
                            taxaDescontoUsina,
                            capacidadeUsinaMw,
                            garantiaFisicaMWm,
                            fatorOperacaoComercial,
                            fatorPerdasInternas,
                            mes,
                            geracaoCentroGravidadeMwm,
                            geracaoTesteCentroGravidade,
                            garantiaFisicaCentroGravidadeMWm,
                            geracaoSegurancaEnergetica,
                            geracaoRestricaoOperacaoConstrainedOn,
                            geracaoManutencaoReservaOperativa,
                            parcelaUsina.Id,
                            perfilAgente.Id
                        );
                        parcelaUsina.DadosGeracaoUsina.Add(dadosGeracaoUsina);
                        
                    }
                    else
                    {
                        dadosGeracaoUsina.AtualizarCegempreendimento(cegEmpreendimento);
                        dadosGeracaoUsina.AtualizarTipoDespacho(tipoDespacho);
                        dadosGeracaoUsina.AtualizarParticipanteRateioPerdas(participanteRateioPerda);
                        dadosGeracaoUsina.AtualizaFonteEnergiaPrimaria(fontePrimariaEnergia);
                        dadosGeracaoUsina.AtualizarCombustivel(combustivel);
                        dadosGeracaoUsina.AtualizarParticipanteMre(participanteMre);
                        dadosGeracaoUsina.AtualizarParticipanteRegimeCotas(participanteRegimeCotas);
                        dadosGeracaoUsina.AtualizarTaxaDescontoUsina(taxaDescontoUsina); 
                        dadosGeracaoUsina.AtualizarCapacidadeUsinaMW(capacidadeUsinaMw); 
                        dadosGeracaoUsina.AtualizarGarantiaFisicaMWm(garantiaFisicaMWm);
                        dadosGeracaoUsina.AtualizarFatorOperacaoComercial(fatorOperacaoComercial);
                        dadosGeracaoUsina.AtualizarFatorPerdasInternas(fatorPerdasInternas);
                        dadosGeracaoUsina.AtualizarGeracaoCentroGravidadeMWm(geracaoTesteCentroGravidade);
                        dadosGeracaoUsina.AtualizarGeracaoTesteCentroGravidadeMWm(geracaoTesteCentroGravidade);
                        dadosGeracaoUsina.AtualizarGarantiaFisicaCentroGravidadeMWm(garantiaFisicaCentroGravidadeMWm);
                        dadosGeracaoUsina.AtualizarGeracaoSegurancaEnergeticaMWm(geracaoSegurancaEnergetica);
                        dadosGeracaoUsina.AtualizarGeracaoRestricaoOperacaoConstrainedOnMwm(geracaoRestricaoOperacaoConstrainedOn);
                        dadosGeracaoUsina.AtualizarGeracaoManutencaoReserveOperativaMWm(geracaoManutencaoReservaOperativa);
                    }
                    
                    linha++;
                }
                
                _usinaRepository.Update(usinasCadastrados.ToArray());
                _usinaRepository.Create(usinasNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

    }
}