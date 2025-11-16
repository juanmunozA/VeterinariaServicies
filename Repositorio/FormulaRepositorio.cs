using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBContext;

namespace Veterinaria.Repositorio
{
    public class FormulaRepositorio
    {
        private readonly VeterinariaContext _context;

        public FormulaRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Formula>> GetAllAsync()
        {
            return await _context.Formulas
                .Include(f => f.Mascota)
                .ToListAsync();
        }

        public async Task<Formula?> GetByIdAsync(int id)
        {
            return await _context.Formulas
                .Include(f => f.Mascota)
                .FirstOrDefaultAsync(f => f.FormulaId == id);
        }

        public async Task<Formula> AddAsync(Formula formula)
        {
            _context.Formulas.Add(formula);
            await _context.SaveChangesAsync();
            return formula;
        }

        public async Task<Formula?> UpdateAsync(Formula formula)
        {
            var existente = await _context.Formulas.FindAsync(formula.FormulaId);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(formula);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var formula = await _context.Formulas.FindAsync(id);
            if (formula == null) return false;

            _context.Formulas.Remove(formula);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
