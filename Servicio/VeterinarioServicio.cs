using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class VeterinarioServicio
    {
        private readonly VeterinarioRepositorio _repo;

        public VeterinarioServicio(VeterinarioRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Veterinario>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Veterinario?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Veterinario> CreateAsync(Veterinario veterinario) => _repo.AddAsync(veterinario);
        public Task<Veterinario?> UpdateAsync(Veterinario veterinario) => _repo.UpdateAsync(veterinario);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
