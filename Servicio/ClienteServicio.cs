using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class ClienteServicio
    {
        private readonly ClienteRepositorio _repo;

        public ClienteServicio(ClienteRepositorio repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Cliente>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Cliente?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Cliente> CreateAsync(Cliente cliente) => _repo.AddAsync(cliente);
        public Task<Cliente?> UpdateAsync(Cliente cliente) => _repo.UpdateAsync(cliente);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
