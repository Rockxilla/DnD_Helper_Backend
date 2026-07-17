using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class SubrazaPersonaje
    {
        [Key]
        public int SubrazaPersonaje_ID { get; set; }

        public int RazaPersonaje_ID { get; set; }
        public int? SubrazaTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }

        // Navegación
        public virtual RazaPersonaje? RazaPersonaje { get; set; }
        public virtual SubrazaTemplate? SubrazaTemplate { get; set; }
    }
}
