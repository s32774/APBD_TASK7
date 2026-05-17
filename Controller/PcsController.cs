namespace APBD_TASK7.Controller;
using APBD_TASK7.DTOs;
using APBD_TASK7.Service;
using Microsoft.AspNetCore.Mvc;

    
    [ApiController]
    [Route("api/pcs")]
    public class PcsController : ControllerBase
    {
        private readonly IPcService _pcService;

        public PcsController(IPcService pcService)
        {
            _pcService = pcService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPcs()
        {
            var pcs = await _pcService.GetAllPcsAsync();
            return Ok(pcs);
        }

        [HttpGet("{id:int}/components")]
        public async Task<IActionResult> GetPcComponents(int id)
        {
            var components = await _pcService.GetPcComponentsAsync(id);

            if (components is null)
            {
                return NotFound($"PC with id {id} was not found.");
            }

            return Ok(components);
        }

        [HttpPost]
        public async Task<IActionResult> AddPc([FromBody] PcRequestDto request)
        {
            var pc = await _pcService.AddPcAsync(request);

            return CreatedAtAction(nameof(GetPcComponents), new { id = pc.Id }, pc);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePc(int id, [FromBody] PcRequestDto request)
        {
            var updated = await _pcService.UpdatePcAsync(id, request);

            if (!updated)
            {
                return NotFound($"PC with id {id} was not found.");
            }

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePc(int id)
        {
            var deleted = await _pcService.DeletePcAsync(id);

            if (!deleted)
            {
                return NotFound($"PC with id {id} was not found.");
            }

            return NoContent();
        }
    }
