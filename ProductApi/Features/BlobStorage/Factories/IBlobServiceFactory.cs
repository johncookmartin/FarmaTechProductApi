using ProductApi.Features.BlobStorage.Services;

namespace ProductApi.Features.BlobStorage.Factories;
public interface IBlobServiceFactory
{
    IBlobService Create(string containerName);
}