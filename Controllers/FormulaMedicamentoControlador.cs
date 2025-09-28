using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialControlador : ControllerBase
    {
        private readonly HistorialServicio _servicio;

        public HistorialControlador(HistorialServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<HistorialClinico>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialClinico>> GetById(int id)
        {
            var historial = await _servicio.GetByIdAsync(id);
            return historial == null ? NotFound() : Ok(historial);
        }

        [HttpPost]
        public async Task<ActionResult<HistorialClinico>> Create(HistorialClinico historial)
        {
            var creado = await _servicio.CreateAsync(historial);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HistorialClinico>> Update(int id, HistorialClinico historial)
        {
            if (id != historial.Id) return BadRequest();
            var actualizado = await _servicio.UpdateAsync(historial);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
