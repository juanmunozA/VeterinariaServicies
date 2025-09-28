using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class Raza
    {
        public int Id { get; set; }

        [Required]
        public string NombreRaza { get; set; } = string.Empty;

        public List<Mascota> Mascotas { get; set; } = new();
    }
}
