using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class HistorialRepositorio
    {
        private readonly VeterinariaContext _context;

        public HistorialRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistorialClinico>> GetAllAsync()
        {
            return await _context.HistorialesClinicos
                .Include(h => h.Mascota)
                .Include(h => h.Cliente)
                .Include(h => h.Formula)
                .Include(h => h.Veterinario)
                .ToListAsync();
        }

        public async Task<HistorialClinico?> GetByIdAsync(int id)
        {
            return await _context.HistorialesClinicos
                .Include(h => h.Mascota)
                .Include(h => h.Cliente)
                .Include(h => h.Formula)
                .Include(h => h.Veterinario)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HistorialClinico> AddAsync(HistorialClinico historial)
        {
            _context.HistorialesClinicos.Add(historial);
            await _context.SaveChangesAsync();
            return historial;
        }

        public async Task<HistorialClinico?> UpdateAsync(HistorialClinico historial)
        {
            var existente = await _context.HistorialesClinicos.FindAsync(historial.Id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(historial);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var historial = await _context.HistorialesClinicos.FindAsync(id);
            if (historial == null) return false;

            _context.HistorialesClinicos.Remove(historial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
