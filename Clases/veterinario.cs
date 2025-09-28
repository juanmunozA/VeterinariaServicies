using System.ComponentModel.DataAnnotations;

namespace Veterinaria.Clases
{
    public class Veterinario
    {
        public int Id { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;
    }
}
