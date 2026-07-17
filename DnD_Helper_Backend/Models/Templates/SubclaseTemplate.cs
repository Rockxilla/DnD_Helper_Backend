using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Templates
{
    public class SubclaseTemplate
    {
        [Key]
        public int SubclaseTemplate_ID { get; set; }

        public short ClaseTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }

        public ClaseTemplate ClaseTemplate { get; set; } = null!;
    }
}
