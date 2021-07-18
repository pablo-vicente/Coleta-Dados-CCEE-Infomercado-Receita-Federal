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
    public class Infomercado011RRH
    {
        private const string NomePlanilha = "011 RRH";
        
        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ParcelaUsinaRepository _parcelaUsinaRepository;
        private readonly RepasseRiscoHidrologicoRepository _repasseRiscoHidrologicoRepository;
        
        public Infomercado011RRH(ILogger<Infomercado011RRH> logger, 
            PerfilAgenteRepository perfilAgenteRepository, 
            ParcelaUsinaRepository parcelaUsinaRepository, 
            RepasseRiscoHidrologicoRepository repasseRiscoHidrologicoRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _parcelaUsinaRepository = parcelaUsinaRepository;
            _repasseRiscoHidrologicoRepository = repasseRiscoHidrologicoRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            _logger.LogInformation($"Importando {NomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[NomePlanilha];
            
            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            var parcelaUsinasCadastrados = _parcelaUsinaRepository.ReadAll().ToList();
            
            
            
            try
            {
                const string primeiraColunaTabela1 = "Cód. Perfil ";
                var tabelas = new List<TipoRepasseRiscoHidrologico>
                {
                    TipoRepasseRiscoHidrologico.GFRRHModEAjuParUsina,
                    TipoRepasseRiscoHidrologico.GFModAjuRRHParUsina,
                    TipoRepasseRiscoHidrologico.ValorRisHidroACRParUsina,
                    TipoRepasseRiscoHidrologico.QntAlocProprioSubEnerSecunRRHParcUsina,
                    TipoRepasseRiscoHidrologico.DireitoEnerSecunRRH,
                    TipoRepasseRiscoHidrologico.QntAlocOutrosSubEnerSecunRRHParcUsina,
                    TipoRepasseRiscoHidrologico.EnerSecunFinsRRH,
                    TipoRepasseRiscoHidrologico.MontContrAmbRegRHParcUsina,
                    TipoRepasseRiscoHidrologico.QntGFRRH,
                    TipoRepasseRiscoHidrologico.ValorRRHACRParcUsina,
                    TipoRepasseRiscoHidrologico.ResulFinalRRH,
                    TipoRepasseRiscoHidrologico.FatorRateioValorTTRRHACR,
                    TipoRepasseRiscoHidrologico.ResultFinalRRHPerfilAgenteACRSujRH,
                    TipoRepasseRiscoHidrologico.EfeitoRRH,
                };

                var linha = 1;
                foreach (var tabela in tabelas)
                {
                    var repasseRiscoHidrologicoCadastrados = _repasseRiscoHidrologicoRepository.ReadByYearTipo(ano, tabela).ToList();
                    var repasseRiscoHidrologicoNovos = new List<RepasseRiscoHidrologico>();
                    
                    var primeiraLinha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela1, linha);
                    linha = primeiraLinha;
                    var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
                    
                    while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
                    {
                        _logger.LogInformation($"Importando {linha} - {tabela.GetHashCode()}/14-{tabela.ToString()}");
                        var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                        if (perfilAgente is null)
                            throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato");

                        var parcelaUsina = (ParcelaUsina)null!;
                        
                        if (int.TryParse(worksheet.Cells[linha, 4].Value?.ToString(), out var codigoParcelaUsina) && codigoParcelaUsina != 0)
                        {
                            parcelaUsina = parcelaUsinasCadastrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);
                            if (parcelaUsina is null)
                                throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado");
                        }
                        
                        var semanaString = worksheet.Cells[linha, 6].Value?.ToString();
                        var semana = (semanaString is null || !semanaString.Contains("ª"))
                            ? (int?) null
                            : int.Parse(semanaString.Split("ª")[0]);
                        
                        var patamar = EnumHelper<Patamar>.GetValueFromName(worksheet.Cells[linha, 7].Value?.ToString());
                        
                        // PERCORRE OS MESES DA PLANILHA
                        for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                        {
                            var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                            var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                            var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                            if (dataInicioMes > DateTime.Today)
                                break;
                            
                            var riscoHidrologico = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                            var repasseRiscoHidrologico =
                                repasseRiscoHidrologicoCadastrados.FirstOrDefault(x =>
                                    x.IdPerfilAgente == perfilAgente.Id &&
                                    x.IdParcelaUsina == parcelaUsina?.Id &&
                                    x.Semana == semana &&
                                    x.Patamar == patamar &&
                                    x.TipoRepasseRiscoHidrologico == tabela &&
                                    x.Mes == dataInicioMes) ??
                                repasseRiscoHidrologicoNovos.FirstOrDefault(x =>
                                    x.IdPerfilAgente == perfilAgente.Id &&
                                    x.IdParcelaUsina == parcelaUsina?.Id &&
                                    x.Semana == semana &&
                                    x.Patamar == patamar &&
                                    x.TipoRepasseRiscoHidrologico == tabela &&
                                    x.Mes == dataInicioMes);

                            if (repasseRiscoHidrologico is null)
                            {
                                repasseRiscoHidrologico = new RepasseRiscoHidrologico(
                                    tabela,
                                    parcelaUsina?.Id,
                                    perfilAgente.Id,
                                    semana,
                                    patamar,
                                    dataInicioMes,
                                    riscoHidrologico
                                );
                    
                                repasseRiscoHidrologicoNovos.Add(repasseRiscoHidrologico);
                            }
                            else
                                repasseRiscoHidrologico.AtualizarRiscoHidrologico(riscoHidrologico);
                            
                        }

                        linha++;
                    }
                    _repasseRiscoHidrologicoRepository.Update(repasseRiscoHidrologicoCadastrados.ToArray());
                    _repasseRiscoHidrologicoRepository.Create(repasseRiscoHidrologicoNovos.ToArray());
                }
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{NomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

    }
}