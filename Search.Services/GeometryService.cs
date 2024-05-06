using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        string cacheKey = $"IntersectingRectangles_{segment.X}_{segment.Y}_{segment.Width}_{segment.Height}";

        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Rectangle>? cachedRectangles))
        {
            cachedRectangles = await _context.Rectangles
                .Where(r => r.X <= segment.X &&
                            (r.X + r.Width) >= (segment.X + segment.Width) &&
                            r.Y <= segment.Y &&
                            (r.Y + r.Height) >= (segment.Y + segment.Height))
                .ToArrayAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10)); 
            //TODO: time must takes from props project

            _cache.Set(cacheKey, cachedRectangles, cacheEntryOptions);
        }

        return cachedRectangles!;
    }
}
