namespace FarmaTechData.Data.Models;
public class FlatFormulationRow
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? ParentId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string ValueType { get; set; } = string.Empty;
    public string? StringValue { get; set; }
    public int SortOrder { get; set; }
}
