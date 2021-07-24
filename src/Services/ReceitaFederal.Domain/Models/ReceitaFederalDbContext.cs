using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ReceitaFederal.Domain.Models
{
    public class InfoMercadoDbContextFactory : IDesignTimeDbContextFactory<ReceitaFederalDbContext>
    {
        public ReceitaFederalDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            var optionsBuilder = new DbContextOptionsBuilder<ReceitaFederalDbContext>();
            var connectionString = configuration.GetConnectionString("ReceitaFederal");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.EnableSensitiveDataLogging();

            return new ReceitaFederalDbContext(optionsBuilder.Options);
        }
    }
    
    public class ReceitaFederalDbContext : DbContext
    {
        public ReceitaFederalDbContext(DbContextOptions<ReceitaFederalDbContext> options) : base(options) { }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ReceitaFederalArquivo>().HasKey(x => x.Id);
            modelBuilder.Entity<ReceitaFederalArquivo>().Property(x => x.Nome).HasMaxLength(200);
            
            modelBuilder.Entity<Empresa>().HasKey(x => x.Id);
            modelBuilder.Entity<Empresa>().HasAlternateKey(x => x.Cnpj);
            modelBuilder.Entity<Empresa>().Property(x => x.Cnpj).HasMaxLength(18);
            modelBuilder.Entity<Empresa>().Property(x => x.NomeFantasia).HasMaxLength(200);
            modelBuilder.Entity<Empresa>().Property(x => x.RazaoSocial).HasMaxLength(200);
            
            modelBuilder.Entity<Socio>().HasKey(x => x.Id);
            modelBuilder.Entity<Socio>().HasAlternateKey(x => x.Numero);
            modelBuilder.Entity<Socio>().Property(x => x.Numero).HasMaxLength(18);
            modelBuilder.Entity<Socio>().Property(x => x.Nome).HasMaxLength(200);
            
            modelBuilder.Entity<MotivoSituacaoCadastral>().HasKey(x => x.Id);
            modelBuilder.Entity<MotivoSituacaoCadastral>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<MotivoSituacaoCadastral>().Property(x => x.DescricaoMotivo).HasMaxLength(200);

        }
        
        public DbSet<ReceitaFederalArquivo> ReceitaFederalArquivos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<MotivoSituacaoCadastral> MotivosSituacaoCadastral { get; set; }
    }
}