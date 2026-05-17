namespace APBD_TASK7.Controller;
using APBD_TASK7.DTOs;
using APBD_TASK7.Service;
using Microsoft.AspNetCore.Mvc;
using APBD_TASK7.Exceptions;

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
        try
        {
            var components = await _pcService.GetPcComponentsAsync(id);
            return Ok(components);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
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
        try
        {
            await _pcService.UpdatePcAsync(id, request);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        try
        {
            await _pcService.DeletePcAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}