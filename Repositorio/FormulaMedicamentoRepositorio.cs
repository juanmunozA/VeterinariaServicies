using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class FormulaMedicamentoRepositorio
    {
        private readonly VeterinariaContext _context;

        public FormulaMedicamentoRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormulaMedicamento>> GetAllAsync()
        {
      
            return await _context.FormulaMedicamentos
                .Include(fm => fm.Formula)
                .Include(fm => fm.Medicamento)
                .ToListAsync();
        }

        public async Task<FormulaMedicamento?> GetByIdAsync(int id)
        {
            return await _context.FormulaMedicamentos
                .Include(fm => fm.Formula)
                .Include(fm => fm.Medicamento)
                .FirstOrDefaultAsync(fm => fm.Id == id);
        }

        public async Task<FormulaMedicamento> AddAsync(FormulaMedicamento fm)
        {
            _context.FormulaMedicamentos.Add(fm);
            await _context.SaveChangesAsync();
            return fm;
        }

        public async Task<FormulaMedicamento?> UpdateAsync(FormulaMedicamento fm)
        {
            var existente = await _context.FormulaMedicamentos.FindAsync(fm.Id);
            if (existente == null)
                return null;

            _context.Entry(existente).CurrentValues.SetValues(fm);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fm = await _context.FormulaMedicamentos.FindAsync(id);
            if (fm == null)
                return false;

            _context.FormulaMedicamentos.Remove(fm);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
