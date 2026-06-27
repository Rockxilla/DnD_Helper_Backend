using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Global
{
    public class Habilidad
    {
        [Key]
        public byte Habilidad_ID { get; set; }
        public string NombreCorto { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public ICollection<SkillCustom> SkillCustoms { get; set; } = new List<SkillCustom>();
        public ICollection<SkillTemplate> SkillTemplates { get; set; } = new List<SkillTemplate>();
        public ICollection<SkillPersonaje> SkillPersonajes { get; set; } = new List<SkillPersonaje>();
        public virtual ICollection<ScorePersonaje>? ScoresPersonaje { get; set; } = new List<ScorePersonaje>();

    }
}
