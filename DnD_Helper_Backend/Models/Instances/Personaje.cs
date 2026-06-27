using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class Personaje
    {
        [Key]
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
        public int? Usuario_ID { get; set; }
        public bool Estatus { get; set; }

        public virtual Usuario? Usuario { get; set; }
        public ICollection<ClasePersonaje>? ClasesPersonaje { get; set; } = new List<ClasePersonaje>();
        public virtual RazaPersonaje? RazaPersonaje { get; set; }
        public virtual StatsPersonaje? StatsPersonaje { get; set; }
        public virtual SaludPersonaje? SaludPersonaje { get; set; }
        public ICollection<SkillCustom> SkillCustoms { get; set; } = new List<SkillCustom>();
        public virtual ICollection<SkillPersonaje>? SkillsPersonaje { get; set; } = new List<SkillPersonaje>();
        public virtual ICollection<ScorePersonaje>? ScoresPersonaje { get; set; } = new List<ScorePersonaje>();

    }
}
