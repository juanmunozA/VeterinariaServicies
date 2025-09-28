using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class RazaServicio
    {
        private readonly RazaRepositorio _repo;

        public RazaServicio(RazaRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Raza>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Raza?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Raza> CreateAsync(Raza raza) => _repo.AddAsync(raza);
        public Task<Raza?> UpdateAsync(Raza raza) => _repo.UpdateAsync(raza);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
