using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;
using Veterinaria.DTOs;

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
        public async Task<IEnumerable<RazaDTO>> GetAll()
        {
            var razas = await _servicio.GetAllAsync();
            return razas.Select(r => new RazaDTO
            {
                RazaId = r.RazaId,
                Nombre = r.Nombre
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RazaDTO>> GetById(int id)
        {
            var raza = await _servicio.GetByIdAsync(id);
            if (raza == null) return NotFound();

            var dto = new RazaDTO
            {
                RazaId = raza.RazaId,
                Nombre = raza.Nombre
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<RazaDTO>> Create(RazaDTO dto)
        {
            var raza = new Raza
            {   Raza = dto.RazaId
                Nombre = dto.Nombre
            };

            var creada = await _servicio.CreateAsync(raza);

            var result = new RazaDTO
            {
                RazaId = creada.RazaId,
                Nombre = creada.Nombre
            };

            return CreatedAtAction(nameof(GetById), new { id = creada.RazaId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RazaDTO>> Update(int id, RazaDTO dto)
        {
            if (id != dto.RazaId) return BadRequest();

            var raza = new Raza
            {
                RazaId = id,
                Nombre = dto.Nombre
            };

            var actualizada = await _servicio.UpdateAsync(raza);
            if (actualizada == null) return NotFound();

            var result = new RazaDTO
            {
                RazaId = actualizada.RazaId,
                Nombre = actualizada.Nombre
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
