namespace Veterinaria.Clases
{
    public class Formula
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }
    }
}
