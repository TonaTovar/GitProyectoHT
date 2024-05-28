using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class GitHubProyectoHtContext : DbContext
{
    public GitHubProyectoHtContext()
    {
    }

    public GitHubProyectoHtContext(DbContextOptions<GitHubProyectoHtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aerolinea> Aerolineas { get; set; }

    public virtual DbSet<Vuelo> Vuelos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=GitHubProyectoHT; Trusted_Connection=True; TrustServerCertificate=True; User ID= sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aerolinea>(entity =>
        {
            entity.HasKey(e => e.IdAerolinea).HasName("PK__Aeroline__E58B9C72E98D5222");

            entity.ToTable("Aerolinea");

            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.AerolineaNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vuelo>(entity =>
        {
            entity.HasKey(e => e.IdVuelo).HasName("PK__Vuelos__25FEB442E86256BC");

            entity.Property(e => e.IdVuelo).HasColumnName("Id_Vuelo");
            entity.Property(e => e.Destino)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HoraLlegada)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hora_LLegada");
            entity.Property(e => e.HoraSalida)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Hora_Salida");
            entity.Property(e => e.IdAerolinea).HasColumnName("Id_Aerolinea");
            entity.Property(e => e.NumeroVuelo).HasColumnName("Numero_Vuelo");
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAerolineaNavigation).WithMany(p => p.Vuelos)
                .HasForeignKey(d => d.IdAerolinea)
                .HasConstraintName("FK__Vuelos__Id_Aerol__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
