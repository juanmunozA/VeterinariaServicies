using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class FormulaMedicamentoServicio
    {
        private readonly FormulaMedicamentoRepositorio _repo;

        public FormulaMedicamentoServicio(FormulaMedicamentoRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<FormulaMedicamento>> GetAllAsync() => _repo.GetAllAsync();
        public Task<FormulaMedicamento?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<FormulaMedicamento> CreateAsync(FormulaMedicamento fm) => _repo.AddAsync(fm);
        public Task<FormulaMedicamento?> UpdateAsync(FormulaMedicamento fm) => _repo.UpdateAsync(fm);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
