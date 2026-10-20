using common;
using System.Numerics;

var reader = new DataReader<Vector2>((data) =>
{
    float[] values = Array.ConvertAll(data.Split(','), s => float.Parse(s));
    return new Vector2(values[0], values[1]);
});
var rows = reader.getTestData();

float maxX = rows.Max(v => v.X);
float maxY = rows.Max(v => v.Y);

// Find largest area
List<Rectangle> rectangles = new List<Rectangle>();
foreach (var item in rows)
{
    foreach (var item1 in rows)
    {
        long A = CalculateArea(item, item1);
        rectangles.Add(new Rectangle(item, item1, A));
    }
}

rectangles = rectangles.OrderBy(r => r.Area).Reverse().ToList();

Console.WriteLine(rectangles[0].Area); // long = 4.767.418.746, float = 4.767.280.642

// part 2

//Console.WriteLine(isPointInside(new Vector2(12, 1)));
DrawTestMap();

//foreach (var item in rectangles)
//{
//    Console.WriteLine($"{rectangles.IndexOf(item) + 1} / {rectangles.Count}");
//    // get all vectors between min and max
//    Vector2 min = Vector2.Min(item.a, item.b);
//    Vector2 max = Vector2.Max(item.a, item.b);
//    var vectorsBetween = rows.Where(v => v.X >= min.X && v.X < max.X && v.Y >= min.Y && v.Y < max.Y).ToList();
//    bool valid = true;
//    foreach (var v in vectorsBetween)
//    {
//        foreach (var adjecent in getAdjacentVectors(v))
//        {
//            if (adjecent.Y > max.Y || adjecent.X > max.X || adjecent.Y < min.Y || adjecent.X < min.X)
//            {
//                continue;
//            }
//            if (isPointInside(adjecent) == false)
//            {
//                valid = false;
//                break;
//            }
//        }
//        if (!valid)
//            break;
//    }

//    if (valid)
//    {
//        Console.WriteLine(item.Area); // 4.635.268.638 = too high
//        break;
//    }
//}

foreach (var item in rectangles)
{
    if(isAreaValid(item.a, item.b))
    {
        Console.WriteLine(item.Area);
        break;
    }
}


long CalculateArea(Vector2 start, Vector2 end)
{
    var sizeVector = Vector2.Abs(end - start);
    sizeVector += new Vector2(1, 1); // inclusive
    return Convert.ToInt64(sizeVector.X) * Convert.ToInt64(sizeVector.Y);
}

bool isPointInside(Vector2 p)
{
    int overlapCount = 0;
    foreach (var item in rows)
    {
        Vector2 e = item + new Vector2(0, 0);
        if(p.X < e.X && p.Y < e.Y)
        {
            overlapCount++;
        }
    }

    // Test theory below, multiple points in the same col could be a problem, maybe not, see picture in fynns chat
    // odd overlap count = inside, even = outside
    return overlapCount % 2 != 0;
}

bool isPointInside2(Vector2 p)
{
    // first level check

    //Direction xDirection;
    //Direction yDirection;

    //if (p.X > maxX / 2)
    //{
    //    xDirection = Direction.Right;
    //}
    //else
    //{
    //    xDirection = Direction.Left;
    //}

    //if (p.Y > maxY / 2)
    //{
    //    yDirection = Direction.Down;
    //}
    //else
    //{
    //    yDirection = Direction.Up;
    //}

    int wallCount = 0;
    bool combo = false;
    for (global::System.Int32 x = (int)p.X; x <= maxX; x++)
    {
        if (isWall(new Vector2(x, p.Y)))
        {
            if(combo)
            {
                // prevent false detection for edges, line of walls
                return false;
            }
            combo = true;
            wallCount++;
        }
    }

    //for (global::System.Int32 i = (int)p.Y; i < (yDirection == Direction.Down ? maxY : 0); i += yDirection == Direction.Down ? 1 : -1)
    //{
    //    if (isWall(new Vector2(p.X, i))) wallCount++;
    //}



    return wallCount % 2 == 1;
}

bool isWall(Vector2 p)
{
    var xOrder = rows.FindAll(v => v.Y == p.Y).OrderBy(v => v.X).ToList();
    var yOrder = rows.FindAll(v => v.X == p.X).OrderBy(v => v.Y).ToList();

    for (global::System.Int32 i = 0; i < xOrder.Count-1; i++)
    {
        if (xOrder[i].X <= p.X && xOrder[i+1].X >= p.X)
        {
            return true;
        }
    }

    for (global::System.Int32 i = 0; i < yOrder.Count - 1; i++)
    {
        if (yOrder[i].Y <= p.Y && yOrder[i + 1].Y >= p.Y)
        {
            return true;
        }
    }

    return false;
}

bool isAreaValid(Vector2 a, Vector2 b)
{
    var bX = new Vector2(a.X, b.Y);
    var bY = new Vector2(b.X, a.Y);

    return isPointInside2(bX) && isPointInside2(bY);
}

List<Vector2> getAdjacentVectors(Vector2 p)
{
    return new List<Vector2>() {
        new Vector2(p.X - 1, p.Y - 1),
        new Vector2(p.X, p.Y - 1),
        new Vector2(p.X + 1, p.Y - 1),

        new Vector2(p.X - 1, p.Y),
        new Vector2(p.X + 1, p.Y),

        new Vector2(p.X - 1, p.Y + 1),
        new Vector2(p.X, p.Y + 1),
        new Vector2(p.X + 1, p.Y + 1),
    };
}

void DrawTestMap()
{
    for (global::System.Int32 y = 0; y < 9; y++)
    {
        for (global::System.Int32 x = 0; x < 14; x++)
        {
            var v = new Vector2(x, y);
            char c = isWall(v) ? 'X' : '.';
            if (isPointInside2(v))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(c);

            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public struct Rectangle
{
    public Vector2 a;
    public Vector2 b;
    public long Area;

    public Rectangle(Vector2 a, Vector2 b, long Area)
    {
        this.a = a;
        this.b = b;
        this.Area = Area;
    }
}