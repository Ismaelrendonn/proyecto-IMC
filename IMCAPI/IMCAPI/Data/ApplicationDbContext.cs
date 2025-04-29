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

            entity.Property(c => c.AlturaCm) // Usar AlturaCm
                .HasColumnType("decimal(5,2)") // Ajustar el tipo de dato
                .IsRequired();

            entity.Property(c => c.ResultadoIMC)
                .HasColumnType("decimal(4,1)")
                .IsRequired();

            entity.Property(c => c.Categoria)
                .HasMaxLength(50);

            entity.Property(c => c.FechaCalculo)
                .HasDefaultValueSql("GETDATE()");
        });
    }
}
