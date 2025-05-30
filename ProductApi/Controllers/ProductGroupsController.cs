using FarmaTechData.Data;
using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;
[ApiController]
[Route("api/product-groups")]
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductGroupModel>> Get(int id)
    {
        var productGroups = await _db.GetProductGroupsAsync();
        var productGroup = productGroups.FirstOrDefault(x => x.Id == id);
        return productGroup == null ? NotFound() : Ok(productGroup);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] ProductGroupModel productGroup)
    {
        if (productGroup == null)
        {
            return BadRequest("Product group cannot be null");
        }
        var productGroupId = await _db.CreateProductGroupAsync(productGroup);
        return productGroupId == -1 ? BadRequest($"Product Group of {productGroup.Group} already exists!") : CreatedAtAction(nameof(Get), new { Id = productGroupId }, productGroupId);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> Put([FromBody] ProductGroupModel productGroup)
    {
        if (productGroup == null)
        {
            return BadRequest("Product group cannot be null");
        }
        var productGroups = await _db.GetProductGroupsAsync();
        var existingGroup = productGroups.FirstOrDefault(x => x.Id == productGroup.Id);
        if (existingGroup == null)
        {
            return NotFound($"Product group with ID {productGroup.Id} not found");
        }
        var updated = await _db.UpdateProductGroupAsync(productGroup);
        return updated ? Ok(true) : BadRequest($"Failed to update product group {productGroup.Group}");
    }
}
