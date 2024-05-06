using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Search.Api.Modules.Dtos;
using Search.Core.Models;
using Search.Data;
using Search.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Search.Tests;

public class GeometryServiceTests
{
    private readonly Mock<IMemoryCache> _mockCache;
    private readonly SearchDbContext _dbContext;
    private readonly GeometryService _geometryService;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions;

    public GeometryServiceTests()
    {
        // Set up the in-memory database
        var options = new DbContextOptionsBuilder<SearchDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new SearchDbContext(options);

        // Mock the memory cache
        _mockCache = new Mock<IMemoryCache>();

        // Set up the cache entry return
        var cache = new MemoryCache(new MemoryCacheOptions());
        _mockCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns((object key) => cache.CreateEntry(key));

        // Set up the service with dependencies
        _geometryService = new GeometryService(_dbContext, _mockCache.Object);

        // Cache entry options used in the tests
        _cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10));
    }

    [Fact]
    public async Task FindIntersectingRectanglesAsync_ReturnsCachedData_IfPresent()
    {
        // Arrange
        var segment = new SegmentDto { X = 5, Y = 5, Width = 10, Height = 10 };
        var cacheKey = $"IntersectingRectangles_{segment.X}_{segment.Y}_{segment.Width}_{segment.Height}";
        var expectedRectangles = new List<Rectangle> { new Rectangle { Id = 1, X = 4, Y = 4, Width = 12, Height = 12 } };
        var cacheEntry = Mock.Of<ICacheEntry>();

        _mockCache.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(cacheEntry);
        _mockCache.Setup(m => m.TryGetValue(cacheKey, out expectedRectangles)).Returns(true);

        // Act
        var rectangles = await _geometryService.FindIntersectingRectanglesAsync(segment);

        // Assert
        Assert.True(rectangles.SequenceEqual(expectedRectangles));
        _mockCache.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Never);
    }


    [Fact]
    public async Task FindIntersectingRectanglesAsync_RetrievesFromDatabase_WhenNotCached()
    {
        // Arrange
        var segment = new SegmentDto { X = 15, Y = 15, Width = 5, Height = 5 };
        var cacheKey = $"IntersectingRectangles_{segment.X}_{segment.Y}_{segment.Width}_{segment.Height}";
        var rectanglesInDb = new List<Rectangle>
            {
                new Rectangle { Id = 2, X = 14, Y = 14, Width = 7, Height = 7 }
            };

        _dbContext.Rectangles.AddRange(rectanglesInDb);
        await _dbContext.SaveChangesAsync();

        _mockCache.Setup(x => x.TryGetValue(cacheKey, out It.Ref<IEnumerable<Rectangle>>.IsAny)).Returns(false);

        // Act
        var rectangles = await _geometryService.FindIntersectingRectanglesAsync(segment);

        // Assert
        Assert.Contains(rectanglesInDb.First(), rectangles);
        _mockCache.Verify(x => x.Set(cacheKey, It.IsAny<IEnumerable<Rectangle>>(), _cacheEntryOptions), Times.Once);
    }
}
