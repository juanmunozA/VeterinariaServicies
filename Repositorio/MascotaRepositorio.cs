using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class MascotaRepositorio
    {
        private readonly VeterinariaContext _context;

        public MascotaRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mascota>> GetAllAsync()
        {
            return await _context.Mascotas
                .Include(m => m.Cliente)
                .Include(m => m.Raza)
                .ToListAsync();
        }

        public async Task<Mascota?> GetByIdAsync(int id)
        {
            return await _context.Mascotas
                .Include(m => m.Cliente)
                .Include(m => m.Raza)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Mascota> AddAsync(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();
            return mascota;
        }

        public async Task<Mascota?> UpdateAsync(Mascota mascota)
        {
            var existente = await _context.Mascotas.FindAsync(mascota.Id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(mascota);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null) return false;

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
