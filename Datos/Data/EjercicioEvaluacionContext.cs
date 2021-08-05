using System;
using System.Collections.Generic;
using Datos.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Datos.ModelosNuevos
{
    public partial class EjercicioEvaluacionContext : IdentityDbContext<AplicationUser,UserRole,string>
    {
       
        public EjercicioEvaluacionContext(DbContextOptions<EjercicioEvaluacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__TipoVehi__06370DAD85D535D2");

                entity.ToTable("TipoVehiculo");

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodigoVehiculoNavigation)
                    .WithMany(p => p.TipoVehiculos)
                    .HasForeignKey(d => d.CodigoVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Vehicuo");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Vehiculo__06370DAD148103C7");

                entity.ToTable("Vehiculo");

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            
        }

        
    }
}
