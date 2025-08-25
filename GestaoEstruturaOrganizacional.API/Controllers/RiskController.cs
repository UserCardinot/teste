using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RiskController : ControllerBase
    {
        private readonly IRiskService _riskService;

        public RiskController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RiskDTO>>> GetAll()
        {
            var items = await _riskService.GetAllRisksAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RiskDTO>> GetById(int id)
        {
            var item = await _riskService.GetRiskByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<RiskDTO>> Create([FromBody] RiskDTO dto)
        {
            var created = await _riskService.CreateRiskAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RiskDTO>> Update(int id, [FromBody] RiskDTO dto)
        {
            var updated = await _riskService.UpdateRiskAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _riskService.DeleteRiskAsync(id);
            return NoContent();
        }
    }
}

