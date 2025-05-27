using FarmaTechData.Data;
using ProductApi.Features.BlobStorage.Services;

namespace ProductApi.Features.BlobStorage.Access;

public class FlyFileAccess : BlobAccess, IFlyFileAccess
{
    public FlyFileAccess(IBlobService blob, IProductData db) : base(blob, db)
    {
    }
}
