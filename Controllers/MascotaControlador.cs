using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotaControlador : ControllerBase
    {
        private readonly MascotaServicio _servicio;

        public MascotaControlador(MascotaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Mascota>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> GetById(int id)
        {
            var mascota = await _servicio.GetByIdAsync(id);
            return mascota == null ? NotFound() : Ok(mascota);
        }

        [HttpPost]
        public async Task<ActionResult<Mascota>> Create(Mascota mascota)
        {
            var creada = await _servicio.CreateAsync(mascota);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Mascota>> Update(int id, Mascota mascota)
        {
            if (id != mascota.Id) return BadRequest();
            var actualizada = await _servicio.UpdateAsync(mascota);
            return actualizada == null ? NotFound() : Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
