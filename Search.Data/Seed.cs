using Search.Core.Models;

namespace Search.Data;

public class Seed
{
    public static async Task SeedRectangles(SearchDbContext context)
    {
        if (context.Rectangles.Any()) return;

        var rectangles = new List<Rectangle>
        {
            new Rectangle { X = 10, Y = 20, Width = 30, Height = 40 },
            new Rectangle { X = 15, Y = 25, Width = 35, Height = 45 },
            new Rectangle { X = 20, Y = 30, Width = 40, Height = 50 },
            new Rectangle { X = 25, Y = 35, Width = 45, Height = 55 },
            new Rectangle { X = 30, Y = 40, Width = 50, Height = 60 },
            new Rectangle { X = 35, Y = 45, Width = 55, Height = 65 },
            new Rectangle { X = 40, Y = 50, Width = 60, Height = 70 },
            new Rectangle { X = 45, Y = 55, Width = 65, Height = 75 },
            new Rectangle { X = 50, Y = 60, Width = 70, Height = 80 },
            new Rectangle { X = 55, Y = 65, Width = 75, Height = 85 }
        };

        await context.Rectangles.AddRangeAsync(rectangles);
        await context.SaveChangesAsync();
    }
}
