using Microsoft.AspNetCore.Mvc;
using GestaoEstruturaOrganizacional.Core.Interfaces;
using GestaoEstruturaOrganizacional.Core.DTOs;

namespace GestaoEstruturaOrganizacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPositionDTO>>> GetAll()
        {
            var items = await _jobPositionService.GetAllJobPositionsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPositionDTO>> GetById(int id)
        {
            var item = await _jobPositionService.GetJobPositionByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<JobPositionDTO>> Create([FromBody] JobPositionDTO dto)
        {
            var created = await _jobPositionService.CreateJobPositionAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobPositionDTO>> Update(int id, [FromBody] JobPositionDTO dto)
        {
            var updated = await _jobPositionService.UpdateJobPositionAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _jobPositionService.DeleteJobPositionAsync(id);
            return NoContent();
        }
    }
}

