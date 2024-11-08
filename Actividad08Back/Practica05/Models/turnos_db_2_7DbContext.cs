﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practica05.Models;

public partial class turnos_db_2_7DbContext : DbContext
{
    public turnos_db_2_7DbContext(DbContextOptions<turnos_db_2_7DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleTurno> TDetallesTurno { get; set; }

    public virtual DbSet<Servicio> TServicios { get; set; }

    public virtual DbSet<Turno> TTurnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleTurno>(entity =>
        {
            entity.HasKey(e => new { e.IdTurno, e.IdServicio });

            entity.ToTable("T_DETALLES_TURNO");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("observaciones");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.TDetallesTurno)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DETALLES_TURNO_T_SERVICIOS");

            entity.HasOne(d => d.IdTurnoNavigation).WithMany(p => p.TDetallesTurno)
                .HasForeignKey(d => d.IdTurno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_DETALLES_TURNO_T_TURNOS");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio);

            entity.ToTable("T_SERVICIOS");

            entity.Property(e => e.IdServicio)
                .ValueGeneratedNever()
                .HasColumnName("id_servicio");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.EnPromocion)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("enPromocion");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno);

            entity.ToTable("T_TURNOS");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.Cliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.Fecha)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("fecha");
            entity.Property(e => e.Hora)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("hora");
            entity.Navigation(e => e.TDetallesTurno).AutoInclude();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}