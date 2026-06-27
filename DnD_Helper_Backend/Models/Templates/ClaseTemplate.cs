using DnD_Helper_Backend.Models.Global;
using DnD_Helper_Backend.Models.Instances;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Templates
{
    public class ClaseTemplate
    {
        [Key]
        public short ClaseTemplate_ID { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public byte? Hit_Dice_ID { get; set; }

        public bool Estatus { get; set; }

        public Dado? HitDice { get; set; }
        public ICollection<ClasePersonaje> Clases { get; set; } = new List<ClasePersonaje>();
    }
}
