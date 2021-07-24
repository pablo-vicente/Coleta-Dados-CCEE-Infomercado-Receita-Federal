using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReceitaFederal.Domain.Models;
using ReceitaFederal.Domain.Repositories;

namespace ReceitaFederal.Services
{
    public class SituacaoCadastralService
    {
        private const string TabelaSituacaoCadastral = "Tabela de Motivo Situação Cadastral";
        private const string TabelaQualificacaoSocio = "Tabela de Qualificação do Sócio-Representante";
        private const string TabelaLayout = "Tabela de Layout";
        
        private readonly ILogger _logger;
        private readonly MotivoSituacaoCadastralRepository _motivoSituacaoCadastralRepository;
        
        public SituacaoCadastralService(ILogger<SituacaoCadastralService> logger, MotivoSituacaoCadastralRepository motivoSituacaoCadastralRepository)
        {
            _logger = logger;
            _motivoSituacaoCadastralRepository = motivoSituacaoCadastralRepository;
        }

        public void ImportarArquivo(string caminhoDownload, DateTime atualizacao)
        {
            _logger.LogInformation($"Importando \"{TabelaSituacaoCadastral}\"...");

            var pathArquivo = Path.Combine(caminhoDownload, atualizacao.ToString("yyyy-MM-dd"), $"{TabelaSituacaoCadastral}.csv");
            
            #region Obtenção arquivo MotivoSituacao Cadastral

            var motivos = _motivoSituacaoCadastralRepository.ReadAll().ToList();
            var novosMotivos = new List<MotivoSituacaoCadastral>();

            using var streamReader = new StreamReader(pathArquivo, Encoding.GetEncoding(1252)); //WIN_1252_CP Excel

            streamReader.ReadLine(); // Cabeçalho
            while (!streamReader.EndOfStream)
            {
                var linha = streamReader.ReadLine()?.Split(";");
                var codigo = int.Parse(linha?[0]!);
                var descricao = linha?[1];

                var motivo = motivos.FirstOrDefault(m => m.Id == codigo);
                if (motivo is null)
                    novosMotivos.Add(new MotivoSituacaoCadastral(codigo, descricao));
                else
                    motivo.AtribuirDescricaoMotivo(descricao);
            }

            _motivoSituacaoCadastralRepository.Update(motivos.ToArray());

            if (novosMotivos.Any())
                _motivoSituacaoCadastralRepository.Create(novosMotivos.ToArray());

            _logger.LogInformation($"Arquivo \"{pathArquivo}\" importado com sucesso.");

            #endregion
        }
    }
}