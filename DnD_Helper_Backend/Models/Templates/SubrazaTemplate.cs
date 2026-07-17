using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Templates
{
    public class SubrazaTemplate
    {
        [Key]
        public int SubrazaTemplate_ID { get; set; }

        public short RazaTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }

        public RazaTemplate RazaTemplate { get; set; } = null!;
    }
}
