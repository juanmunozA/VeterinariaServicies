using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class Medicamento
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;
    }
}
