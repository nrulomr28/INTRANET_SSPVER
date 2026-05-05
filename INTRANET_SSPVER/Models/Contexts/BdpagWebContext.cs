using System;
using System.Collections.Generic;
using INTRANET_SSPVER.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace INTRANET_SSPVER.Models.Contexts;

public partial class BdpagWebContext : DbContext
{
    public BdpagWebContext()
    {
    }

    public BdpagWebContext(DbContextOptions<BdpagWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AreaDirectorio> AreaDirectorios { get; set; }

    public virtual DbSet<AreasCalea> AreasCaleas { get; set; }

    public virtual DbSet<AvisoPrivacidad> AvisoPrivacidads { get; set; }

    public virtual DbSet<ComiteTransparencium> ComiteTransparencia { get; set; }

    public virtual DbSet<DirectivaCalea> DirectivaCaleas { get; set; }

    public virtual DbSet<DirectorioTelefonico> DirectorioTelefonicos { get; set; }

    public virtual DbSet<Formato> Formatos { get; set; }

    public virtual DbSet<LogAcceso> LogAccesos { get; set; }

    public virtual DbSet<UbicacionFisica> UbicacionFisicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK_cat_Area");

            entity.ToTable("Area");

            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AreaDirectorio>(entity =>
        {
            entity.HasKey(e => e.IdArea);

            entity.ToTable("AreaDirectorio");

            entity.Property(e => e.Area).HasMaxLength(250);
        });

        modelBuilder.Entity<AreasCalea>(entity =>
        {
            entity.HasKey(e => e.IdAreaCalea);

            entity.ToTable("AreasCalea");

            entity.Property(e => e.IdAreaCalea).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreAreaCalea).HasMaxLength(200);

            entity.HasOne(d => d.IdAreaCaleaNavigation).WithOne(p => p.AreasCalea)
                .HasForeignKey<AreasCalea>(d => d.IdAreaCalea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AreasCalea_DirectivaCalea");
        });

        modelBuilder.Entity<AvisoPrivacidad>(entity =>
        {
            entity.HasKey(e => e.IdAvisoPrivacidad);

            entity.ToTable("AvisoPrivacidad");

            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AvisoIntegralUrl).IsUnicode(false);
            entity.Property(e => e.AvisoSimplificadoUrl).IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.SistemaDatosUrl).IsUnicode(false);
        });

        modelBuilder.Entity<ComiteTransparencium>(entity =>
        {
            entity.HasKey(e => e.IdComite);

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.NumeroActa).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(300);
        });

        modelBuilder.Entity<DirectivaCalea>(entity =>
        {
            entity.HasKey(e => e.IdDirectiva);

            entity.ToTable("DirectivaCalea");

            entity.Property(e => e.ExtencionArchivo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.NombreDirectiva)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UrlArchivoDirectiva).IsUnicode(false);
            entity.Property(e => e.UrlImgDirectiva).IsUnicode(false);
        });

        modelBuilder.Entity<DirectorioTelefonico>(entity =>
        {
            entity.HasKey(e => e.IdDirectorio).HasName("PK_tdirectorio_tel");

            entity.ToTable("DirectorioTelefonico");

            entity.Property(e => e.Extension)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Formato>(entity =>
        {
            entity.HasKey(e => e.IdFormato).HasName("PK_tformatos_tecnologias");

            entity.ToTable("Formato");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.RutaArchivo).HasMaxLength(300);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Formatos)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Formato_Area1");
        });

        modelBuilder.Entity<LogAcceso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogAcces__3214EC074E2817C9");

            entity.ToTable("LogAcceso");

            entity.Property(e => e.Equipo).HasMaxLength(100);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("IP");
            entity.Property(e => e.Modulo).HasMaxLength(100);
            entity.Property(e => e.SessionId).HasMaxLength(50);
            entity.Property(e => e.Usuario).HasMaxLength(100);
        });

        modelBuilder.Entity<UbicacionFisica>(entity =>
        {
            entity.HasKey(e => e.IdUbicacionFisica);

            entity.ToTable("UbicacionFisica");

            entity.Property(e => e.UbicacionFisica1)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("UbicacionFisica");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
