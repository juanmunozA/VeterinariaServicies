using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.DTOs;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoControlador : ControllerBase
    {
        private readonly MedicamentoServicio _servicio;

        public MedicamentoControlador(MedicamentoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Medicamento>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetById(int id)
        {
            var medicamento = await _servicio.GetByIdAsync(id);
            return medicamento == null ? NotFound() : Ok(medicamento);
        }

        [HttpPost]
        public async Task<ActionResult<MedicamentoDTO>> Create(MedicamentoDTO dto)
        {
            var medicamento = new Medicamento
            {
                Nombre = dto.Nombre
            };

            var creado = await _servicio.CreateAsync(medicamento);

            var result = new MedicamentoDTO
            {
                Nombre = creado.Nombre
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medicamento>> Update(int id, Medicamento medicamento)
        {
            if (id != medicamento.Id) return BadRequest();
            var actualizado = await _servicio.UpdateAsync(medicamento);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
