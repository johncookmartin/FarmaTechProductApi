
namespace ProductApi.Features.BlobStorage.Services;

public interface IBlobService
{
    Task<Stream> StreamAsync(string fileName);
    Task<(string BlobUrl, string BlobPath)> UploadAsync(string fileName, Stream fileStream);
}