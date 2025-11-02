using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Mascota
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public int Edad { get; set; }

        public int ClienteId { get; set; }

        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        public int?  RazaId { get; set; }
        [JsonIgnore]
        public Raza? Raza { get; set; }
    }
}

