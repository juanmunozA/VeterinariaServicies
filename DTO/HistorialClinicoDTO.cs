namespace Veterinaria.DTOs
{
    public class HistorialClinicoDTO
    {
        
        public int HistorialClinicoId { get; set; }    
        
        public string Codigo { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public int MascotaId { get; set; }

        public int ClienteId { get; set; }

        public int FormulaId { get; set; }

        public int VeterinarioId { get; set; }

        public string Observaciones { get; set; } = string.Empty;
    }
}
