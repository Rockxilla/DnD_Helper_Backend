using DnD_Helper_Backend.Models;
using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Models.Templates;
using DnD_Helper_Backend.Models.Global;
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
        // Templates y Globales
        public virtual DbSet<Dado> Dados { get; set; }
        public virtual DbSet<Habilidad> Habilidades { get; set; }
        public virtual DbSet<SkillTemplate> SkillTemplates { get; set; }
        public virtual DbSet<ClaseTemplate> ClaseTemplates { get; set; }
        public virtual DbSet<RazaTemplate> RazaTemplates { get; set; }
        
        // Datos del Personaje
        public virtual DbSet<Personaje> Personajes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<ClasePersonaje> ClasePersonajes { get; set; }
        public virtual DbSet<RazaPersonaje> RazaPersonajes { get; set; }
        public virtual DbSet<SkillCustom> SkillCustoms { get; set; }
        public virtual DbSet<SkillPersonaje> SkillPersonajes { get; set; }
        public virtual DbSet<ScorePersonaje> ScorePersonajes { get; set; }
        public virtual DbSet<StatsPersonaje> StatsPersonajes { get; set; }
        public virtual DbSet<SaludPersonaje> SaludPersonajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personaje>(entity =>
            {
                entity.ToTable("Personaje");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.Usuario).WithMany(p => p.Personajes).HasForeignKey(d => d.Usuario_ID).HasConstraintName("FK_PerUser");

                // Relación con Clases
                entity.HasMany(p => p.ClasesPersonaje).WithOne(c => c.Personaje).HasForeignKey(c => c.Personaje_ID).HasConstraintName("FK_ClaPerPersonaje");
                // Relación con Raza
                entity.HasOne(d => d.RazaPersonaje).WithOne(rp => rp.Personaje).HasForeignKey<RazaPersonaje>(rp => rp.Personaje_ID).HasConstraintName("FK_RazPerPersonaje");
                // Relación con Stats
                entity.HasOne(p => p.StatsPersonaje).WithOne(s => s.Personaje).HasForeignKey<StatsPersonaje>(s => s.Personaje_ID);
                entity.HasOne(p => p.SaludPersonaje).WithOne(s => s.Personaje).HasForeignKey<SaludPersonaje>(s => s.Personaje_ID);
            });

            modelBuilder.Entity<Personaje>().HasQueryFilter(x => x.Estatus == true);
            
            // Clases
            modelBuilder.Entity<ClasePersonaje>(entity =>
            {
                entity.ToTable("ClasePersonaje");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(cp => cp.ClaseTemplate).WithMany(ct => ct.Clases).HasForeignKey(cp => cp.ClaseTemplate_ID).HasConstraintName("FK_ClaPerTemplate");

                entity.HasOne(cp => cp.HitDice).WithMany(d => d.ClasePersonajes).HasForeignKey(cp => cp.Hit_Dice_ID).HasConstraintName("FK_ClaPerHitDice");
            });
            modelBuilder.Entity<ClaseTemplate>(entity =>
            {
                entity.ToTable("ClaseTemplate");

                entity.HasOne(c => c.HitDice).WithMany(d => d.ClaseTemplates).HasForeignKey(c => c.Hit_Dice_ID).HasConstraintName("FK_ClaseTemplate_Dado");
            });

            // Razas
            modelBuilder.Entity<RazaPersonaje>(entity =>
            {
                entity.ToTable("RazaPersonaje");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(rp => rp.RazaTemplate).WithMany().HasForeignKey(rp => rp.RazaTemplate_ID).HasConstraintName("FK_RazPerTemplate");
            });
            modelBuilder.Entity<RazaTemplate>().ToTable("RazaTemplate");

            //Dados
            modelBuilder.Entity<Dado>(entity =>
            {
                entity.ToTable("Dado");

                entity.Property(e => e.Nombre).HasMaxLength(10);
            });

            //Ability Scores
            modelBuilder.Entity<ScorePersonaje>(entity =>
            {
                entity.ToTable("ScorePersonaje");

                entity.HasKey(e => new
                {
                    e.Personaje_ID,
                    e.Habilidad_ID
                });

                entity.HasOne(s => s.Personaje).WithMany(p => p.ScoresPersonaje).HasForeignKey(s => s.Personaje_ID);

                entity.HasOne(s => s.Habilidad).WithMany(h => h.ScoresPersonaje).HasForeignKey(s => s.Habilidad_ID);
            });

            modelBuilder.Entity<Habilidad>(entity =>
            {
                entity.ToTable("Habilidad");

                entity.Property(e => e.Nombre).HasMaxLength(20);

                entity.Property(e => e.NombreCorto).HasMaxLength(3);
            });

            //Skills
            modelBuilder.Entity<SkillTemplate>(entity =>
            {
                entity.ToTable("SkillTemplate");

                entity.HasOne(s => s.Habilidad).WithMany(h => h.SkillTemplates).HasForeignKey(s => s.Habilidad_ID).HasConstraintName("FK_SkillTemplate_Habilidad");
            });
            modelBuilder.Entity<SkillCustom>(entity =>
            {
                entity.ToTable("SkillCustom");

                entity.HasOne(s => s.Personaje).WithMany(p => p.SkillCustoms).HasForeignKey(s => s.Personaje_ID);

                entity.HasOne(s => s.Habilidad).WithMany(h => h.SkillCustoms).HasForeignKey(s => s.Habilidad_ID);

                entity.HasIndex(e => new
                {
                    e.Personaje_ID,
                    e.Nombre
                }).IsUnique();
            });
            modelBuilder.Entity<SkillPersonaje>(entity =>
            {
                entity.ToTable("SkillPersonaje");

                entity.HasOne(sp => sp.Personaje).WithMany(p => p.SkillsPersonaje).HasForeignKey(sp => sp.Personaje_ID);

                entity.HasOne(sp => sp.SkillTemplate).WithMany(st => st.SkillPersonajes).HasForeignKey(sp => sp.SkillTemplate_ID);

                entity.HasOne(sp => sp.SkillCustom).WithMany(sc => sc.SkillPersonajes).HasForeignKey(sp => sp.SkillCustom_ID);

                entity.ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_SkillPersonaje_OneSource",@"(([skillTemplate_ID] IS NOT NULL AND [skillCustom_ID] IS NULL)
                        OR 
                        ([skillTemplate_ID] IS NULL AND [skillCustom_ID] IS NOT NULL)
                    )"));
            });

            //Stats y Salud
            modelBuilder.Entity<StatsPersonaje>().ToTable("StatsPersonaje");
            modelBuilder.Entity<SaludPersonaje>().ToTable("SaludPersonaje");
            
            // Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Contraseña).HasMaxLength(20);

            });

            modelBuilder.Entity<ScorePersonaje>().HasKey(x => new
            {
                x.Personaje_ID,
                x.Habilidad_ID
            });
            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
