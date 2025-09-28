namespace Veterinaria.Clases
{
    public class FormulaMedicamento
    {
        public int Id { get; set; }

        public int FormulaId { get; set; }
        public Formula? Formula { get; set; }

        public int MedicamentoId { get; set; }
        public Medicamento? Medicamento { get; set; }

        public int Cantidad { get; set; }
    }
}
