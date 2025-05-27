using FarmaTechData.Data;
using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Exceptions;
using ProductApi.Features.BlobStorage.Services;

namespace ProductApi.Features.BlobStorage.Access;

public class BlobAccess
{
    private readonly IBlobService _blob;
    private readonly IProductData _db;

    public BlobAccess(IBlobService blob, IProductData db)
    {
        _blob = blob;
        _db = db;
    }

    public async Task<FileStreamResult> DownloadFileByName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ApiException("File name cannot be null or empty", 400);
        }
        try
        {
            string safeName = Uri.UnescapeDataString(fileName);

            var stream = await _blob.StreamAsync(safeName);
            var result = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = Path.GetFileName(safeName)
            };
            return result;
        }
        catch (Exception ex)
        {
            throw new ApiException($"File not found: {ex.Message}", 404);
        }
    }

    public async Task<ProductFileModel?> DownloadFileById(int id)
    {
        var productFile = await _db.GetProductFileAsync(id);
        return productFile;
    }

    public async Task<int?> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ApiException("No file uploaded", 400);
        }

        string blobUrl;
        string blobPath;

        try
        {
            await using var stream = file.OpenReadStream();
            (blobUrl, blobPath) = await _blob.UploadAsync(file.FileName, stream);
        }
        catch (Exception ex)
        {
            throw new ApiException($"Internal server error: {ex.Message}", 500);
        }

        int? fileId = await CreateFileRecord(file.ContentType, blobUrl, blobPath);
        return fileId;

    }

    private async Task<int?> CreateFileRecord(string fileType, string blobUrl, string blobPath, string? description = null)
    {
        int? fileId = await _db.CreateProductFileAsync(fileType, blobUrl, blobPath, description);

        return fileId;
    }
}
