namespace FarmaTechData.Models;
public class FormulationModel
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public ValueTypeEnum ValueType { get; set; }
    public string? StringValue { get; set; }
    public List<string>? ArrayValues { get; set; }
    public List<FormulationModel>? ObjectValues { get; set; }
    public int SortOrder { get; set; }
}

public enum ValueTypeEnum
{
    String,
    Array,
    Object
}
