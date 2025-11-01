using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class VeterinarioRepositorio
    {
        private readonly VeterinariaContext _context;

        public VeterinarioRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veterinario>> GetAllAsync()
        {
            return await _context.Veterinarios.ToListAsync();
        }

        public async Task<Veterinario?> GetByIdAsync(int id)
        {
            return await _context.Veterinarios
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Veterinario> AddAsync(Veterinario veterinario)
        {
            _context.Veterinarios.Add(veterinario);
            await _context.SaveChangesAsync();
            return veterinario;
        }

        public async Task<Veterinario?> UpdateAsync(Veterinario veterinario)
        {
            var existente = await _context.Veterinarios.FindAsync(veterinario.Id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(veterinario);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var veterinario = await _context.Veterinarios.FindAsync(id);
            if (veterinario == null) return false;

            _context.Veterinarios.Remove(veterinario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
