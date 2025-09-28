using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteControlador : ControllerBase
    {
        private readonly ClienteServicio _servicio;

        public ClienteControlador(ClienteServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Cliente>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            var cliente = await _servicio.GetByIdAsync(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Create(Cliente cliente)
        {
            var creado = await _servicio.CreateAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> Update(int id, Cliente cliente)
        {
            if (id != cliente.Id) return BadRequest();
            var actualizado = await _servicio.UpdateAsync(cliente);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
