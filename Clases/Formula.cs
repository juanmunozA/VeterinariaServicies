using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class Formula
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }
    }
}



