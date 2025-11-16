using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;
using Veterinaria.DTOs;

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
        public async Task<ActionResult<MascotaDTO>> Create(MascotaDTO dto)
        {
            // Buscar el cliente por cédula
            var cliente = await _servicio.BuscarClientePorCedulaAsync(dto.CedulaCliente);
            if (cliente == null)
            {
                return NotFound(new { mensaje = "Cliente no encontrado" });
            }

            // Crear la mascota asociada al cliente
            var mascota = new Mascota
            {   MascotaId = dto.MascotaId,
                Nombre = dto.Nombre,
                Edad = dto.Edad,
                ClienteId = cliente.ClienteId, 
                RazaId = dto.RazaId
            };

            var creado = await _servicio.CreateAsync(mascota);

            var result = new MascotaDTO
            {   MascotaId = creado.MascotaId,
                Nombre = creado.Nombre,
                Edad = creado.Edad,
                ClienteId = null,
                RazaId = creado.RazaId
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.MascotaId }, result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Mascota>> Update(int id, Mascota mascota)
        {
            if (id != mascota.MascotaId) return BadRequest();
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
