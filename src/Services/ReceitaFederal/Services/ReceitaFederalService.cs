using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Infomercado.Domain.Models;
using Infomercado.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReceitaFederal.Domain.Enums;
using ReceitaFederal.Domain.Models;
using ReceitaFederal.Domain.Repositories;
using ReceitaFederal.Helpers;

namespace ReceitaFederal.Services
{
    public class ReceitaFederalService
    {
        private const string DetalheCabelhalho = "0";
        private const string DetalheEmpresa = "1";
        private const string DetalheSocio = "2";
        private const string DetalheCnae = "6";
        private const string DetalheTotalizador = "9";
        
        private readonly string _downloads;
        private readonly ILogger _logger;
        private readonly ReceitaFederalArquivoRepository _receitaFederalArquivoRepository;
        private readonly MotivoSituacaoCadastralRepository _situacaoCadastralRepository;
        private readonly SituacaoCadastralService _situacaoCadastralService;
        
        private readonly AgenteRepository _agenteRepository;
        private readonly EmpresaRepository _empresaRepository;
        private readonly SocioRepository _socioRepository;
        
        public ReceitaFederalService(ILogger<ReceitaFederalService> logger, 
            IConfiguration configuration, 
            ReceitaFederalArquivoRepository receitaFederalArquivoRepository, 
            SituacaoCadastralService situacaoCadastralService, 
            EmpresaRepository empresaRepository, 
            SocioRepository socioRepository, 
            MotivoSituacaoCadastralRepository situacaoCadastralRepository, 
            AgenteRepository agenteRepository)
        {
            _logger = logger;
            _receitaFederalArquivoRepository = receitaFederalArquivoRepository;
            _situacaoCadastralService = situacaoCadastralService;
            _empresaRepository = empresaRepository;
            _socioRepository = socioRepository;
            _situacaoCadastralRepository = situacaoCadastralRepository;
            _agenteRepository = agenteRepository;
            _downloads = configuration["ArquivosReceita"];
        }
        
        private void IncluirArquivosSalvosRepositorioBanco()
        {
            _logger.LogInformation("Salvando arquivos no banco de dados...");
            
            var arquivos = new DirectoryInfo(_downloads)
                .GetDirectories()
                .OrderBy(x=>x.Name)
                .ToList();
            
            var receitaFederalArquivos = _receitaFederalArquivoRepository.ReadAll().ToList();
            var novosArquivos = new List<ReceitaFederalArquivo>();

            foreach (var arquivo in arquivos)
            {
                var arquivoReceita = receitaFederalArquivos
                    .FirstOrDefault(x => x.Nome.Equals(arquivo.Name, StringComparison.InvariantCultureIgnoreCase));

                if (arquivoReceita is not null)
                    continue;

                var atualizacao = DateTime.ParseExact(arquivo.Name, "yyyy-MM-dd" ,CultureInfo.CurrentCulture);
                novosArquivos.Add(new ReceitaFederalArquivo(arquivo.Name, atualizacao));
            }

            _receitaFederalArquivoRepository.Create(novosArquivos.ToArray());
        }

        public void ImportarArquivosReceitaFederal()
        {
            IncluirArquivosSalvosRepositorioBanco();

            var receitaFederalArquivos = _receitaFederalArquivoRepository.ListarArquivosNaoImportados().ToList();
            
            foreach (var receitaFederalArquivo in receitaFederalArquivos)
            {
                _situacaoCadastralService.ImportarArquivo(_downloads, receitaFederalArquivo.Atualizacao);

                ImportarDadosJuridicos(receitaFederalArquivo);
            }
        }

