using Search.Api.Modules.Dtos;
using Search.Core.Models;

namespace Search.Services.Contracts;

public interface IGeometryService
{
    Task<IEnumerable<Rectangle>> FindIntersectingRectanglesAsync(SegmentDto segment);
}
