using Microsoft.EntityFrameworkCore;
using Search.Core.Models;

namespace Search.Data;

public class SearchDbContext : DbContext
{
    public SearchDbContext(DbContextOptions<SearchDbContext> options)
        : base(options)
    {
    }

    public DbSet<Rectangle> Rectangles { get; set; }

    //public DbSet<Segment> Segments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SearchDbContext).Assembly);
    }
}
