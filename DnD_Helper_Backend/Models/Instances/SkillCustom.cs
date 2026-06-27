using DnD_Helper_Backend.Models.Global;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class SkillCustom
    {
        [Key]
        public int SkillCustom_ID { get; set; }
        public int Personaje_ID { get; set; }
        public string Nombre { get; set; } = null!;
        public byte Habilidad_ID { get; set; }

        public virtual Personaje? Personaje { get; set; }
        public virtual Habilidad? Habilidad { get; set; }
        public virtual ICollection<SkillPersonaje> SkillPersonajes { get; set; } = new List<SkillPersonaje>();

    }
}
