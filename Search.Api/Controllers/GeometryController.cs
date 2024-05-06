using Microsoft.AspNetCore.Mvc;
using Search.Api.Modules.Dtos;
using Search.Services.Contracts;

namespace Search.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeometryController : ControllerBase
{
    readonly IGeometryService _geometryService;

    public GeometryController(IGeometryService geometryService)
    {
        _geometryService = geometryService;
    }

    [HttpPost("intersections")]
    public async Task<IActionResult> FindIntersections([FromBody] SegmentDto segment)
    {
        //TODO: MediatR - we can use it, if need but this is another story)
        return Ok(await _geometryService.FindIntersectingRectanglesAsync(segment));
    }
}
