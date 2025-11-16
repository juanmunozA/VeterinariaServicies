using System;
using System.Text.Json.Serialization;

namespace Veterinaria.Clases
{
    public class HistorialClinico
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HistorialClinicoId { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int MascotaId { get; set; }
        [JsonIgnore]
        public Mascota? Mascota { get; set; }

        public int ClienteId { get; set; }
        [JsonIgnore]
        public Cliente? Cliente { get; set; }

        public int FormulaId { get; set; }
        [JsonIgnore]
        public Formula? Formula { get; set; }

        public int VeterinarioId { get; set; }
        [JsonIgnore]
        public Veterinario? Veterinario { get; set; }

        public string Observaciones { get; set; } = string.Empty;
    }
}
