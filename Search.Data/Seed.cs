using Search.Core.Models;

namespace Search.Data;

public class Seed
{
    public static async Task SeedRectangles(SearchDbContext context)
    {
        if (context.Rectangles.Any()) return;

        var rectangles = new List<Rectangle>
        {
            new Rectangle { Id = 1, X1 = 10, Y1 = 20, X2 = 10 + 30, Y2 = 20 + 40 },
            new Rectangle { Id = 2, X1 = 15, Y1 = 25, X2 = 15 + 35, Y2 = 25 + 45 },
            new Rectangle { Id = 3, X1 = 20, Y1 = 30, X2 = 20 + 40, Y2 = 30 + 50 },
            new Rectangle { Id = 4, X1 = 25, Y1 = 35, X2 = 25 + 45, Y2 = 35 + 55 },
            new Rectangle { Id = 5, X1 = 30, Y1 = 40, X2 = 30 + 50, Y2 = 40 + 60 },
            new Rectangle { Id = 6, X1 = 35, Y1 = 45, X2 = 35 + 55, Y2 = 45 + 65 },
            new Rectangle { Id = 7, X1 = 40, Y1 = 50, X2 = 40 + 60, Y2 = 50 + 70 },
            new Rectangle { Id = 8, X1 = 45, Y1 = 55, X2 = 45 + 65, Y2 = 55 + 75 },
            new Rectangle { Id = 9, X1 = 50, Y1 = 60, X2 = 50 + 70, Y2 = 60 + 80 },
            new Rectangle { Id = 10, X1 = 55, Y1 = 65, X2 = 55 + 75, Y2 = 65 + 85 }
        };
        

        await context.Rectangles.AddRangeAsync(rectangles);
        await context.SaveChangesAsync();
    }
}
