using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Veterinaria.Clases;
using Veterinaria.Servicio;
using Veterinaria.DTOs;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteControlador : ControllerBase
    {
        private readonly ClienteServicio _clienteServicio;

        public ClienteControlador(ClienteServicio clienteServicio)
        {
            _clienteServicio = clienteServicio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteServicio.GetAllAsync();
            var dtos = clientes.Select(c => new ClienteDTO
            {
                ClienteId = c.ClienteId,
                Nombre = c.Nombre,
                Cedula = c.Cedula,
                Correo = c.Correo
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteServicio.GetByIdAsync(id);
            if (cliente == null) return NotFound();

            var dto = new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nombre = cliente.Nombre,
                Cedula = cliente.Cedula,
                Correo = cliente.Correo
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDTO dto)
        {
            var cliente = new Cliente
            {   
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                Correo = dto.Correo
            };

            var nuevoCliente = await _clienteServicio.CreateAsync(cliente);

            var result = new ClienteDTO
            {   ClienteId = nuevoCliente.ClienteId,
                Nombre = nuevoCliente.Nombre,
                Cedula = nuevoCliente.Cedula,
                Correo = nuevoCliente.Correo
            };

            return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.ClienteId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDTO dto)
        {
            var cliente = new Cliente
            {
                ClienteId = id,
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                Correo = dto.Correo
            };

            var actualizado = await _clienteServicio.UpdateAsync(id, cliente);
            if (actualizado == null) return NotFound();

            var result = new ClienteDTO
            {
                ClienteId = actualizado.ClienteId,
                Nombre = actualizado.Nombre,
                Cedula = actualizado.Cedula,
                Correo = actualizado.Correo
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _clienteServicio.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
