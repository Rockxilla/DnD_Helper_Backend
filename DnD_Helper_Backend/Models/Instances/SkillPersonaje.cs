using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class SkillPersonaje
    {
        [Key]
        public int SkillPersonaje_ID { get; set; }

        public int Personaje_ID { get; set; }

        public int? SkillTemplate_ID { get; set; }
        public int? SkillCustom_ID { get; set; }

        public byte Proficiencia { get; set; }
        public short BonusTemporal { get; set; }

        public virtual Personaje? Personaje { get; set; }
        public virtual SkillTemplate? SkillTemplate { get; set; }
        public virtual SkillCustom? SkillCustom { get; set; }
    }
}
