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
    public class Infomercado009Proinfa
    {
        private const string NomePlanilha = "009 Proinfa";
        
        private readonly ILogger _logger;
        private readonly PerfilAgenteRepository _perfilAgenteRepository;
        private readonly ParcelaUsinaRepository _parcelaUsinaRepository;
        private readonly ProinfaInformacoesConformeResolucao1833UsinaRepository _proinfaInformacoesConformeResolucao1833UsinaRepository;
        private readonly ProinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository _proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository;
        
        public Infomercado009Proinfa(ILogger<Infomercado008Cotista> logger, 
            PerfilAgenteRepository perfilAgenteRepository, 
            ParcelaUsinaRepository parcelaUsinaRepository, 
            ProinfaInformacoesConformeResolucao1833UsinaRepository proinfaInformacoesConformeResolucao1833UsinaRepository,
            ProinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository)
        {
            _logger = logger;
            _perfilAgenteRepository = perfilAgenteRepository;
            _parcelaUsinaRepository = parcelaUsinaRepository;
            _proinfaInformacoesConformeResolucao1833UsinaRepository = proinfaInformacoesConformeResolucao1833UsinaRepository;
            _proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository = proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage, int ano)
        {
            _logger.LogInformation($"Importando {NomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[NomePlanilha];
            
            var perfisCadatrados = _perfilAgenteRepository.ReadAll().ToList();
            var parcelaUsinasCadastrados = _parcelaUsinaRepository.ReadAll().ToList();
            
            try
            {
                const string primeiraColunaTabela1 = "Código do Ativo";
                var primeiraLinhaTabela1 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela1) + 1;
                ImportarProinfaInformacoesConformeResolucao1833Usina(ano, worksheet, primeiraLinhaTabela1, parcelaUsinasCadastrados);
                    
                const string primeiraColunaTabela2 = "Cód. Perfil Agente";
                var primeiraLinhaTabela2 = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, primeiraColunaTabela2, primeiraLinhaTabela1) + 1;
                ImportarProinfaMontanteEnergiaAlocadaUsinasParticipantesMre(ano, worksheet, primeiraLinhaTabela2, parcelaUsinasCadastrados, perfisCadatrados);

            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{NomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }

        private void ImportarProinfaInformacoesConformeResolucao1833Usina(int ano, ExcelWorksheet worksheet, int primeiraLinha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados)
        {
            var proinfaInformacoesConformeResolucao1833UsinaCadastrados = _proinfaInformacoesConformeResolucao1833UsinaRepository.ReadByYear(ano).ToList();
            var proinfaInformacoesConformeResolucao1833UsinaNovos = new List<ProinfaInformacoesConformeResolucao1833Usina>();

            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
            _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");
            while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoAtivo))
            {
                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 4].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);
                    
                if (parcelaUsina is null)
                    throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado");
                
                var ccve = worksheet.Cells[linha, 6].Value.ToString();
                var fontePrimaria = EnumHelper<FonteEnergiaPrimaria>.GetValueFromName(worksheet.Cells[linha, 7].Value.ToString());

                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var geracao = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var proinfaInformacoesConformeResolucao1833Usina =
                        proinfaInformacoesConformeResolucao1833UsinaCadastrados.FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.Data == dataInicioMes) ??
                        proinfaInformacoesConformeResolucao1833UsinaNovos.FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.Data == dataInicioMes);

                    if (proinfaInformacoesConformeResolucao1833Usina is null)
                    {
                        proinfaInformacoesConformeResolucao1833Usina = new ProinfaInformacoesConformeResolucao1833Usina(
                            ccve,
                            fontePrimaria,
                            dataInicioMes,
                            geracao,
                            parcelaUsina.Id
                        );
                    
                        proinfaInformacoesConformeResolucao1833UsinaNovos.Add(proinfaInformacoesConformeResolucao1833Usina);
                    }
                    else
                    {
                        proinfaInformacoesConformeResolucao1833Usina.AtualizarCcve(ccve);
                        proinfaInformacoesConformeResolucao1833Usina.AtualizarFonteEnergiaPrimaria(fontePrimaria);
                        proinfaInformacoesConformeResolucao1833Usina.AtualizarGeracaoCentroGravidadeMWm(geracao);
                    }
                }
                
                linha++;
            }
            
            _proinfaInformacoesConformeResolucao1833UsinaRepository.Update(proinfaInformacoesConformeResolucao1833UsinaCadastrados.ToArray());
            _proinfaInformacoesConformeResolucao1833UsinaRepository.Create(proinfaInformacoesConformeResolucao1833UsinaNovos.ToArray());
        }
        
        private void ImportarProinfaMontanteEnergiaAlocadaUsinasParticipantesMre(int ano, ExcelWorksheet worksheet, int primeiraLinha, IEnumerable<ParcelaUsina> parcelasUsinaCadatrados, IEnumerable<PerfilAgente> perfisCadatrados)
        {
            var proinfaMontanteEnergiaAlocadaUsinasParticipantesMreCadastrados = _proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository.ReadByYear(ano).ToList();
            var proinfaMontanteEnergiaAlocadaUsinasParticipantesMreNovos = new List<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre>();
            
            var primeiraColunaMes = InfomercadoHelper.ObterPrimeiraColunaMes(primeiraLinha, worksheet);
            var linha = primeiraLinha;
            
            _logger.LogInformation($"Importando linha: {linha} - {NomePlanilha}");
            while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoPerfilAgente))
            {
                var perfilAgente = perfisCadatrados.FirstOrDefault(x => x.Codigo == codigoPerfilAgente);
                if (perfilAgente is null)
                    throw new ApplicationException($"Pefil de agente {codigoPerfilAgente} não encontrato linha {linha} ignorada");
                
                var codigoParcelaUsina = int.Parse(worksheet.Cells[linha, 4].Value.ToString());
                var parcelaUsina = parcelasUsinaCadatrados.FirstOrDefault(x => x.Codigo == codigoParcelaUsina);
                    
                if (parcelaUsina is null)
                    throw new ApplicationException($"Parcela usina {codigoParcelaUsina} não encontrado");
                
                var participanteMre = worksheet.Cells[linha, 6].Value.ToString().Equals("Sim");
                var fontePrimaria = EnumHelper<FonteEnergiaPrimaria>.GetValueFromName(worksheet.Cells[linha, 7].Value.ToString());

                // PERCORRE OS MESES DA PLANILHA
                for (int col = primeiraColunaMes; col <= primeiraColunaMes + 11; col++)
                {
                    var dataColuna = worksheet.Cells[primeiraLinha - 1, col].Value?.ToString();
                    var dataConvertida = Convert.ToDateTime(dataColuna, CultureInfo.CurrentCulture);
                    var dataInicioMes = new DateTime(dataConvertida.Year, dataConvertida.Month, 1);
                    
                    if (dataInicioMes > DateTime.Today)
                        break;
                    
                    var fluxoEnergiaMre = InfomercadoHelper.ConverteDouble(worksheet.Cells[linha, col].Value?.ToString());

                    var proinfaMontanteEnergiaAlocadaUsinasParticipantesMre =
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMreCadastrados.FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.Data == dataInicioMes) ??
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMreNovos.FirstOrDefault(x =>
                            x.IdParcelaUsina == parcelaUsina.Id && x.IdPerfilAgente == perfilAgente.Id && x.Data == dataInicioMes);

                    if (proinfaMontanteEnergiaAlocadaUsinasParticipantesMre is null)
                    {
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMre = new ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre(
                           participanteMre,
                           fontePrimaria,
                           dataInicioMes,
                           fluxoEnergiaMre,
                           perfilAgente.Id,
                           parcelaUsina.Id
                        );
                    
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMreNovos.Add(proinfaMontanteEnergiaAlocadaUsinasParticipantesMre);
                    }
                    else
                    {
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMre.AtualizarParticipanteMreUsina(participanteMre);
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMre.AtualizarFonteEnergiaPrimaria(fontePrimaria);
                        proinfaMontanteEnergiaAlocadaUsinasParticipantesMre.AtualizarFluxoEnergiaMreMWm(fluxoEnergiaMre);
                    }
                }
                
                linha++;
            }
            
            _proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository.Update(proinfaMontanteEnergiaAlocadaUsinasParticipantesMreCadastrados.ToArray());
            _proinfaMontanteEnergiaAlocadaUsinasParticipantesMreRepository.Create(proinfaMontanteEnergiaAlocadaUsinasParticipantesMreNovos.ToArray());
            
        }
        
    }
}