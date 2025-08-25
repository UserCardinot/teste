using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetAll()
        {
            var items = await _unitService.GetAllUnitsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDTO>> GetById(int id)
        {
            var item = await _unitService.GetUnitByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<UnitDTO>> Create([FromBody] UnitDTO dto)
        {
            var created = await _unitService.CreateUnitAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UnitDTO>> Update(int id, [FromBody] UnitDTO dto)
        {
            var updated = await _unitService.UpdateUnitAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _unitService.DeleteUnitAsync(id);
            return NoContent();
        }
    }
}

