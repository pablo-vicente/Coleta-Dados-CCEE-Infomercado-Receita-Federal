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
        }
    }
}