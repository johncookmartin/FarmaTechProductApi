using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Exceptions;
using ProductApi.Features.BlobStorage.Access;

namespace ProductApi.Controllers;
[ApiController]
[Route("api/product-files")]
public class ProductFilesController : ControllerBase
{
    private readonly IProductFileAccess _db;

    public ProductFilesController(IProductFileAccess db)
    {
        _db = db;
    }

    [HttpGet("blob/{fileName}")]
    public async Task<IActionResult> GetByFileName(string fileName)
    {
        try
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("File name cannot be null or empty");
            }
            var streamResult = await _db.DownloadFileByName(fileName);
            return streamResult;
        }
        catch (ApiException apiEx)
        {
            return StatusCode(apiEx.StatusCode, apiEx.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("data/{id:int}")]
    public async Task<ActionResult<ProductFileModel>> GetById(int id)
    {
        var productFile = await _db.DownloadFileById(id);
        return productFile == null ? NotFound() : Ok(productFile);
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile file)
    {
        int? fileId = null;

        try
        {
            fileId = await _db.UploadFile(file);
        }
        catch (ApiException apiEx)
        {
            return StatusCode(apiEx.StatusCode, apiEx.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        if (fileId == null)
        {
            return BadRequest($"File of type {file.ContentType} already exists!");
        }

        return CreatedAtAction(nameof(GetById), new { Id = fileId }, fileId);

    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProductFileModel productFile)
    {
        try
        {
            await _db.UpdateFile(productFile);
        }
        catch (ApiException apiEx)
        {
            return StatusCode(apiEx.StatusCode, apiEx.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return NoContent();
    }

}
