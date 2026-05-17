namespace APBD_TASK7.model;


public class ComponentManufacturer
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = [];
}