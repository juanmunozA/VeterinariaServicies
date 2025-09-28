using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Cedula { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public List<Mascota> Mascotas { get; set; } = new();
    }


}

