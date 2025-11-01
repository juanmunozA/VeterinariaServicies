using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class FormulaMedicamento
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        public int FormulaId { get; set; }
        [JsonIgnore]
        public Formula? Formula { get; set; }

        public int MedicamentoId { get; set; }
        [JsonIgnore]
        public Medicamento? Medicamento { get; set; }

        public int Cantidad { get; set; }
    }
}
