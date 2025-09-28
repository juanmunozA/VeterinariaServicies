using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class MedicamentoServicio
    {
        private readonly MedicamentoRepositorio _repo;

        public MedicamentoServicio(MedicamentoRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Medicamento>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Medicamento?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Medicamento> CreateAsync(Medicamento medicamento) => _repo.AddAsync(medicamento);
        public Task<Medicamento?> UpdateAsync(Medicamento medicamento) => _repo.UpdateAsync(medicamento);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
