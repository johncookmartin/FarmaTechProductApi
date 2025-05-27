using ProductApi.Features.BlobStorage.Services;

namespace ProductApi.Features.BlobStorage.Factories;

public class BlobServiceFactory : IBlobServiceFactory
{
    private readonly IConfiguration _config;

    public BlobServiceFactory(IConfiguration config)
    {
        _config = config;
    }

    public IBlobService Create(string containerName)
    {
        var accountName = _config["BlobStorage:AccountName"];
        if (string.IsNullOrEmpty(accountName))
        {
            throw new ArgumentException("BlobStorage:AccountName configuration is missing.");
        }

        return new BlobService(accountName, containerName);
    }
}
