using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBContext;

namespace Veterinaria.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly VeterinariaContext _context;

        public ClienteRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public Cliente? ObtenerPorId(int id)
        {
            return _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(int id, Cliente cliente)
        {
            var clienteExistente = await _context.Clientes.FindAsync(id);
            if (clienteExistente == null)
            {
                return null;
            }

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Correo = cliente.Correo; // Cambiado de Email a Correo
            // Actualiza otros campos según sea necesario

            _context.Clientes.Update(clienteExistente);
            await _context.SaveChangesAsync();
            return clienteExistente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        // Nuevo: buscar cliente por correo (para login)
        public async Task<Cliente?> ObtenerPorCorreoAsync(string correo)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Correo == correo);
        }

        public async Task<Cliente?> ObtenerPorCedulaAsync(string cedula)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Cedula == cedula);
        }
    }
}
