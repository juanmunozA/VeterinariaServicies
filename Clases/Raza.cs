using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Raza
    {
       [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int RazaId { get; set; }

        
        public string NombreRaza { get; set; } = string.Empty;
        [Required]
        [JsonIgnore]
        public List<Mascota> Mascotas { get; set; } = new();
    }
}
