using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models
{
    public class ClaseTemplate
    {
        [Key]
        public short ClaseTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }
    }
}
