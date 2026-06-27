using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class StatsPersonaje
    {
        [Key]
        public int Personaje_ID { get; set; }

        public short ArmorClassBonus { get; set; }
        public short InitiativeBonus { get; set; }
        public short ProficiencyBonus { get; set; }
        public short MovementBonus { get; set; }
        public short Inspiration { get; set; }

        public virtual Personaje? Personaje { get; set; }
    }
}
