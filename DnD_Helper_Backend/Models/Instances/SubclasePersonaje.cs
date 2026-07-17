using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class SubclasePersonaje
    {
        [Key]
        public int SubclasePersonaje_ID { get; set; }

        public int ClasePersonaje_ID { get; set; }
        public int? SubclaseTemplate_ID { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }

        // Navegación
        public virtual ClasePersonaje? ClasePersonaje { get; set; }
        public virtual SubclaseTemplate? SubclaseTemplate { get; set; }
    }
}
