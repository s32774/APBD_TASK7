namespace APBD_TASK7.model;

public class PcComponent
{
    public int PcId { get; set; }
    public Pc Pc { get; set; } = null!;

    public string ComponentCode { get; set; } = string.Empty;
    public Component Component { get; set; } = null!;

    public int Amount { get; set; }
}