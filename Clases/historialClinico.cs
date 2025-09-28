using System;

namespace Veterinaria.Clases
{
    public class HistorialClinico
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int FormulaId { get; set; }
        public Formula? Formula { get; set; }

        public int VeterinarioId { get; set; }
        public Veterinario? Veterinario { get; set; }

        public string Observaciones { get; set; } = string.Empty;
    }
}
