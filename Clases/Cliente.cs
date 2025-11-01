using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Cliente
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required]
        
        public string Nombre { get; set; } = string.Empty;

        [Required]
        
        public string Cedula { get; set; } = string.Empty;

        [EmailAddress]
       
        public string Correo { get; set; } = string.Empty;


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
