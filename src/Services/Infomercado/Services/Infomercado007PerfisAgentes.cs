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
    public class Infomercado007PerfisAgentes
    {
        private const string PrimeiraColuna = "Cód. Agente";

        private readonly ILogger _logger;
        private readonly AgenteRepository _agenteRepository;

        public Infomercado007PerfisAgentes(ILogger<Infomercado007PerfisAgentes> logger,
            AgenteRepository agenteRepository)
        {
            _logger = logger;
            _agenteRepository = agenteRepository;
        }

        public void ImportarPlanilha(ExcelPackage excelPackage)
        {
            const string nomePlanilha = "007 Lista de Perfis";
            _logger.LogInformation($"Importando {nomePlanilha}...");
            var worksheet = excelPackage.Workbook.Worksheets[nomePlanilha];

            var agentesCadastrados = _agenteRepository.ReadAll().ToList();
            var agentesNovos = new List<Agente>();

            var linha = InfomercadoHelper.ObterPrimeiraLinhaDados(worksheet, PrimeiraColuna);

            try
            {
                while (int.TryParse(worksheet.Cells[linha, 2].Value?.ToString(), out var codigoAgente))
                {
                    _logger.LogInformation($"Importando linha: {linha} - {nomePlanilha}");
                    var sigla = worksheet.Cells[linha, 3].Value.ToString();
                    var nomeEmpresarial = worksheet.Cells[linha, 4].Value.ToString();
                    var cnpj = InfomercadoHelper.FormatarCnpj(worksheet.Cells[linha, 5].Value.ToString());
                    var categoria = EnumHelper<Categoria>.GetValueFromName(worksheet.Cells[linha, 10].Value.ToString());

                    var agente = agentesCadastrados.FirstOrDefault(x => x.Codigo == codigoAgente) ??
                                 agentesNovos.FirstOrDefault(x => x.Codigo == codigoAgente);

                    if (agente is null)
                    {
                        agente = new Agente(codigoAgente, sigla, nomeEmpresarial, cnpj, categoria);
                        agentesNovos.Add(agente);
                    }
                    else
                    {
                        agente.AtualizarSigla(sigla);
                        agente.AtualizarNomeEmpresarial(nomeEmpresarial);
                        agente.AtualizarCnpj(cnpj);
                        agente.AtualizarCategoria(categoria);
                    }

                    var codigoPerfil = int.Parse(worksheet.Cells[linha, 6].Value.ToString());
                    var siglaPerfil = worksheet.Cells[linha, 7].Value.ToString();
                    var classePerfil = EnumHelper<Classe>.GetValueFromName(worksheet.Cells[linha, 8].Value.ToString());
                    var status = EnumHelper<Status>.GetValueFromName(worksheet.Cells[linha, 9].Value.ToString());
                    var submercado = EnumHelper<Submercado>.GetValueFromName(worksheet.Cells[linha, 11].Value.ToString());
                    var varejista = worksheet.Cells[linha, 12].Value.ToString().Equals("Sim", StringComparison.Ordinal);
                    
                    var perfilAgente = agente.PerfisAgente.FirstOrDefault(x => x.Codigo == codigoPerfil);

                    if (perfilAgente is null)
                    {
                        perfilAgente = new PerfilAgente(codigoPerfil, siglaPerfil, classePerfil, status, submercado, varejista);
                        agente.PerfisAgente.Add(perfilAgente);
                    }
                    else
                    {
                        perfilAgente.AtualizarSigla(siglaPerfil);
                        perfilAgente.AtualizarClasse(classePerfil);
                        perfilAgente.AtualizarStatus(status);
                        perfilAgente.AtualizarSubmercado(submercado);
                        perfilAgente.AtualizarVarejista(varejista);
                    }

                    linha++;
                    if (linha == 15352)
                    {
                        Console.WriteLine();
                    }
                }
                
                _agenteRepository.Update(agentesCadastrados.ToArray());
                _agenteRepository.Create(agentesNovos.ToArray());
            }
            catch (Exception ex)
            {
                var message = $"Ocorreu um erro '{ex.Message}' ao importar '{nomePlanilha}'";
                throw new ApplicationException(message, ex);
            }
        }
    }
}