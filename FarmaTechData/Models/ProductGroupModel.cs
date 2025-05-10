namespace FarmaTechData.Models;
public class ProductGroupModel
{
    public int Id { get; set; }
    public string Group { get; set; } = string.Empty;
    public ProductFileModel? GroupPhoto { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    public List<string> TargetPests { get; set; } = new List<string>();
    public string? SpecialInstructions { get; set; }
}
