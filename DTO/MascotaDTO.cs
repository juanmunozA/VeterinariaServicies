using System.Text.Json.Serialization;

namespace Veterinaria.DTOs
{
    public class MascotaDTO
    {   public int MascotaId { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ClienteId { get; set; }
        public int? RazaId { get; set; }
        public string CedulaCliente { get; set; }
    }
}
