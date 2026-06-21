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

        public short ClaseTemplate_ID { get; set; }
        public short RazaTemplate_ID { get; set; }

        public byte ClaseNivelInicial { get; set; } = 1;
    }

    public class UpdatePersonajeDto
    {
        public int Personaje_ID { get; set; }

        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
    }

    public class PersonajeDetailDto
    {
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int Experiencia { get; set; }

        public UsuarioDto? Usuario { get; set; }

        public RazaPersonajeDto? Raza { get; set; }
        public List<ClasePersonajeDto>? Clases { get; set; }

        // public List<SkillDto> Skills { get; set; }
    }


    public class UsuarioDto
    {
        public int Usuario_ID { get; set; }
        public string? Nombre { get; set; }
    }
}