        private void ImportarDadosJuridicos(ReceitaFederalArquivo receitaFederalArquivo)
        {
            var cnpjsComBarra = ListarCnpj(receitaFederalArquivo.Atualizacao).ToList();
            var empresasExistentes = _empresaRepository.ReadAllByCnpj(cnpjsComBarra).ToList();
            
            var arquivos = new DirectoryInfo(Path.Combine(_downloads, receitaFederalArquivo.Nome))
                .GetFiles("*.zip")
                .OrderBy(x => x.Name)
                .ToList();

            foreach (var arquivo in arquivos)
            {
                var arquivoExtraido = ExtrairArquivoReceitaFederal(arquivo);

                var primeiroCnpj = ObterPrimeiroCnpj(arquivoExtraido);
                var ultimoCnpj = ObterUltimoCnpj(arquivoExtraido);

                var cnpjsNoArquivo = cnpjsComBarra.Select(RetirarFormatacaoCnpj)
                    .Where(cnpj => cnpj >= primeiroCnpj && cnpj <= ultimoCnpj)
                    .ToList();

                using (var fileStream = new FileStream(arquivoExtraido.FullName, FileMode.Open, FileAccess.Read))
                {
                    _logger.LogInformation($"Lendo CNPJs: {primeiroCnpj:00\\.000\\.000\\/0000\\-00} | ATÉ: {ultimoCnpj:00\\.000\\.000\\/0000\\-00}");

                    var qntCnpj = 0;
                    foreach (var cnpj in cnpjsNoArquivo)
                    {
                        qntCnpj++;
                        // if (qntCnpj == 166)
                        // {
                        //     Console.WriteLine();
                        // }
                        
                        _logger.LogInformation($"{qntCnpj}/{cnpjsNoArquivo.Count} CNPJs");
                        var cnpjBarras = $"{cnpj:00\\.000\\.000\\/0000\\-00}";
                        var posicaoCnpjArquivo = BuscarPosicaoCnpjArquivo(fileStream, cnpj);
                        if (posicaoCnpjArquivo == -1)
                        {
                            var aviso = $"CNPJ: {cnpjBarras} não encontrado no arquivo \"{arquivo.Name}\".";
                            _logger.LogWarning(aviso);
                            continue;
                        }

                        var empresa = ImportarDadosCnpj(fileStream, posicaoCnpjArquivo, cnpjBarras, empresasExistentes);
                        empresa.AtualizarAtualizacao(receitaFederalArquivo.Atualizacao);
                        _empresaRepository.Update(empresa);
                    }
                }

                _logger.LogInformation($"Arquivo \"{arquivo.Name}\" importado com sucesso.");

                // Apagar Arquivo
                arquivoExtraido.Delete();
            }
        }

