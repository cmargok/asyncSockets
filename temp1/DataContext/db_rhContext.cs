using System;
using System.Collections.Generic;
using DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataContext
{
    public partial class db_rhContext : DbContext
    {
       

        public virtual DbSet<Cargo> Cargos { get; set; } = null!;
        public virtual DbSet<Ciudad> Ciudades { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Historico> Historicos { get; set; } = null!;
        public virtual DbSet<Localizacion> Localizaciones { get; set; } = null!;
        public virtual DbSet<LozalizacionDepartamento> LozalizacionDepartamentos { get; set; } = null!;
        public virtual DbSet<Pais> Paises { get; set; } = null!;

       

        public db_rhContext(DbContextOptions<db_rhContext> options) : base(options)
        {

        }



        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.ToTable("CARGOS");

                entity.Property(e => e.CargoId).HasColumnName("cargo_ID");

                entity.Property(e => e.CargoNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cargo_nombre");

                entity.Property(e => e.CargoSueldoMaximo)
                    .HasColumnType("money")
                    .HasColumnName("cargo_sueldo_maximo");

                entity.Property(e => e.CargoSueldoMinimo)
                    .HasColumnType("money")
                    .HasColumnName("cargo_sueldo_minimo");
            });

            modelBuilder.Entity<Ciudad>(entity =>
            {
                entity.HasKey(e => e.CiudId)
                    .HasName("PK_Ciudades");

                entity.ToTable("CIUDADES");

                entity.Property(e => e.CiudId).HasColumnName("ciud_ID");

                entity.Property(e => e.CiudNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ciud_nombre");

                entity.Property(e => e.PaisId).HasColumnName("pais_ID");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Ciudades)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("FK_Ciudades_Pais");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.DptoId)
                    .HasName("PK_Departamentos");

                entity.ToTable("DEPARTAMENTOS");

                entity.Property(e => e.DptoId).HasColumnName("dpto_ID");

                entity.Property(e => e.DptoNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dpto_nombre");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.EmplId)
                    .HasName("PK_Empleado");

                entity.ToTable("EMPLEADOS");

                entity.Property(e => e.EmplId)
                    .ValueGeneratedNever()
                    .HasColumnName("empl_ID");

                entity.Property(e => e.EmplCargoId).HasColumnName("empl_cargo_ID");

                entity.Property(e => e.EmplComision).HasColumnName("empl_comision");

                entity.Property(e => e.EmplDptoId).HasColumnName("empl_dpto_ID");

                entity.Property(e => e.EmplEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("empl_email");

                entity.Property(e => e.EmplGerenteId).HasColumnName("empl_Gerente_ID");

                entity.Property(e => e.EmplPrimerNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empl_primer_nombre");

                entity.Property(e => e.EmplSegundoNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("empl_segundo_nombre");

                entity.Property(e => e.EmplSueldo)
                    .HasColumnType("money")
                    .HasColumnName("empl_sueldo");

                entity.HasOne(d => d.EmplCargo)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.EmplCargoId)
                    .HasConstraintName("FK_Empleado_Cargo");

                entity.HasOne(d => d.EmplDpto)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.EmplDptoId)
                    .HasConstraintName("FK_Empleado_Departamento");

                entity.HasOne(d => d.EmplGerente)
                    .WithMany(p => p.InverseEmplGerente)
                    .HasForeignKey(d => d.EmplGerenteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleados_Gerente");
            });

            modelBuilder.Entity<Historico>(entity =>
            {
                entity.HasKey(e => e.EmphistId)
                    .HasName("PK_Historico");

                entity.ToTable("HISTORICO");

                entity.Property(e => e.EmphistId)
                    .ValueGeneratedNever()
                    .HasColumnName("emphist_ID");

                entity.Property(e => e.EmphistCargoId).HasColumnName("emphist_cargo_ID");

                entity.Property(e => e.EmphistDptoId).HasColumnName("emphist_dpto_ID");

                entity.Property(e => e.EmphistEmplId).HasColumnName("emphist_empl_ID");

                entity.Property(e => e.EmphistFechaRetiro)
                    .HasColumnType("date")
                    .HasColumnName("emphist_fecha_retiro");

                entity.HasOne(d => d.EmphistCargo)
                    .WithMany(p => p.Historicos)
                    .HasForeignKey(d => d.EmphistCargoId)
                    .HasConstraintName("FK_Historico_Cargo");

                entity.HasOne(d => d.EmphistDpto)
                    .WithMany(p => p.Historicos)
                    .HasForeignKey(d => d.EmphistDptoId)
                    .HasConstraintName("FK_Historico_Departamento");

                entity.HasOne(d => d.EmphistEmpl)
                    .WithMany(p => p.Historicos)
                    .HasForeignKey(d => d.EmphistEmplId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Historico_Empleado");
            });

            modelBuilder.Entity<Localizacion>(entity =>
            {
                entity.HasKey(e => e.LocalizId)
                    .HasName("PK_localizaciones");

                entity.ToTable("LOCALIZACIONES");

                entity.Property(e => e.LocalizId).HasColumnName("localiz_ID");

                entity.Property(e => e.CiudId).HasColumnName("ciud_ID");

                entity.Property(e => e.LocalizNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("localiz_nombre");

                entity.HasOne(d => d.Ciud)
                    .WithMany(p => p.Localizaciones)
                    .HasForeignKey(d => d.CiudId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Localizaciones_Ciudad");
            });

            modelBuilder.Entity<LozalizacionDepartamento>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOZALIZACION_DEPARTAMENTO");

                entity.Property(e => e.DptoId).HasColumnName("dpto_ID");

                entity.Property(e => e.LocalizId).HasColumnName("localiz_ID");

                entity.HasOne(d => d.Dpto)
                    .WithMany()
                    .HasForeignKey(d => d.DptoId)
                    .HasConstraintName("FK_Departamento_Localizacion");

                entity.HasOne(d => d.Localiz)
                    .WithMany()
                    .HasForeignKey(d => d.LocalizId)
                    .HasConstraintName("FK_Localizacion_Departamento");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.pais_ID)
                    .HasName("PK_Pais");

                entity.ToTable("PAISES");

                entity.Property(e => e.pais_ID).HasColumnName("pais_ID");

                entity.Property(e => e.pais_nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pais_nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
