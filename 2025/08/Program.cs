using _8;
using common;
using System.Numerics;


var reader = new DataReader<Vector3>((data) => {
    var positionData = Array.ConvertAll(data.Split(','), s => int.Parse(s));
    return new Vector3(positionData[0], positionData[1], positionData[2]);
});

var junctionBoxPositions = reader.getData();

var distances = new List<Distance>();
foreach (var a in junctionBoxPositions)
{
    foreach (var b in junctionBoxPositions)
    {
        if (a == b) continue;

        double distance = (a - b).Length();
        //if(distances.Contains(new Distance(b, a, distance)) == false) // old dupe removal, expensive
        distances.Add(new Distance(a, b, distance));
    }
}

#region Fynn Loop
//for (int i = 0; i < junctionBoxPositions.Count; i++)
//{
//    for (int j = i + 1; j < junctionBoxPositions.Count; j++)
//    {
//        var a = junctionBoxPositions[i];
//        var b = junctionBoxPositions[j];

//        double distance = (a - b).Length();
//        //if(distances.Contains(new Distance(b, a, distance)) == false) // old dupe removal, expensive
//        distances.Add(new Distance(a, b, distance));
//    }
//}
#endregion

distances = distances.OrderBy(d => d.distance).ToList();
// removed dupes (a;b, b;a)
distances = distances.Where((d, i) => i % 2 != 0).ToList();

CircuitManager cm = new CircuitManager();
for (int i = 0; i < distances.Count; i++)
{
    var item = distances[i];
    bool newConnection = cm.Connect(item.a, item.b);
}

cm.circuits = cm.circuits.OrderBy(c => c.junctionBoxes.Count).Reverse().ToList();

// Part 1
//Console.WriteLine(cm.circuits[0].junctionBoxes.Count * cm.circuits[1].junctionBoxes.Count * cm.circuits[2].junctionBoxes.Count);

public struct Distance
{
    public Vector3 a;
    public Vector3 b;
    public double distance;

    public Distance(Vector3 a, Vector3 b, double distance)
    {
        this.a = a;
        this.b = b;
        this.distance = distance;
    }
}