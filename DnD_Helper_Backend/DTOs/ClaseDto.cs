namespace DnD_Helper_Backend.DTOs
{
    public class GetClasePersonajeDto
    {
        public short? ClaseTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public byte? Nivel { get; set; }
        public byte? Hit_Dice_ID { get; set; }

    }
    public class ClaseListDto
    {
        public short? ClaseTemplate_ID { get; set; }
        public string? Nombre { get; set; }
    }

}
