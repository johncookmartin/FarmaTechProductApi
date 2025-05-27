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

    public async Task<int?> UploadFile(IFormFile file)
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

    public async Task UpdateFile(ProductFileModel productFile)
    {
        if (productFile == null || productFile.Id <= 0)
        {
            throw new ApiException("Invalid product file data", 400);
        }

        var existingFile = await _db.GetProductFileAsync(productFile.Id);
        if (existingFile == null)
        {
            throw new ApiException("Product file not found", 404);
        }

        if (!string.IsNullOrWhiteSpace(productFile.FileUrl)) existingFile.FileUrl = productFile.FileUrl;
        if (!string.IsNullOrWhiteSpace(productFile.BlobPath)) existingFile.BlobPath = productFile.BlobPath;
        if (!string.IsNullOrWhiteSpace(productFile.Description)) existingFile.Description = productFile.Description;
        if (!string.IsNullOrWhiteSpace(productFile.FileType)) existingFile.FileType = productFile.FileType;

        bool success = await _db.UpdateProductFileAsync(existingFile);
        if (!success)
        {
            throw new ApiException("Failed to update product file", 500);
        }
    }
    private async Task<int> CreateFileRecord(string fileType, string blobUrl, string blobPath, string? description = null)
    {
        int fileId = await _db.CreateProductFileAsync(fileType, blobUrl, blobPath, description);

        return fileId;
    }
}
