using Microsoft.AspNetCore.Mvc;
using Veterinaria.Clases;
using Veterinaria.Servicio;

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
        public async Task<ActionResult<Formula>> Create(Formula formula)
        {
            var creada = await _servicio.CreateAsync(formula);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
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
    