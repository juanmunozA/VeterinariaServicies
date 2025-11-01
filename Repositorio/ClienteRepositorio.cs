using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class ClienteRepositorio
    {
        private readonly VeterinariaContext _context;

        public ClienteRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            // Obtiene todos los clientes junto con sus mascotas
            return await _context.Clientes
                .Include(c => c.Mascotas)
                .ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            // Busca cliente con sus mascotas
            return await _context.Clientes
                .Include(c => c.Mascotas)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(Cliente cliente)
        {
            var existente = await _context.Clientes.FindAsync(cliente.Id);
            if (existente == null)
                return null;

            // Actualiza los valores existentes con los nuevos
            _context.Entry(existente).CurrentValues.SetValues(cliente);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
