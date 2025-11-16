namespace Veterinaria.DTOs
{
    public class ClienteDTO
    {
        public int? ClienteId { get; set; } // Agregar clave para que el front reciba/mande el id cuando haga PUT/DELETE

        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
