using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class FormulaServicio
    {
        private readonly FormulaRepositorio _repo;

        public FormulaServicio(FormulaRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Formula>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Formula?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Formula> CreateAsync(Formula formula) => _repo.AddAsync(formula);
        public Task<Formula?> UpdateAsync(Formula formula) => _repo.UpdateAsync(formula);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
