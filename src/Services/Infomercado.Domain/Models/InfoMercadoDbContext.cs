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

        public virtual DbSet<InfoMercadoArquivo> InfoMercadoArquivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InfoMercadoArquivo>().HasKey(x=>x.Id);
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Ano).IsRequired();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.DataUltimaAtualizacao).IsRequired();
            modelBuilder.Entity<InfoMercadoArquivo>().Property(x => x.Lido).IsRequired();
        }
    }
}