using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Veterinaria.Clases;
using Veterinaria.Repositorio;

namespace Veterinaria.Servicio
{
    public class ClienteServicio
    {
        private readonly ClienteRepositorio _clienteRepositorio;

        public ClienteServicio(ClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _clienteRepositorio.GetAllAsync();
        }

        // Obtener un cliente por ID
        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _clienteRepositorio.GetByIdAsync(id);
        }

        // Crear un nuevo cliente
        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            return await _clienteRepositorio.CreateAsync(cliente);
        }

        // Actualizar un cliente existente
        public async Task<Cliente?> UpdateAsync(int id, Cliente cliente)
        {
            return await _clienteRepositorio.UpdateAsync(id, cliente);
        }

        // Eliminar un cliente por ID
        public async Task<bool> DeleteAsync(int id)
        {
            return await _clienteRepositorio.DeleteAsync(id);
        }

        public Cliente? ObtenerPorId(int id)
        {
            return _clienteRepositorio.ObtenerPorId(id);
        }

        // Nuevo: exponer búsqueda por correo
        public async Task<Cliente?> ObtenerPorCorreoAsync(string correo)
        {
            return await _clienteRepositorio.ObtenerPorCorreoAsync(correo);
        }

        // Nuevo: autenticación simple: email + password=cédula
        public async Task<Cliente?> AuthenticateAsync(string correo, string password)
        {
            var cliente = await ObtenerPorCorreoAsync(correo);
            if (cliente == null) return null;
            // contraseña = cedula (como pediste)
            return cliente.Cedula == password ? cliente : null;
        }
    }
}
