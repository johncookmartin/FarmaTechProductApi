using FarmaTechData.Data;
using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductGroupsController : ControllerBase
{
    private readonly IProductData _db;

    public ProductGroupsController(IProductData db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<ProductGroupModel>> Get()
    {
        var productGroups = await _db.GetProductGroupsAsync();
        return productGroups == null ? NotFound() : Ok(productGroups);
    }
}
