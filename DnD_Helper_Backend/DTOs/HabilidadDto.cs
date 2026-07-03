namespace DnD_Helper_Backend.DTOs
{
    public class ScoresDto
    {
        public byte STR { get; set; }

        public byte DEX { get; set; }

        public byte CON { get; set; }

        public byte INT { get; set; }

        public byte WIS { get; set; }

        public byte CHA { get; set; }
    }

    public class GetScorePersonajeDto
    {
        public byte Habilidad_ID { get; set; }

        public string NombreCorto { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public byte ValorBase { get; set; }

        public short BonusTemporal { get; set; }

        public bool EsProficiente { get; set; }
    }

}
