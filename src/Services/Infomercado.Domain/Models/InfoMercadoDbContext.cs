using Microsoft.EntityFrameworkCore;

namespace Infomercado.Domain.Models
{
    public class InfoMercadoDbContext : DbContext
    {
        public InfoMercadoDbContext(DbContextOptions<InfoMercadoDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
                optionsBuilder.UseLazyLoadingProxies(false);
                optionsBuilder.EnableSensitiveDataLogging();
            }

            base.OnConfiguring(optionsBuilder);
        }
        
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