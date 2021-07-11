using System.IO;
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
            
            modelBuilder.Entity<DadosGeracaoUsina>().HasKey(x => x.Id);
            modelBuilder.Entity<DadosGeracaoUsina>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DadosGeracaoUsina>().Property(x => x.Cegempreendimento).HasMaxLength(200);
            modelBuilder.Entity<DadosGeracaoUsina>()
                .HasAlternateKey(x => new {x.IdParcelaUsina, x.IdPerfilAgente, x.Mes});


        }
    }
}