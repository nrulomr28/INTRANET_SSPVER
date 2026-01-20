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

    public virtual DbSet<TdirectorioTel> TdirectorioTels { get; set; }

    public virtual DbSet<TformatosTecnologia> TformatosTecnologias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<TdirectorioTel>(entity =>
        {
            entity.HasKey(e => e.IdDirectorio);

            entity.ToTable("tdirectorio_tel");

            entity.Property(e => e.IdDirectorio).HasColumnName("idDirectorio");
            entity.Property(e => e.Area)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ext)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TformatosTecnologia>(entity =>
        {
            entity.ToTable("tformatos_tecnologias");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.RutaArchivo).HasMaxLength(300);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
