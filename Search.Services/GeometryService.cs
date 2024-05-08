using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetTopologySuite.Geometries;
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
            new Coordinate(segment.X, segment.Y),
            new Coordinate(segment.X + segment.Width, segment.Y + segment.Height)
        });

        string cacheKey = $"IntersectingRectangles_{segment.X}_{segment.Y}_{segment.Width}_{segment.Height}";

        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Rectangle>? cachedRectangles))
        {
            var rectangles = await _context.Rectangles.ToListAsync();
            var intersectingRectangles = rectangles.Where(r =>
            {
                var rectangle = factory.CreatePolygon(new[]
                {
                    new Coordinate(r.X, r.Y),
                    new Coordinate(r.X + r.Width, r.Y),
                    new Coordinate(r.X + r.Width, r.Y + r.Height),
                    new Coordinate(r.X, r.Y + r.Height),
                    new Coordinate(r.X, r.Y)
                });

                return rectangle.Intersects(segmentLine);
            }).ToList();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)); 
            // TODO pulling this from config

            _cache.Set(cacheKey, intersectingRectangles, cacheEntryOptions);
            cachedRectangles = intersectingRectangles;
        }

        return cachedRectangles!;
    }
}
