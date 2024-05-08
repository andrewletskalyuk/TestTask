using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetTopologySuite.Geometries;
using NetTopologySuite.Triangulate;
using Search.Api.Modules.Dtos;
using Search.Core.Models;
using Search.Data;
using Search.Services.Contracts;

namespace Search.Services;

public class GeometryService : IGeometryService
{
    readonly SearchDbContext _context;
    readonly IMemoryCache _cache;

    public GeometryService(SearchDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Rectangle>> FindIntersectingRectanglesAsync(SegmentDto segment)
    {
        var factory = new GeometryFactory();
        var segmentLine = factory.CreateLineString(new[]
        {
            new Coordinate(segment.X1, segment.Y1),
            new Coordinate(segment.X2, segment.Y2)
        });

        var rectangles = await _context.Rectangles.ToListAsync();
        var intersectingRectangles = rectangles.Where(r =>
        {
            var rectangle = factory.CreatePolygon(new[]
            {
                new Coordinate(r.X1, r.Y1),
                new Coordinate(r.X2, r.Y1),
                new Coordinate(r.X2, r.Y2),
                new Coordinate(r.X1, r.Y2),
                new Coordinate(r.X1, r.Y1) // Close the polygon
            });

            return rectangle.Intersects(segmentLine);
        }).ToList();

        return intersectingRectangles;
    }
}
