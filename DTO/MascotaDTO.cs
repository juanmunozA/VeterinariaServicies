namespace Veterinaria.DTOs
{
    public class MascotaDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public int ClienteId { get; set; }
        public int? RazaId { get; set; }
    }
}
