using DnD_Helper_Backend.Models.Global;
using DnD_Helper_Backend.Models.Templates;
using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models.Instances
{
    public class ClasePersonaje
    {
        [Key]
        public int ClasePersonaje_ID { get; set; }

        public int Personaje_ID { get; set; }

        public short? ClaseTemplate_ID { get; set; }

        // EDITABLE
        public string?  Nombre { get; set; }
        public string?  Descripcion { get; set; }
        public byte?    Nivel { get; set; }
        public byte? Hit_Dice_ID { get; set; }

        public bool Estatus { get; set; }

        // NAVEGACION
        public virtual Personaje? Personaje { get; set; }
        public virtual ClaseTemplate? ClaseTemplate { get; set; }
        public virtual Dado? HitDice { get; set; }

    }
}
