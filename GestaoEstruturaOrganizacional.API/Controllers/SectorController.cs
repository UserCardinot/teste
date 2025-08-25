using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;

        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectorDTO>>> GetAll()
        {
            var items = await _sectorService.GetAllSectorsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDTO>> GetById(int id)
        {
            var item = await _sectorService.GetSectorByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<SectorDTO>> Create([FromBody] SectorDTO dto)
        {
            var created = await _sectorService.CreateSectorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SectorDTO>> Update(int id, [FromBody] SectorDTO dto)
        {
            var updated = await _sectorService.UpdateSectorAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _sectorService.DeleteSectorAsync(id);
            return NoContent();
        }
    }
}

