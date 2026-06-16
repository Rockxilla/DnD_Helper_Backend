namespace DnD_Helper_Backend.DTOs
{
    public class PersonajeDto
    {
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }

        public UsuarioDto? Usuario { get; set; }
        public ClaseDto? Clase { get; set; }
        public RazaDto? Raza { get; set; }
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
        public short Clase_ID { get; set; }
        public short Raza_ID { get; set; }
    }

    public class UpdatePersonajeDto
    {
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }

        public int Usuario_ID { get; set; }
        public short Clase_ID { get; set; }
        public short Raza_ID { get; set; }
    }

    public class UsuarioDto
    {
        public int Usuario_ID { get; set; }
        public string? Nombre { get; set; }
    }

    public class ClaseDto
    {
        public short Clase_ID { get; set; }
        public string? Nombre { get; set; }
    }

    public class RazaDto
    {
        public short Raza_ID { get; set; }
        public string? Nombre { get; set; }
    }
}