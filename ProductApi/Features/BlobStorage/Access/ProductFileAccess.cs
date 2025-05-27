using FarmaTechData.Data;
using ProductApi.Features.BlobStorage.Services;

namespace ProductApi.Features.BlobStorage.Access;

public class ProductFileAccess : BlobAccess, IProductFileAccess
{
    public ProductFileAccess(IBlobService blob, IProductData db) : base(blob, db)
    {
    }
}
