namespace FarmaTechData.Models;
public class ProductFileModel
{
    public int Id { get; set; }
    public string FileType { get; set; } = string.Empty;
    public string BlobPath { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string? Description { get; set; }
}
