using Microsoft.EntityFrameworkCore;
using Veterinaria.Clases;
using Veterinaria.DBcontext;

namespace Veterinaria.Repositorio
{
    public class MedicamentoRepositorio
    {
        private readonly VeterinariaContext _context;

        public MedicamentoRepositorio(VeterinariaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _context.Medicamentos.ToListAsync();
        }

        public async Task<Medicamento?> GetByIdAsync(int id)
        {
            return await _context.Medicamentos
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Medicamento> AddAsync(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
            return medicamento;
        }

        public async Task<Medicamento?> UpdateAsync(Medicamento medicamento)
        {
            var existente = await _context.Medicamentos.FindAsync(medicamento.Id);
            if (existente == null) return null;

            _context.Entry(existente).CurrentValues.SetValues(medicamento);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null) return false;

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
