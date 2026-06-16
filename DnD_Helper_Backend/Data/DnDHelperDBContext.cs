using DnD_Helper_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace DnD_Helper_Backend.Data
{
    public partial class DnDHelperDBContext : DbContext
    {
        public DnDHelperDBContext()
        {
        }
        public DnDHelperDBContext(DbContextOptions<DnDHelperDBContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Personaje> Personajes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Clase> Clases { get; set; }
        public virtual DbSet<Raza> Razas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje>(entity =>
            {
                entity.ToTable("Personaje");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.Usuario).WithMany(p => p.Personajes).HasForeignKey(d => d.Usuario_ID).HasConstraintName("FK_PerUser");

                entity.HasOne(d => d.Clase).WithMany(p => p.Personajes).HasForeignKey(d => d.Clase_ID).HasConstraintName("FK_PerClase");

                entity.HasOne(d => d.Raza).WithMany(p => p.Personajes).HasForeignKey(d => d.Raza_ID).HasConstraintName("FK_PerRaza");
            });

            modelBuilder.Entity<Personaje>()
                .HasQueryFilter(x => x.Estatus == true);

            modelBuilder.Entity<Clase>(entity =>
            {
                entity.ToTable("Clase");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Raza>(entity =>
            {
                entity.ToTable("Raza");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Contraseña).HasMaxLength(20);

            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
