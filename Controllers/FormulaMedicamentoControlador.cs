using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.DTOs;
using Veterinaria.Servicio;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulaMedicamentoControlador : ControllerBase
    {
        private readonly FormulaMedicamentoServicio _servicio;

        public FormulaMedicamentoControlador(FormulaMedicamentoServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<FormulaMedicamento>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<FormulaMedicamento>> GetById(int id)
        {
            var fm = await _servicio.GetByIdAsync(id);
            return fm == null ? NotFound() : Ok(fm);
        }

        [HttpPost]
        public async Task<ActionResult<FormulaMedicamentoDTO>> Create(FormulaMedicamentoDTO dto)
        {
            var fm = new FormulaMedicamento
            {   ForMedicamentoId = dto.ForMedicamentoId,
                FormulaId = dto.FormulaId,
                MedicamentoId = dto.MedicamentoId,
                Cantidad = dto.Cantidad
            };

            var creado = await _servicio.CreateAsync(fm);

            var result = new FormulaMedicamentoDTO
            {   ForMedicamentoId = creado.ForMedicamentoId,
                FormulaId = creado.FormulaId,
                MedicamentoId = creado.MedicamentoId,
                Cantidad = creado.Cantidad
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.ForMedicamentoId }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FormulaMedicamentoDTO>> Update(int id, FormulaMedicamentoDTO dto)
        {
            var existente = await _servicio.GetByIdAsync(id);
            if (existente == null) return NotFound();
            existente.ForMedicamentoId = dto.ForMedicamentoId;
            existente.FormulaId = dto.FormulaId;
            existente.MedicamentoId = dto.MedicamentoId;
            existente.Cantidad = dto.Cantidad;

            var actualizado = await _servicio.UpdateAsync(existente);

            var result = new FormulaMedicamentoDTO
            {   ForMedicamentoId = actualizado.ForMedicamentoId, 
                FormulaId = actualizado!.FormulaId,
                MedicamentoId = actualizado.MedicamentoId,
                Cantidad = actualizado.Cantidad
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _servicio.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
