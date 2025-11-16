using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Veterinario
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int VeterinarioId { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;
    }
}
