using DnD_Helper_Backend.Models.Global;
using DnD_Helper_Backend.Models.Instances;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Templates
{
    public class SkillTemplate
    {
        [Key]
        public int SkillTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public byte Habilidad_ID { get; set; }

        public virtual Habilidad? Habilidad { get; set; }

        public virtual ICollection<SkillPersonaje> SkillPersonajes { get; set; }= new List<SkillPersonaje>();
    }
}
