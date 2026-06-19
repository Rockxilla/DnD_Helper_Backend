namespace DnD_Helper_Backend.DTOs
{
    public class ClaseDto
    {
        public short? ClaseTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
    public class ClaseListDto
    {
        public short? ClaseTemplate_ID { get; set; }
        public string? Nombre { get; set; }
    }
}
