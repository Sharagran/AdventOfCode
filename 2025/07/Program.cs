using _7;
using common;
using System.Numerics;
using System.Diagnostics;

// loading & parsing
var reader = new DataReader<string>((data) => data);
var rows = reader.getData();

#region Parsing
char[,] data = new char[rows.Count, rows[0].Length];
Vector2 beamPos = new Vector2(-1, -1);
for (int y = 0; y < rows.Count; y++)
{
    for (int x = 0; x < rows[0].Length; x++)
    {
        char s = rows[y][x];
        data[y, x] = s;
        if (s == 'S')
        {
            beamPos = new Vector2(x, y);
        }
    }
}
#endregion

#region Part 1
var m = new Manifold(data, beamPos);

bool endReached;
//m.Draw();
do
{
    endReached = m.SimulateStep();
    //Console.Clear();
    //m.Draw();
}
while (endReached == false);
//m.Draw();

Console.WriteLine(m.SplitCounter);
#endregion

#region Part 2

Console.WriteLine(m.CountAllPossiblePaths());

#endregion