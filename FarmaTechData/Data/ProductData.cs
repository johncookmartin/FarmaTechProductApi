using FarmaTechData.Data.Models;
using FarmaTechData.Models;

namespace FarmaTechData.Data;
public class ProductData : IProductData
{
    private readonly ISqlDataAccess _db;

    public ProductData(ISqlDataAccess db)
    {
        _db = db;
    }

    public async Task<List<FormulationModel>> GetFormulationsAsync(int searchId, bool searchProduct = true)
    {
        var flatFormulations = (await _db.QueryDataAsync<FlatFormulationRow, dynamic>("stp_Formulation_SelectAll", new { SearchId = searchId, SearchProduct = searchProduct })).ToList();

        var formulationDict = flatFormulations.ToDictionary(x => x.Id, x => new FormulationModel
        {
            Id = x.Id,
            Key = x.Key,
            ValueType = Enum.Parse<ValueTypeEnum>(x.ValueType),
            StringValue = x.StringValue,
            SortOrder = x.SortOrder
        });

        foreach (var formulation in formulationDict.Values)
        {
            if (formulation.ValueType == ValueTypeEnum.Array)
            {
                formulationDict[formulation.Id].ArrayValues = await GetFormulationArray(formulation.Id);
            }
        }

        foreach (var formulation in flatFormulations)
        {
            if (formulation.ParentId.HasValue)
            {
                formulationDict[formulation.Id].ObjectValues = await GetFormulationsAsync(formulation.ParentId.Value, false);
            }
        }

        List<FormulationModel> result = formulationDict.Values.OrderBy(x => x.SortOrder).ToList();
        return result;
    }

    private async Task<List<string>> GetFormulationArray(int formulationId)
    {
        var arrayValues = (await _db.QueryDataAsync<string, dynamic>("stp_FormulationArray_SelectAll",
                                                                     new { FormulationId = formulationId })).ToList();
        return arrayValues;
    }

    public async Task<List<ProductModel>> GetProductsAsync(int productGroupId)
    {
        var flatProducts = (await _db.QueryDataAsync<FlatProductRow, dynamic>("stp_Product_SelectAll",
                                                                              new { ProductGroupId = productGroupId })).ToList();

        var productTasks = flatProducts.Select(async x =>
        {
            var model = new ProductModel
            {
                Id = x.Id,
                Name = x.Name,
                Photos = await GetProductPhotosAsync(x.Id),
                Definition = x.Definition,
                Sds = await GetProductFileAsync(x.SdsId),
                Formulations = await GetFormulationsAsync(x.Id),
                KeysToRemove = await GetKeysToRemove(x.Id),

            };
            return model;
        });

        return (await Task.WhenAll(productTasks)).ToList();
    }

    private async Task<List<string>> GetKeysToRemove(int productId)
    {
        var keysToRemove = (await _db.QueryDataAsync<string, dynamic>("stp_FormulationKey_SelectAll", new { ProductId = productId })).ToList();
        return keysToRemove;
    }

    private async Task<List<ProductFileModel>> GetProductPhotosAsync(int productId)
    {
        var productPhotos = (await _db.QueryDataAsync<ProductFileModel, dynamic>("stp_ProductFile_SelectAllPhotos", new { ProductId = productId })).ToList();
        return productPhotos;
    }

    public async Task<ProductFileModel?> GetProductFileAsync(int productFileId)
    {
        var productFile = (await _db.QueryDataAsync<ProductFileModel, dynamic>("stp_ProductFile_Select", new { ProductFileId = productFileId })).FirstOrDefault();
        return productFile;
    }

    public async Task<List<ProductGroupModel>> GetProductGroupsAsync()
    {
        var flatProductGroups = (await _db.QueryDataAsync<FlatProductGroupRow, dynamic>("stp_ProductGroup_SelectAll", new { })).ToList();

        var productGroupTasks = flatProductGroups.Select(async x =>
        {
            var model = new ProductGroupModel
            {
                Id = x.Id,
                Group = x.Group,
                GroupPhoto = await GetProductFileAsync(x.PhotoId),
                Products = await GetProductsAsync(x.Id),
                TargetPests = await GetTargetPestsAsync(x.Id),
                SpecialInstructions = x.SpecialInstructions
            };
            return model;
        });

        return (await Task.WhenAll(productGroupTasks)).ToList();
    }

    public async Task<int> CreateProductGroupAsync(ProductGroupModel productGroup)
    {
        var productGroupId = (await _db.QueryDataAsync<int, dynamic>("stp_ProductGroup_Insert",
                                                                    new
                                                                    {
                                                                        Group = productGroup.Group,
                                                                        SpecialInstructions = productGroup.SpecialInstructions,
                                                                        PhotoId = productGroup.GroupPhoto?.Id,
                                                                    })).First();

        if (productGroupId != -1)
        {
            foreach (var targetPest in productGroup.TargetPests)
            {
                await JoinProductGroupTargetPest(productGroupId, targetPest.Id);
            }
        }

        return productGroupId;
    }

    public async Task<bool> UpdateProductGroupAsync(ProductGroupModel productGroup)
    {
        int rowsAffected = (await _db.QueryDataAsync<int, dynamic>("stp_ProductGroup_Update",
            new
            {
                Id = productGroup.Id,
                Group = productGroup.Group,
                SpecialInstructions = productGroup.SpecialInstructions,
                PhotoId = productGroup.GroupPhoto?.Id
            })).First();
        if (rowsAffected == 1)
        {

            await _db.ExecuteDataAsync("stp_ProductGroupTargetPest_DeleteAll", new { ProductGroupId = productGroup.Id });

            foreach (var targetPest in productGroup.TargetPests)
            {
                await JoinProductGroupTargetPest(productGroup.Id, targetPest.Id);
            }
        }
        return rowsAffected == 1;
    }

    private async Task JoinProductGroupTargetPest(int productGroupId, int targetPestId)
    {
        await _db.ExecuteDataAsync("stp_ProductGroupTargetPest_Insert", new { ProductGroupId = productGroupId, TargetPestId = targetPestId });
    }

    public async Task<int> CreateProductFileAsync(string fileType, string fileUrl, string blobPath, string? description)
    {
        int productFileId = (await _db.QueryDataAsync<int, dynamic>("stp_ProductFile_Insert",
                                               new
                                               {
                                                   FileType = fileType,
                                                   FileUrl = fileUrl,
                                                   BlobPath = blobPath,
                                                   Description = description
                                               })).FirstOrDefault();
        return productFileId;
    }

    public async Task<bool> UpdateProductFileAsync(ProductFileModel productFile)
    {
        int rowsAffected = (await _db.QueryDataAsync<int, dynamic>("stp_ProductFile_Update",
            new
            {
                Id = productFile.Id,
                FileType = productFile.FileType,
                FileUrl = productFile.FileUrl,
                BlobPath = productFile.BlobPath,
                Description = productFile.Description
            })).First();

        return rowsAffected == 1;
    }

    public async Task<List<TargetPestModel>> GetTargetPestsAsync(int? productGroupId)
    {
        var targetPests = (await _db.QueryDataAsync<TargetPestModel, dynamic>("stp_TargetPest_SelectAll", new { ProductGroupId = productGroupId })).ToList();
        return targetPests;
    }

    public async Task<int> CreateTargetPestAsync(string targetPest)
    {
        var targetPestId = (await _db.QueryDataAsync<int, dynamic>("stp_TargetPest_Insert",
            new { TargetPest = targetPest })).First();
        return targetPestId;
    }
}
