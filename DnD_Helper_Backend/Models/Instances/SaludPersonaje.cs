using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class SaludPersonaje
    {
        [Key]
        public int Personaje_ID { get; set; }

        public short MaxHp { get; set; }
        public short CurrentHp { get; set; }
        public short TempHp { get; set; }

        public short MaxHpBonus { get; set; }
        public short MaxHpPenalty { get; set; }

        public virtual Personaje? Personaje { get; set; }
    }
}
