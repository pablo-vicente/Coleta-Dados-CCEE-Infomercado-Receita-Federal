using System.IO;
using Infomercado.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infomercado.Domain.Models
{
    public class InfoMercadoDbContextFactory : IDesignTimeDbContextFactory<InfoMercadoDbContext>
    {
        public InfoMercadoDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<InfoMercadoDbContext>();
            var connectionString = configuration.GetConnectionString("InfoMercado");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.EnableSensitiveDataLogging();

            return new InfoMercadoDbContext(optionsBuilder.Options);
        }
    }
    
    public class InfoMercadoDbContext : DbContext
    {
        public InfoMercadoDbContext(DbContextOptions<InfoMercadoDbContext> options) : base(options) { }

        public virtual DbSet<InfoMercadoArquivo> InfoMercadoArquivos { get; private set; }
        public virtual DbSet<Agente> Agentes { get; private set; }
        public virtual DbSet<PerfilAgente> PerfilAgentes { get; private set; }
        public virtual DbSet<Contrato> Contratos { get; private set; }
        public virtual DbSet<Usina> Usinas { get; private set; }
        public virtual DbSet<ParcelaUsina> ParcelaUsinas { get; private set; }
        public virtual DbSet<DadosGeracaoUsina> DadosGeracaoUsinas { get; private set; }
        public virtual DbSet<Contabilizacao> Contabilizacaes { get; private set; }
        public virtual DbSet<Encargo> Encargos { get; private set; }
        public virtual DbSet<DadoMre> DadosMres { get; private set; }
        public virtual DbSet<LiquidacaoDistribuidoraCotistaGarantiaFisica> CotistasLiquidacaoDistribuidoraGarantiaFisicas { get; private set; }
        public virtual DbSet<ReceitaVendaDistribuidoraCotistaGarantiaFisica> ReceitaVendaDistribuidoraCotistaGarantiaFisicas { get; private set; }
        public virtual DbSet<ReceitaVendaComercializacaoEnergiaNuclear> ReceitaVendaComercializacaoEnergiaNucleares { get; private set; }
        public virtual DbSet<ReceitaVendaDistribuidoraCotistaEnergiaNuclear> ReceitaVendaDistribuidoraCotistaEnergiaNucleares { get; private set; }
        public virtual DbSet<ProinfaInformacoesConformeResolucao1833Usina> ProinfaInformacoesConformeResolucao1833Usinas { get; private set; }
        public virtual DbSet<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre> ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres { get; private set; }
        public virtual DbSet<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade> MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades { get; private set; }
        public virtual DbSet<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade> GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades { get; private set; }
        public virtual DbSet<ConsumoParcelaCarga> ConsumoParcelaCargas { get; private set; }
        public virtual DbSet<ConsumoPerfilAgente> ConsumoPerfilAgentes { get; private set; }
        public virtual DbSet<ConsumoGeracaoPerfilAgente> ConsumoGeracaoPerfilAgentes { get; private set; }
        public virtual DbSet<RepasseRiscoHidrologico> RepasseRiscoHidrologicos { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoMercadoArquivo>().HasKey(x=>x.Id);
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Ano).IsRequired();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.DataUltimaAtualizacao).IsRequired();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Lido).IsRequired();
            
            modelBuilder.Entity<Agente>().HasKey(x=>x.Id);
            modelBuilder.Entity<Agente>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Agente>().HasAlternateKey(x => x.Codigo);
            modelBuilder.Entity<Agente>().Property(x => x.Sigla).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Agente>().Property(x => x.NomeEmpresarial).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Agente>().Property(x => x.Cnpj).IsRequired().HasMaxLength(18);
            modelBuilder.Entity<Agente>().HasAlternateKey(x => x.Cnpj);
            modelBuilder.Entity<Agente>()
                .HasMany(e => e.PerfisAgente)
                .WithOne(e => e.Agente)
                .HasForeignKey(e => e.IdAgente);
            
            modelBuilder.Entity<PerfilAgente>().HasKey(x => x.Id);
            modelBuilder.Entity<PerfilAgente>().HasAlternateKey(x => x.Codigo);
            modelBuilder.Entity<PerfilAgente>().Property(x => x.Sigla).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.Contratos)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.DadosGeracaoUsinas)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.Contabilizacaes)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.Encargos)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.DadosMres)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ReceitaVendaDistribuidoraCotistaGarantiaFisicas)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ReceitaVendaComercializacaoEnergiaNucleares)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ReceitaVendaDistribuidoraCotistaEnergiaNucleares)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ConsumoParcelaCargas)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ConsumoPerfilAgentes)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.ConsumoGeracaoPerfilAgentes)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            modelBuilder.Entity<PerfilAgente>()
                .HasMany(x => x.RepasseRiscoHidrologicos)
                .WithOne(x => x.PerfilAgente)
                .HasForeignKey(x => x.IdPerfilAgente);
            
            modelBuilder.Entity<Contrato>().HasKey(x => x.Id);
            modelBuilder.Entity<Contrato>().HasAlternateKey(x => new {x.IdPerfilAgente, x.Data, x.Tipo});
            
            modelBuilder.Entity<Usina>().HasKey(x => x.Id);
            modelBuilder.Entity<Usina>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Usina>().Property(x => x.SiglaAtivo).HasMaxLength(50);
            modelBuilder.Entity<Usina>().Property(x => x.Uf).HasMaxLength(2);
            modelBuilder.Entity<Usina>()
                .HasMany(x => x.ParcelasUsina)
                .WithOne(x => x.Usina)
                .HasForeignKey(x => x.IdUsina);
            
            modelBuilder.Entity<ParcelaUsina>().HasKey(x => x.Id);
            modelBuilder.Entity<ParcelaUsina>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ParcelaUsina>().Property(x => x.Sigla).HasMaxLength(50);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.DadosGeracaoUsina)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.Encargos)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.LiquidacaoDistribuidoraCotistaGarantiaFisicas)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.ReceitaVendaDistribuidoraCotistaGarantiaFisicas)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.ProinfaInformacoesConformeResolucao1833Usinas)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.ProinfaMontanteEnergiaAlocadaUsinasParticipantesMres)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidades)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidades)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);
            modelBuilder.Entity<ParcelaUsina>()
                .HasMany(x => x.RepasseRiscoHidrologicos)
                .WithOne(x => x.ParcelaUsina)
                .HasForeignKey(x => x.IdParcelaUsina);

            modelBuilder.Entity<DadosGeracaoUsina>().HasKey(x => x.Id);
            modelBuilder.Entity<DadosGeracaoUsina>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DadosGeracaoUsina>().Property(x => x.Cegempreendimento).HasMaxLength(200);
            modelBuilder.Entity<DadosGeracaoUsina>()
                .HasAlternateKey(x => new {x.IdParcelaUsina, x.IdPerfilAgente, x.Mes});
            
            modelBuilder.Entity<Contabilizacao>().HasKey(x => x.Id);
            modelBuilder.Entity<Contabilizacao>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contabilizacao>().HasAlternateKey(x => new {x.MesAno, x.IdPerfilAgente});
            
            modelBuilder.Entity<Encargo>().HasKey(x => x.Id);
            modelBuilder.Entity<Encargo>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Encargo>().HasAlternateKey(x => new {x.IdParcelaUsina, x.IdPerfilAgente, x.Mes});
            
            modelBuilder.Entity<DadoMre>().HasKey(x => x.Id);
            modelBuilder.Entity<DadoMre>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DadoMre>().HasAlternateKey(x => new {x.IdPerfilAgente, x.Data, x.TipoMre, x.Patamar});
            
            modelBuilder.Entity<LiquidacaoDistribuidoraCotistaGarantiaFisica>().HasKey(x => x.Id);
            modelBuilder.Entity<LiquidacaoDistribuidoraCotistaGarantiaFisica>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LiquidacaoDistribuidoraCotistaGarantiaFisica>().HasAlternateKey(x => new {x.IdParcelaUsina, x.MesAno});
            
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaGarantiaFisica>().HasKey(x => x.Id);
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaGarantiaFisica>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaGarantiaFisica>().HasAlternateKey(x => new {x.MesAno, x.IdPerfilAgente, x.IdParcelaUsina});
            
            modelBuilder.Entity<ReceitaVendaComercializacaoEnergiaNuclear>().HasKey(x => x.Id);
            modelBuilder.Entity<ReceitaVendaComercializacaoEnergiaNuclear>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ReceitaVendaComercializacaoEnergiaNuclear>().HasAlternateKey(x => new {x.MesAno, x.IdPerfilAgente});
            
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaEnergiaNuclear>().HasKey(x => x.Id);
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaEnergiaNuclear>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ReceitaVendaDistribuidoraCotistaEnergiaNuclear>().HasAlternateKey(x => new {x.MesAno, x.IdPerfilAgente});
            
            modelBuilder.Entity<ProinfaInformacoesConformeResolucao1833Usina>().HasKey(x => x.Id);
            modelBuilder.Entity<ProinfaInformacoesConformeResolucao1833Usina>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProinfaInformacoesConformeResolucao1833Usina>().Property(x => x.Ccve).HasMaxLength(10);
            modelBuilder.Entity<ProinfaInformacoesConformeResolucao1833Usina>().HasAlternateKey(x => new {x.IdParcelaUsina, MesAno = x.Data});
            
            modelBuilder.Entity<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre>().HasKey(x => x.Id);
            modelBuilder.Entity<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProinfaMontanteEnergiaAlocadaUsinasParticipantesMre>().HasAlternateKey(x => new {x.IdParcelaUsina, x.IdPerfilAgente, x.Data});

            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().HasKey(x => x.Id);
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().Property(x => x.CegeEmpreendimento).HasMaxLength(200);
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().Property(x => x.Leilao).HasMaxLength(100);
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().Property(x => x.Produto).HasMaxLength(8);
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().Property(x => x.SiglaLeilao).HasMaxLength(50);
            modelBuilder.Entity<MontanteContratadoGarantiaFisicaComprometidaGeracaoDestinadaLeilaoDisponibilidade>().HasAlternateKey(x => new {x.IdParcelaUsina, x.MesAno, x.Leilao, x.Produto, x.CegeEmpreendimento});

            modelBuilder.Entity<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade>().HasKey(x => x.Id);
            modelBuilder.Entity<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade>().Property(x => x.CegeEmpreendimento).HasMaxLength(200);
            modelBuilder.Entity<GeracaoGarantiaFisicaUsinasParticipantesLeiloesDisponibilidade>().HasAlternateKey(x => new {x.IdParcelaUsina, x.MesAno});

            modelBuilder.Entity<ConsumoParcelaCarga>().HasKey(x => x.Id);
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.Carga).HasMaxLength(50);
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.Cidade).HasMaxLength(50);
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.Estado).HasMaxLength(2);
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.RamoAtividade).HasMaxLength(50);
            modelBuilder.Entity<ConsumoParcelaCarga>().Property(x => x.SiglaPerfilDistribuidora).HasMaxLength(50);
            modelBuilder.Entity<ConsumoParcelaCarga>().HasAlternateKey(x => new {x.Mes, x.IdPerfilAgente, x.CodigoCarga});

            modelBuilder.Entity<ConsumoPerfilAgente>().HasKey(x => x.Id);
            modelBuilder.Entity<ConsumoPerfilAgente>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConsumoPerfilAgente>().HasAlternateKey(x => new {x.Mes, x.IdPerfilAgente, x.Patamar});

            modelBuilder.Entity<ConsumoGeracaoPerfilAgente>().HasKey(x => x.Id);
            modelBuilder.Entity<ConsumoGeracaoPerfilAgente>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ConsumoGeracaoPerfilAgente>().HasAlternateKey(x => new {x.Mes, x.IdPerfilAgente});
            
            modelBuilder.Entity<RepasseRiscoHidrologico>().HasKey(x => x.Id);
            modelBuilder.Entity<RepasseRiscoHidrologico>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}