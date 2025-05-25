using FarmaTechData.Models;

namespace FarmaTechData.Data;
public interface IProductData
{
    Task<List<FormulationModel>> GetFormulationsAsync(int searchId, bool searchProduct = true);
    Task<ProductFileModel?> GetProductFileAsync(int productFileId);
    Task<List<ProductGroupModel>> GetProductGroupsAsync();
    Task<List<ProductModel>> GetProductsAsync(int productGroupId);
}