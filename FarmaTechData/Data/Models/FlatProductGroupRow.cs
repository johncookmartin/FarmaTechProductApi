namespace FarmaTechData.Data.Models;
public class FlatProductGroupRow
{
    public int Id { get; set; }
    public string Group { get; set; } = string.Empty;
    public int PhotoId { get; set; }
    public string? SpecialInstructions { get; set; }
}
