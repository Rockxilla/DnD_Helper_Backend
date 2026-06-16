using System.ComponentModel.DataAnnotations;

namespace DnD_Helper_Backend.Models
{
    public class Usuario
    {
        [Key]
        public int Usuario_ID       { get; set; }
        public string? Nombre        { get; set; }
        public string? Contraseña    { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public bool Estatus { get; set; }
        public virtual ICollection<Personaje> Personajes { get; set; } = new List<Personaje>();
    }
}
