using Azure.Identity;
using Azure.Storage.Blobs;

namespace FarmaTechBlob;
public class BlobService : IBlobService
{
    private readonly BlobContainerClient _containerClient;
    public BlobService(string accountName, string connectionName)
    {
        var conainterUri = new Uri($"https://{accountName}.blob.core.windows.net/{connectionName}");
        _containerClient = new BlobContainerClient(conainterUri, new DefaultAzureCredential());
    }

    public async Task<(string BlobUrl, string BlobPath)> UploadAsync(string fileName, Stream fileStream)
    {
        var blob = _containerClient.GetBlobClient(fileName);
        await blob.UploadAsync(fileStream, overwrite: true);

        string blobUrl = blob.Uri.ToString();
        string blobPath = blob.Name;

        return (blobUrl, blobPath);
    }

    public async Task<Stream> StreamAsync(string fileName)
    {
        var blob = _containerClient.GetBlobClient(fileName);
        var response = await blob.DownloadAsync();
        return response.Value.Content;
    }
}
