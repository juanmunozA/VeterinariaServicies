using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBContext;

namespace Veterinaria.Repositorio
{
    public class RazaRepositorio
    {
        private readonly VeterinariaContext _context;

        public RazaRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Raza>> GetAllAsync()
        {
            return await _context.Razas.ToListAsync();
        }

        public async Task<Raza?> GetByIdAsync(int id)
        {
            return await _context.Razas
                .FirstOrDefaultAsync(r => r.RazaId == id);
        }

        public async Task<Raza> AddAsync(Raza raza)
        {
            _context.Razas.Add(raza);
            await _context.SaveChangesAsync();
            return raza;
        }

        public async Task<Raza?> UpdateAsync(Raza raza)
        {
            var existente = await _context.Razas.FindAsync(raza.RazaId);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(raza);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var raza = await _context.Razas.FindAsync(id);
            if (raza == null) return false;

            _context.Razas.Remove(raza);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
