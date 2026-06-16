using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models
{
    public class Raza
    {
        [Key]
        public short Raza_ID { get; set; }
        public string? Nombre { get; set; }
        public bool Estatus { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
    }
}
