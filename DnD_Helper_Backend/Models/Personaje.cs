using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models
{
    public class Personaje
    {
        [Key]
        public int Personaje_ID { get; set; }
        public string? Nombre { get; set; }
        public int? Experiencia { get; set; }
        public int? Usuario_ID { get; set; }
        public bool Estatus { get; set; }

        public virtual Usuario? Usuario { get; set; }

        public virtual ClasePersonaje? ClasePersonaje { get; set; }
        public virtual RazaPersonaje? RazaPersonaje { get; set; }
    }
}
