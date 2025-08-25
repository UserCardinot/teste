using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaDTO>>> GetAll()
        {
            var items = await _areaService.GetAllAreasAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AreaDTO>> GetById(int id)
        {
            var item = await _areaService.GetAreaByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<AreaDTO>> Create([FromBody] AreaDTO dto)
        {
            var created = await _areaService.CreateAreaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AreaDTO>> Update(int id, [FromBody] AreaDTO dto)
        {
            var updated = await _areaService.UpdateAreaAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _areaService.DeleteAreaAsync(id);
            return NoContent();
        }
    }
}

