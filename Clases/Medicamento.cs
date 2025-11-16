using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Medicamento
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int MedicamentoId { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;
        
    }
}
