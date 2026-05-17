namespace APBD_TASK7.Service;
using APBD_TASK7.Data;
using APBD_TASK7.DTOs;
using APBD_TASK7.model;
using Microsoft.EntityFrameworkCore;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcResponseDto>> GetAllPcsAsync()
    {
        return await _context.Pcs
            .Select(p => new PcResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<List<ComponentResponseDto>?> GetPcComponentsAsync(int id)
    {
        var exists = await _context.Pcs.AnyAsync(p => p.Id == id);

        if (!exists)
        {
            return null;
        }

        return await _context.PcComponents
            .Where(pc => pc.PcId == id)
            .Select(pc => new ComponentResponseDto
            {
                Code = pc.Component.Code,
                Name = pc.Component.Name,
                Description = pc.Component.Description,
                Manufacturer = pc.Component.ComponentManufacturer.FullName,
                Type = pc.Component.ComponentType.Name,
                Amount = pc.Amount
            })
            .ToListAsync();
    }

    public async Task<PcResponseDto> AddPcAsync(PcRequestDto request)
    {
        var pc = new Pc
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> UpdatePcAsync(int id, PcRequestDto request)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc is null)
        {
            return false;
        }

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}