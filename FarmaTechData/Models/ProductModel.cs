namespace FarmaTechData.Models;
public class ProductModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductFileModel> Photos { get; set; } = new List<ProductFileModel>();
    public string Definition { get; set; } = string.Empty;
    public ProductFileModel? Sds { get; set; }
    public List<FormulationModel> Formulations { get; set; } = new List<FormulationModel>();
    public List<string> KeysToRemove { get; set; } = new List<string>();
}
