namespace FarmaTechData.Models;
public class ProductGroupModel
{
    public int Id { get; set; }
    public string Group { get; set; } = string.Empty;
    public ProductFileModel? GroupPhoto { get; set; }
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    public List<TargetPestModel> TargetPests { get; set; } = new List<TargetPestModel>();
    public string? SpecialInstructions { get; set; }
}
