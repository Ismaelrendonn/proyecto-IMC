/*using IMCAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CalculoIMC> CalculosIMC { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalculoIMC>(entity =>
            {
                entity.Property(c => c.FechaCalculo)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(c => c.Peso)
                    .HasColumnType("decimal(5,2)");

                entity.Property(c => c.Altura)
                    .HasColumnType("decimal(5,2)");

                entity.Property(c => c.ResultadoIMC)
                    .HasColumnType("decimal(5,2)");
            });
        }
    }
}*/


using IMCAPI.Models;
using Microsoft.EntityFrameworkCore;

    namespace IMCAPI.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public virtual DbSet<CalculoIMC> CalculosIMC => Set<CalculoIMC>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculoIMC>(entity =>
        {
            // Clave primaria
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).ValueGeneratedOnAdd();

            // Columnas
            entity.Property(c => c.Peso)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            entity.Property(c => c.Altura)
                .HasColumnType("decimal(3,2)") // 2.50 m (máx)
                .IsRequired();

            entity.Property(c => c.ResultadoIMC)
                .HasColumnType("decimal(4,1)") // 99.9 (suficiente para IMC)
                .IsRequired();

            entity.Property(c => c.Categoria)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.FechaCalculo)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();
        });
    }
}