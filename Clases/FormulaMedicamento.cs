using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class FormulaMedicamento
    {
        [Key] 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int ForMedicamentoId { get; set; }

        
        public int FormulaId { get; set; }

        [JsonIgnore]
        public Formula? Formula { get; set; }

        public int MedicamentoId { get; set; }

        [JsonIgnore]
        public Medicamento? Medicamento { get; set; }

        public int Cantidad { get; set; }
    }
}
