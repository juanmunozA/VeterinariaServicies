using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class Mascota
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public int Edad { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int? RazaId { get; set; }
        public Raza? Raza { get; set; }
    }
}

