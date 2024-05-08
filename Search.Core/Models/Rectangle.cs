using NetTopologySuite.Geometries;

namespace Search.Core.Models;

public class Rectangle
{
    public int Id { get; set; }

    public double X1 { get; set; } // left-upper corner

    public double Y1 { get; set; } // left-upper corner

    public double X2 { get; set; }

    public double Y2 { get; set; }
}