        private void ImportarDadosEmpresa(string linha, Empresa empresa)
        {
            var campos = new StringHelper(linha);
            campos.Ler(1); //TIPO DE REGISTRO
            campos.Ler(1); //INDICADOR-FULLDIARIO
            campos.Ler(1); //TIPO-ATUALIZACAO 
            long.TryParse(campos.Ler(14), out var cnpj); //CNPJ
            campos.Ler(1); //1 – MATRIZ 2 – FILIAL
            var razaoSocial = campos.Ler(150).Trim();
            var nomeFantasia = campos.Ler(55).Trim();
            Enum.TryParse<SituacaoCadastral>(campos.Ler(2).Trim(), out var situacaoCadastral);
            campos.Ler(8);
            int.TryParse(campos.Ler(2).Trim(), out var motivoSituacaoCadastralId);
            campos.Ler(55);
            campos.Ler(3);
            campos.Ler(70);
            campos.Ler(4);
            DateTime.TryParseExact(campos.Ler(8).Trim(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var inicioAtividade);
            campos.Ler(7);
            campos.Ler(20);
            campos.Ler(60);
            campos.Ler(6);
            campos.Ler(156);
            campos.Ler(50);
            campos.Ler(8);
            campos.Ler(2);
            campos.Ler(4);
            campos.Ler(50);
            campos.Ler(12);
            campos.Ler(12);
            campos.Ler(12);
            campos.Ler(115);
            campos.Ler(2);
            decimal.TryParse(campos.Ler(14), out var capitalSocial); //CAPITAL SOCIAL DA EMPRESA

            var cnpjCompleto = cnpj;
            var cnpjEmpresa = RetirarFormatacaoCnpj(empresa.Cnpj);
            if (cnpjCompleto != cnpjEmpresa)
                throw new ApplicationException("Cnpj Diferente da Empresa");

            var motivoSituacaoCadastral = _situacaoCadastralRepository.Read(motivoSituacaoCadastralId);

            empresa.AtualizarRazaoSocial(razaoSocial);
            empresa.AtualizarNomeFantasia(nomeFantasia);
            empresa.AtualizarInicioAtividade(inicioAtividade);
            empresa.AtualizarSituacaoCadastral(situacaoCadastral);
            empresa.AtualizarCapitalSocial(capitalSocial);
            if (motivoSituacaoCadastral != null)
                empresa.AtualizarMotivoSituacaoCadastral(motivoSituacaoCadastral.Id);

            _empresaRepository.Update(empresa);
        }
        
        private Empresa ImportarDadosCnpj(FileStream fileStream, long offsefFim, string cnpjBarras, IEnumerable<Empresa> empresasExistentes)
        {
            var offsetDados = offsefFim;
            var linha = LerLinhaOffset(fileStream, offsetDados);
            var lerDadosEmpresa = true;
            
            var camposSocios = new StringHelper(linha);
            camposSocios.Ler(1); //TIPO DE REGISTRO
            camposSocios.Ler(1); //INDICADOR-FULLDIARIO
            
            var empresa = empresasExistentes.FirstOrDefault(x=>x.Cnpj.Equals(cnpjBarras));
            if (empresa is null)
            {
                empresa = new Empresa(cnpjBarras, "-", "-", SituacaoCadastral.Ativa, null, null, 0, DateTime.MinValue);
                _empresaRepository.Create(empresa);
            }
            else
                _socioRepository.ExcluirSocios(empresa.Id);
            
            var socios = new List<Socio>();
            
            #region Importacao Socio Parte de Cima do Arquivo Antes de Encontrar a linha que contem os dados da empresa

            while (!linha.StartsWith(DetalheEmpresa))
            {
                socios.Add(ImportarDadosSocio(linha, empresa));
                linha = LerLinhaOffset(fileStream, offsetDados -= 1201);

                if (offsetDados >= 0) continue;
                lerDadosEmpresa = false;
                break;
            }

            #endregion
            
            #region Importacao dos Dados da Empresa

            if (lerDadosEmpresa)
                ImportarDadosEmpresa(linha, empresa);

            #endregion

            #region Importacao Socio Parte de BAIXO do Arquivo DEPOIS de Encontrar a linha que contem os dados da empresa

            offsetDados = offsefFim + 1201;
            linha = LerLinhaOffset(fileStream, offsetDados);
            while (linha.StartsWith(DetalheSocio))
            {
                socios.Add(ImportarDadosSocio(linha, empresa));
                linha = LerLinhaOffset(fileStream, offsetDados += 1201);
                if (offsetDados > fileStream.Length) break;
            }

            #endregion
            
            _socioRepository.Create(socios.ToArray());
            return empresa;
        }

        private Socio ImportarDadosSocio(string linha, Empresa empresa)
        {
            var campos = new StringHelper(linha);
            campos.Ler(1); //TIPO DE REGISTRO
            campos.Ler(1); //INDICADOR-FULLDIARIO
            campos.Ler(1); //TIPO-ATUALIZACAO 
            long.TryParse(campos.Ler(14), out var cnpj); //CNPJ
            Enum.TryParse<TipoSocio>(campos.Ler(1).Trim(), out var tipoSocio);
            var nome = campos.Ler(150).Trim();
            var numero = campos.Ler(14).Trim();

            var cnpjEmpresa = RetirarFormatacaoCnpj(empresa.Cnpj);
            if (cnpjEmpresa != cnpj)
                throw new ApplicationException("SOCIO Cadastrado na Empresa Errada");
            var socio = new Socio(nome, numero, tipoSocio, empresa.Id);
            return socio;
        }
        
        private long BuscarPosicaoCnpjArquivo(FileStream fileStream, long cnpj)
        {
            long offsetInicio = 0;
            var offsefFim = fileStream.Length / 2 - fileStream.Length / 2 % 1201;
            string linha;

            // PARA CASO O CNPJ SEJA INVÁLIDO
            var contadorCnpjInvalido = 0;
            while (contadorCnpjInvalido < 10)
            {
                linha = LerLinhaOffset(fileStream, offsefFim);

                if (linha.StartsWith(DetalheCabelhalho) || linha.StartsWith(DetalheTotalizador) || linha.StartsWith(DetalheCnae))
                    offsefFim -= 1201;
                else
                {
                    var campos = new StringHelper(linha);
                    campos.Ler(1); //TIPO DE REGISTRO
                    campos.Ler(1); //INDICADOR-FULLDIARIO
                    campos.Ler(1); //TIPO-ATUALIZACAO

                    var cnpjArquivo = long.Parse(campos.Ler(14)); //CNPJ

                    if (cnpjArquivo > cnpj)
                    {
                        offsefFim = (offsefFim - offsetInicio) / 2 + offsetInicio;
                        offsefFim -= offsefFim % 1201;
                    }
                    else if (cnpjArquivo < cnpj)
                    {
                        var temp = offsetInicio;
                        offsetInicio = offsefFim;

                        offsefFim += (offsefFim - temp) / 2;
                        offsefFim += 1201 - offsefFim % 1201;
                    }
                    else
                        return offsefFim;
                }

                if (offsetInicio != offsefFim) continue;
                // O CNPJ PODE SER ESTAR NAS LINHAS PRÓXIMAS
                offsefFim += 1201 * 2;
                contadorCnpjInvalido++;
            }

            return -1;
        }
        
        private static long ObterPrimeiroCnpj(FileInfo arquivoExtraido)
        {
            var primeiraLinha = File.ReadLines(arquivoExtraido.FullName)
                .SkipWhile(x => x.StartsWith(DetalheCabelhalho) || x.StartsWith(DetalheTotalizador))
                .FirstOrDefault();

            var campos = new StringHelper(primeiraLinha);
            campos.Ler(1); //TIPO DE REGISTRO
            campos.Ler(1); //INDICADOR-FULLDIARIO
            campos.Ler(1); //TIPO-ATUALIZACAO
            long.TryParse(campos.Ler(14), out var primeiroCnpj); //CNPJ
            return primeiroCnpj;
        }

        private long ObterUltimoCnpj(FileInfo arquivoExtraido)
        {
            var ultimaLinha = string.Empty;
            using (var fs = new FileStream(arquivoExtraido.FullName, FileMode.Open, FileAccess.Read))
            {
                var offset = fs.Length;

                while (ultimaLinha == string.Empty || ultimaLinha.StartsWith(DetalheCabelhalho) || ultimaLinha.StartsWith(DetalheTotalizador))
                {
                    ultimaLinha = LerLinhaOffset(fs, offset);
                    offset -= 1201;
                }
            }

            var campos = new StringHelper(ultimaLinha);
            campos.Ler(1); //TIPO DE REGISTRO
            campos.Ler(1); //INDICADOR-FULLDIARIO
            campos.Ler(1); //TIPO-ATUALIZACAO
            long.TryParse(campos.Ler(14), out var ultimoCnpj); //CNPJ
            return ultimoCnpj;
        }
        
        private string LerLinhaOffset(FileStream fs, long offsefFim)
        {
            var linha = string.Empty;
            fs.Seek(offsefFim, SeekOrigin.Begin);
            for (var bit = offsefFim - 1201; bit < offsefFim; bit++)
            {
                fs.Seek(bit, SeekOrigin.Begin);
                linha += Convert.ToChar(fs.ReadByte());
            }

            return linha;
        }
        
        private FileInfo ExtrairArquivoReceitaFederal(FileInfo arquivoReceitaZip)
        {
            using var zipArchive = ZipFile.Open(arquivoReceitaZip.FullName, ZipArchiveMode.Read);
            var nome = zipArchive.Entries[0].Name;
            var arquivoExtraido = new FileInfo(Path.Combine(arquivoReceitaZip.Directory.ToString(), nome));
            if (!arquivoExtraido.Exists || arquivoExtraido.Length != zipArchive.Entries[0].Length)
            {
                _logger.LogInformation($"Extraindo arquivo \"{arquivoReceitaZip.Name}\"...");
                zipArchive.ExtractToDirectory(arquivoReceitaZip.DirectoryName);
                _logger.LogInformation($"Arquivo \"{arquivoReceitaZip.Name}\" extraido com sucesso em \"{arquivoReceitaZip.DirectoryName}\"");
            }

            arquivoExtraido.Refresh();
            return arquivoExtraido;
        }
        
        private IEnumerable<string> ListarCnpj(DateTime atualizacao)
        {
            _logger.LogInformation("Obtendo lista de CNPJs...");
            var cnpjAgentes = _agenteRepository.ListarCnpjs().ToList();
            var cnpjsReceita = _empresaRepository.ListarCnpjs().ToList();
            var cnpjsComBarras = cnpjAgentes.Concat(cnpjsReceita).ToHashSet();
            
            _logger.LogInformation("Filtrando CNPJs desatualizados...");
            var cnpjsAtualizados = _empresaRepository.ListarCnpjsAtualizados(atualizacao);
            
            cnpjsComBarras.ExceptWith(cnpjsAtualizados);
            
            // var cnpjs = cnpjsComBarras.Select(RetirarFormatacaoCnpj).ToList();
            // cnpjs = cnpjs.Where(cnpj => cnpjsAtualizados.All(cnpjAtt => cnpj != cnpjAtt)).ToList();
            return cnpjsComBarras;
        }

        private long RetirarFormatacaoCnpj(string cnpj)
        {
            var cnpjNumerico = cnpj
                .Trim()
                .Replace("/", string.Empty)
                .Replace(".", string.Empty)
                .Replace("-", string.Empty);
            
            if (string.IsNullOrEmpty(cnpjNumerico) || cnpjNumerico.Length > 14 || !long.TryParse(cnpjNumerico, out var cnpjLong))
                throw new Exception("Não possível dar Parse para Long, foi encontrado um caractere inválido no CNPJ.");
            
            return cnpjLong;
        }
    }
}