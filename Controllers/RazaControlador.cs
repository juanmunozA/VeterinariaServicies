using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RazaControlador : ControllerBase
    {
        private readonly RazaServicio _servicio;

        public RazaControlador(RazaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Raza>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Raza>> GetById(int id)
        {
            var raza = await _servicio.GetByIdAsync(id);
            return raza == null ? NotFound() : Ok(raza);
        }

        [HttpPost]
        public async Task<ActionResult<Raza>> Create(Raza raza)
        {
            var creada = await _servicio.CreateAsync(raza);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Raza>> Update(int id, Raza raza)
        {
            if (id != raza.Id) return BadRequest();
            var actualizada = await _servicio.UpdateAsync(raza);
            return actualizada == null ? NotFound() : Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
