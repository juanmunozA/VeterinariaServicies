using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;
using Veterinaria.DTOs;

namespace Veterinaria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulaControlador : ControllerBase
    {
        private readonly FormulaServicio _servicio;

        public FormulaControlador(FormulaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IEnumerable<Formula>> GetAll() => await _servicio.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Formula>> GetById(int id)
        {
            var formula = await _servicio.GetByIdAsync(id);
            return formula == null ? NotFound() : Ok(formula);
        }

        [HttpPost]
        public async Task<ActionResult<FormulaDTO>> Create(FormulaDTO dto)
        {
            var formula = new Formula
            {
                Codigo = dto.Codigo,
                MascotaId = dto.MascotaId
            };

            var creado = await _servicio.CreateAsync(formula);

            var result = new FormulaDTO
            {
                    
                Codigo = creado.Codigo,
                MascotaId = creado.MascotaId
            };

            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Formula>> Update(int id, Formula formula)
        {
            if (id != formula.Id) return BadRequest();
            var actualizada = await _servicio.UpdateAsync(formula);
            return actualizada == null ? NotFound() : Ok(actualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _servicio.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}
    