using DnD_Helper_Backend.Models.Global;

namespace DnD_Helper_Backend.Models.Instances
{
    public class ScorePersonaje
    {
        public int Personaje_ID { get; set; }
        public byte Habilidad_ID { get; set; }

        public byte ValorBase { get; set; }
        public short BonusTemporal { get; set; }
        public bool EsProficiente { get; set; }

        public virtual Personaje? Personaje { get; set; }
        public virtual Habilidad Habilidad { get; set; } = null!;
    }
}
