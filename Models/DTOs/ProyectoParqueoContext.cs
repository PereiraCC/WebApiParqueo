using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models.DTOs;

public partial class ProyectoParqueoContext : DbContext
{
    private string connectionBD;

    public ProyectoParqueoContext(string connectionBD)
    {
        this.connectionBD = connectionBD;
    }

    public ProyectoParqueoContext(DbContextOptions<ProyectoParqueoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Parqueo> Parqueos { get; set; }

    public virtual DbSet<Tiquete> Tiquetes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionBD);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Idempleado).HasName("PK__Empleado__50621DCD59E1639F");

            entity.ToTable("Empleado");

            entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Idparqueo).HasColumnName("IDParqueo");
            entity.Property(e => e.NumeroEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PersonaContacto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdparqueoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Idparqueo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Empleado__IDParq__398D8EEE");
        });

        modelBuilder.Entity<Parqueo>(entity =>
        {
            entity.HasKey(e => e.Idparqueo).HasName("PK__Parqueo__0217800FE5051642");

            entity.ToTable("Parqueo");

            entity.Property(e => e.Idparqueo).HasColumnName("IDParqueo");
            entity.Property(e => e.HoraApertura).HasColumnType("datetime");
            entity.Property(e => e.HoraCierre).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tiquete>(entity =>
        {
            entity.HasKey(e => e.Idtiquete).HasName("PK__Tiquete__2A1B3FD9CAAD779D");

            entity.ToTable("Tiquete");

            entity.Property(e => e.Idtiquete).HasColumnName("IDTiquete");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaSalida).HasColumnType("datetime");
            entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
            entity.Property(e => e.Idparqueo).HasColumnName("IDParqueo");
            entity.Property(e => e.MontoPagar)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreParqueo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TiempoConsumido)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.Tiquetes)
                .HasForeignKey(d => d.Idempleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tiquete__IDEmple__3D5E1FD2");

            entity.HasOne(d => d.IdparqueoNavigation).WithMany(p => p.Tiquetes)
                .HasForeignKey(d => d.Idparqueo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tiquete__IDParqu__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
