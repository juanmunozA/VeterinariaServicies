using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Mascota
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MascotaId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public int Edad { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        // Relación con Raza
        public int? RazaId { get; set; }

        [JsonIgnore]
        public Raza? Raza { get; set; }
    }
}

