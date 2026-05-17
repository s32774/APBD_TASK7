namespace APBD_TASK7.model;


public class Component
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int ComponentManufacturerId { get; set; }
    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

    public int ComponentTypeId { get; set; }
    public ComponentType ComponentType { get; set; } = null!;

    public ICollection<PcComponent> PcComponents { get; set; } = [];
}