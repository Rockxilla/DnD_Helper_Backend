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
        public virtual DbSet<ClasePersonaje> ClasePersonajes { get; set; }
        public virtual DbSet<RazaPersonaje> RazaPersonajes { get; set; }
        public virtual DbSet<ClaseTemplate> ClaseTemplates { get; set; }
        public virtual DbSet<RazaTemplate> RazaTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje>(entity =>
            {
                entity.ToTable("Personaje");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.Usuario).WithMany(p => p.Personajes).HasForeignKey(d => d.Usuario_ID).HasConstraintName("FK_PerUser");

                // Class relationship (1 to 1)
                entity.HasOne(d => d.ClasePersonaje).WithOne(cp => cp.Personaje).HasForeignKey<ClasePersonaje>(cp => cp.Personaje_ID).HasConstraintName("FK_ClaPerPersonaje");

                // Race relationship (1 to 1)
                entity.HasOne(d => d.RazaPersonaje).WithOne(rp => rp.Personaje).HasForeignKey<RazaPersonaje>(rp => rp.Personaje_ID).HasConstraintName("FK_RazPerPersonaje");
            });

            modelBuilder.Entity<Personaje>().HasQueryFilter(x => x.Estatus == true);

            modelBuilder.Entity<ClasePersonaje>(entity =>
            {
                entity.ToTable("ClasePersonaje");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(cp => cp.ClaseTemplate).WithMany().HasForeignKey(cp => cp.ClaseTemplate_ID).HasConstraintName("FK_ClaPerTemplate");
            });

            modelBuilder.Entity<RazaPersonaje>(entity =>
            {
                entity.ToTable("RazaPersonaje");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(rp => rp.RazaTemplate).WithMany().HasForeignKey(rp => rp.RazaTemplate_ID).HasConstraintName("FK_RazPerTemplate");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Contraseña).HasMaxLength(20);

            });

            modelBuilder.Entity<ClaseTemplate>().ToTable("ClaseTemplate");

            modelBuilder.Entity<RazaTemplate>().ToTable("RazaTemplate");

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
