using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.DTOs;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialClinicoControlador : ControllerBase
    {
        private readonly HistorialServicio _servicio;

        public HistorialClinicoControlador(HistorialServicio servicio)
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
        public async Task<ActionResult<HistorialClinicoDTO>> Create(HistorialClinicoDTO dto)
        {
            var historial = new HistorialClinico
            {
                Fecha = dto.Fecha,
                MascotaId = dto.MascotaId,
                Observaciones = dto.Observaciones
            };

            var creado = await _servicio.CreateAsync(historial);

            var result = new HistorialClinicoDTO
            {
                Fecha = creado.Fecha,
                MascotaId = creado.MascotaId,
                Observaciones = creado.Observaciones
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, result);
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
