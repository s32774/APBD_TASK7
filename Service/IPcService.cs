namespace APBD_TASK7.Service;
using APBD_TASK7.DTOs;

public interface IPcService
{
    Task<List<PcResponseDto>> GetAllPcsAsync();
    Task<List<ComponentResponseDto>> GetPcComponentsAsync(int id);
    Task<PcResponseDto> AddPcAsync(PcRequestDto request);
    Task UpdatePcAsync(int id, PcRequestDto request);
    Task DeletePcAsync(int id);
}