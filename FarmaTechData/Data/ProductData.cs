using FarmaTechData.Data.Models;
using FarmaTechData.Models;

namespace FarmaTechData.Data;
public class ProductData
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

}
