using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EPIController : ControllerBase
    {
        private readonly IEPIService _epiService;

        public EPIController(IEPIService epiService)
        {
            _epiService = epiService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EPIDTO>>> GetAll()
        {
            var items = await _epiService.GetAllEPIsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EPIDTO>> GetById(int id)
        {
            var item = await _epiService.GetEPIByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<EPIDTO>> Create([FromBody] EPIDTO dto)
        {
            var created = await _epiService.CreateEPIAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EPIDTO>> Update(int id, [FromBody] EPIDTO dto)
        {
            var updated = await _epiService.UpdateEPIAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _epiService.DeleteEPIAsync(id);
            return NoContent();
        }
    }
}

