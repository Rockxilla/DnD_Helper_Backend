using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class RazaPersonaje
    {
        [Key]
        public int RazaPersonaje_ID { get; set; }

        public int Personaje_ID { get; set; }

        public short? RazaTemplate_ID { get; set; }

        // EDITABLE
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public bool Estatus { get; set; }

        // NAVEGACION
        public virtual Personaje? Personaje { get; set; }
        public virtual RazaTemplate? RazaTemplate { get; set; }
    }
}
