using FarmaTechData.Models;

namespace FarmaTechData.Data;
public interface IProductData
{
    Task<int> CreateProductFileAsync(string fileType, string fileUrl, string blobPath, string? description);
    Task<int> CreateProductGroupAsync(ProductGroupModel productGroup);
    Task<int> CreateTargetPestAsync(string targetPest);
    Task<List<FormulationModel>> GetFormulationsAsync(int searchId, bool searchProduct = true);
    Task<ProductFileModel?> GetProductFileAsync(int productFileId);
    Task<List<ProductGroupModel>> GetProductGroupsAsync();
    Task<List<ProductModel>> GetProductsAsync(int productGroupId);
    Task<List<TargetPestModel>> GetTargetPestsAsync(int? productGroupId);
    Task<bool> UpdateProductFileAsync(ProductFileModel productFile);
    Task<bool> UpdateProductGroupAsync(ProductGroupModel productGroup);
}