using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinarioControlador : ControllerBase
    {
        private readonly VeterinarioServicio _servicio;

        public VeterinarioControlador(VeterinarioServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Veterinario>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinario>> GetById(int id)
        {
            var veterinario = await _servicio.GetByIdAsync(id);
            return veterinario == null ? NotFound() : Ok(veterinario);
        }

        [HttpPost]
        public async Task<ActionResult<Veterinario>> Create(Veterinario veterinario)
        {
            var creado = await _servicio.CreateAsync(veterinario);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Veterinario>> Update(int id, Veterinario veterinario)
        {
            if (id != veterinario.Id) return BadRequest();
            var actualizado = await _servicio.UpdateAsync(veterinario);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
