namespace DnD_Helper_Backend.DTOs
{
    public class SkillDisplayDto
    {
        public int SkillPersonaje_ID { get; set; }

        public string Nombre { get; set; } = "";

        public byte Habilidad_ID { get; set; }

        public string Habilidad { get; set; } = "";

        public byte ValorHabilidad { get; set; }

        public int ModificadorHabilidad { get; set; }

        public byte Proficiencia { get; set; }

        public int BonusProficiencia { get; set; }

        public short BonusTemporal { get; set; }

        public int BonusTotal { get; set; }
    }
}
