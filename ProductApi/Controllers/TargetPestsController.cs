using FarmaTechData.Data;
using FarmaTechData.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers;

[ApiController]
[Route("api/target-pests")]
public class TargetPestsController : ControllerBase
{
    private readonly IProductData _db;

    public TargetPestsController(IProductData db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<TargetPestModel>>> GetTargetPests()
    {
        var targetPests = await _db.GetTargetPestsAsync(null);
        return targetPests == null ? NotFound() : Ok(targetPests);
    }

    [HttpGet("group/{groupId:int}")]
    public async Task<ActionResult<List<TargetPestModel>>> GetTargetPestsByGroup(int groupId)
    {
        var targetPests = await _db.GetTargetPestsAsync(groupId);
        return targetPests == null ? NotFound() : Ok(targetPests);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] string targetPest)
    {
        if (string.IsNullOrWhiteSpace(targetPest))
        {
            return BadRequest("Target pest cannot be null or empty");
        }
        var targetPestId = await _db.CreateTargetPestAsync(targetPest);
        return targetPestId == -1 ? BadRequest($"Target Pest '{targetPest}' already exists!") : Ok(targetPestId);
    }

}
