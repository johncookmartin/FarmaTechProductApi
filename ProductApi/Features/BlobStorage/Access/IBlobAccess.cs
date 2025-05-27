using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Features.BlobStorage.Access;
public interface IBlobAccess
{
    Task<ProductFileModel?> DownloadFileById(int id);
    Task<FileStreamResult> DownloadFileByName(string fileName);
    Task<int?> UploadFile([FromForm] IFormFile file);
    Task UpdateFile(ProductFileModel productFile);
}