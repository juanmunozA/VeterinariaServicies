using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class MascotaServicio
    {
        private readonly MascotaRepositorio _repo;
        private readonly ClienteRepositorio _clienteRepositorio; // Add this field

        public MascotaServicio(MascotaRepositorio repo, ClienteRepositorio clienteRepositorio) // Inject ClienteRepositorio
        {
            _repo = repo;
            _clienteRepositorio = clienteRepositorio;
        }

        public Task<IEnumerable<Mascota>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Mascota?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Mascota> CreateAsync(Mascota mascota) => _repo.AddAsync(mascota);
        public Task<Mascota?> UpdateAsync(Mascota mascota) => _repo.UpdateAsync(mascota);
        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);

        public async Task<Cliente?> BuscarClientePorCedulaAsync(string cedula)
        {
            return await _clienteRepositorio.ObtenerPorCedulaAsync(cedula);
        }
    }
}
