using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class HistorialServicio
    {
        private readonly HistorialRepositorio _repo;

        public HistorialServicio(HistorialRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<HistorialClinico>> GetAllAsync() => _repo.GetAllAsync();
        public Task<HistorialClinico?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<HistorialClinico> CreateAsync(HistorialClinico historial) => _repo.AddAsync(historial);
        public Task<HistorialClinico?> UpdateAsync(HistorialClinico historial) => _repo.UpdateAsync(historial);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
