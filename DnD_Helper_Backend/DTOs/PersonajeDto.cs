namespace DnD_Helper_Backend.DTOs
{
    public class PersonajeDto
    {
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }

        public UsuarioDto? Usuario { get; set; }
    }
    public class PersonajeListDto
    {
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
    }

    public class CreatePersonajeDto
    {
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
        public int Usuario_ID { get; set; }

        public short? ClaseTemplate_ID { get; set; }
        public short? SubclaseTemplate_ID { get; set; }

        public short? RazaTemplate_ID { get; set; }
        public int? SubrazaTemplate_ID { get; set; }


        // CUSTOM CLASE/RAZA
        public string? ClaseNombre { get; set; }
        public string? ClaseDescripcion { get; set; }
        public byte? HitDice_ID { get; set; }

        public string? RazaNombre { get; set; }
        public string? RazaDescripcion { get; set; }

        public string? SubclaseNombre { get; set; }
        public string? SubclaseDescripcion { get; set; }
        public string? SubrazaNombre { get; set; }
        public string? SubrazaDescripcion { get; set; }

        public byte ClaseNivelInicial { get; set; } = 1;
        public ScoresDto Scores { get; set; } = new();
    }

    public class UpdatePersonajeDto
    {
        public int Personaje_ID { get; set; }

        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
    }

    public class UsuarioDto
    {
        public int Usuario_ID { get; set; }
        public string? Nombre { get; set; }
    }
}