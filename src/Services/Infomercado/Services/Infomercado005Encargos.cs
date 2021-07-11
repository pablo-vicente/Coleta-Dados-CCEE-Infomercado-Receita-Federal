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
    public class Infomercado005Encargos
    {
        private const string PrimeiraColuna = "Mês/Ano";

        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ParcelaUsinaRepository _parcelaUsinaRepository;
        private readonly EncargoRepository _encargoRepository;

        public Infomercado005Encargos(ILogger<Infomercado005Encargos> logger,
            PerfilAgenteRepository perfilAgenteRepository, 
            ParcelaUsinaRepository parcelaUsinaRepository, 
            EncargoRepository encargoRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _parcelaUsinaRepository = parcelaUsinaRepository;
            _encargoRepository = encargoRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            const string nomePlanilha = "005 Encargos";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            var parcelasUsinaCadatrados = _parcelaUsinaRepository.ReadAll().ToList();
            
            var encargosCadastrados = _encargoRepository.ReadByYear(ano).ToList();
            var encargosNovos = new List<Encargo>();

            var linha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);

            try
            {
                while (DateTime.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var mes))
                {
                    _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");

                    var codigoPerfilAgente = int.Parse(worksheet.Cells[linha, 3].Value?.ToString());
                    var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                    
                    if (perfilAgente is null)
                        throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");
                    
                    var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 6].Value.ToString());
                    
                    var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);
                    
                    if (parcelaUsina is null)
                        throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado");
                    
                    var restricaoConstrainedOff = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 8].Value.ToString());
                    var restricaoConstrainedOn = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 9].Value.ToString());
                    var compensacaoSincrona = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 10].Value.ToString());
                    var outrosServicosAncilares = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 11].Value.ToString());     
                    var despachoSegurancaEnergetica = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 12].Value.ToString()); 
                    var deslocamentoUsinaHidreletrica = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 13].Value.ToString());
                    var ressarcimentoDeslocEnergetico = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 14].Value.ToString());
                    var ressarDespaPotenciaOperativa = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 15].Value.ToString());
                    var encargoImportacaoEnergia = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, 16].Value.ToString());

                    var encargo = encargosCadastrados.FirstOrDefault(x =>
                                      x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.Mes == mes) ??
                                  encargosNovos.FirstOrDefault(x =>
                                      x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.Mes == mes);

                    if (encargo is null)
                    {
                        encargo = new Encargo(mes, 
                            restricaoConstrainedOff, 
                            restricaoConstrainedOn, 
                            compensacaoSincrona,
                            outrosServicosAncilares, 
                            despachoSegurancaEnergetica, 
                            deslocamentoUsinaHidreletrica,
                            ressarcimentoDeslocEnergetico,
                            ressarDespaPotenciaOperativa,
                            encargoImportacaoEnergia,
                            perfilAgente.Id,
                            parcelaUsina.Id);
                        encargosNovos.Add(encargo);
                    }
                    else
                    {
                        encargo.AtualizarRestricaoConstrainedOff(restricaoConstrainedOff);
                        encargo.AtualizarRestricaoConstrainedOn(restricaoConstrainedOn);
                        encargo.AtualizarCompensacaoSincrona(compensacaoSincrona);
                        encargo.AtualizarOutrosServicosAncilares(outrosServicosAncilares);
                        encargo.AtualizarDespachoSegurancaEnergetica(despachoSegurancaEnergetica);
                        encargo.AtualizarDeslocamentoUsinaHidreletrica(deslocamentoUsinaHidreletrica);
                        encargo.AtualizarRessarcimentoDeslocamento(ressarcimentoDeslocEnergetico);
                        encargo.AtualizarRessarcimentoDespachoReservaPotenciaOperativa(ressarDespaPotenciaOperativa);
                        encargo.AtualizarEncargoImportacaoEnergia(encargoImportacaoEnergia);
                    }
                    
                    linha++;
                }
                
                _encargoRepository.Update(encargosCadastrados.ToArray());
                _encargoRepository.Create(encargosNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }
    }
}