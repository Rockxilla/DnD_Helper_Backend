namespace DnD_Helper_Backend.DTOs
{
    public class ClasePersonajeDto
    {
        public short? ClaseTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public byte? Nivel { get; set; }
    }
    public class ClaseListDto
    {
        public short? ClaseTemplate_ID { get; set; }
        public string? Nombre { get; set; }
    }

    public class UpdateClasePersonajeDto
    {
        public int ClasePersonaje_ID { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public byte? Nivel { get; set; }
    }
}
