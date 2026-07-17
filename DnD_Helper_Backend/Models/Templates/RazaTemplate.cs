using DnD_Helper_Backend.Models.Instances;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Templates
{
    public class RazaTemplate
    {
        [Key]
        public short RazaTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }
        public ICollection<RazaPersonaje> Razas { get; set; } = new List<RazaPersonaje>();
        public ICollection<SubrazaTemplate> Subrazas { get; set; } = new List<SubrazaTemplate>();
    }
}
