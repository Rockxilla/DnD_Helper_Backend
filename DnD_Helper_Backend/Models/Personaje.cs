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
        public short? Clase_ID { get; set; }
        public short? Raza_ID { get; set; }
        public bool Estatus { get; set; }
        public virtual Usuario? Usuario { get; set; }
        public virtual Clase? Clase { get; set; }
        public virtual Raza? Raza { get; set; }




    }
}
