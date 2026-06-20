namespace DnD_Helper_Backend.DTOs
{
    public class RazaPersonajeDto
    {
        public short? RazaTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
    public class RazaListDto
    {
        public short? RazaTemplate_ID { get; set; }
        public string? Nombre { get; set; }
    }
}
