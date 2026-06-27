using DnD_Helper_Backend.Models.Instances;
using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Global
{
    public class Dado
    {
        [Key]
        public byte Dado_ID { get; set; }
        public string Nombre { get; set; } = null!;
        public byte Valor { get; set; }

        public ICollection<ClaseTemplate> ClaseTemplates { get; set; } = new List<ClaseTemplate>();
        public ICollection<ClasePersonaje> ClasePersonajes { get; set; } = new List<ClasePersonaje>();
    }
}
