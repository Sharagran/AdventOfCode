using _5;
using common;
using System;

var reader = new DataReader<string>((data) => data);
var rows = reader.getData();

#region Parsing
List<MyRange> ranges = new List<MyRange>();
List<ulong> ids = new List<ulong>();

bool blankLineFound = false;
List<RangeNumber> rn = new List<RangeNumber>();
for (int i = 0; i < rows.Count; i++)
{
    string line = rows[i];

    if(line.Length == 0)
    {
        blankLineFound = true;
        continue;
    }

    if (!blankLineFound)
    {
        string[] parts = line.Split('-');
        ulong start = Convert.ToUInt64(parts[0]);
        ulong end = Convert.ToUInt64(parts[1]);
        ranges.Add(new MyRange(start, end));
        rn.Add(new RangeNumber(start, false));
        rn.Add(new RangeNumber(end, true));
    }
    else
    {
        ulong id = Convert.ToUInt64(line);
        ids.Add(id);
    }

}
#endregion

#region Part 1
int freshIds = 0;
foreach (var id in ids)
{
    foreach (var range in ranges)
    {
        if(id >= range.Start && id <= range.End)
        {
            freshIds++;
            break;
        }
    }
}

Console.WriteLine("Fresh ids: " + freshIds + "\n\n");
#endregion

#region Part 2
ranges.Sort((a, b) =>
{
    if(a.Start == b.Start)
    {
        return a.End > b.End ? 1 : -1;
    }

    return a.Start > b.Start ? 1 : -1;
});

#region idk
var nums = rn.OrderBy(x => x.Number);
int unclosedRanges = 0;
ulong startIndex = 0;
ulong sum = 0;
foreach (var num in nums)
{
    if(num.isEnd)
    {
        unclosedRanges--;

        if (unclosedRanges == 0)
        {
            sum += num.Number + 1 - startIndex;
        }
    }
    else
    {
        if (unclosedRanges == 0)
        {
            startIndex = num.Number;
        }
        unclosedRanges++;
    }
}

//Console.WriteLine(sum);
#endregion

ranges = ranges.DistinctBy(r => r.ToString()).ToList();

// fix overlaps
var nonOverlappingRanges = new List<MyRange>();
var overlaps = new List<MyRange>();
for (int i = 0; i < ranges.Count; i++)
{
    if(i == ranges.Count - 1)
    {
        Console.WriteLine(ranges[i] + "\tis valid");
        nonOverlappingRanges.Add(ranges[i]);
        break;
    }

    MyRange current = ranges[i];
    MyRange next = ranges[i + 1];

    // check if inside
    if (
        // current inside next
        (current.Start >= next.Start && current.End <= next.End)
        ||
        // next inside current
        (next.Start <= current.Start && next.End <= current.End)
        )
    {
        Console.WriteLine(current + "\tinside\t" + next);
        Console.WriteLine("skipping\t" + current);
        Console.WriteLine();
        continue;
    }

    if(current.End >= next.Start)
    {
        var smallerEnd = Math.Min(current.End, next.End);
        var biggerEnd = Math.Max(current.End, next.End);
        var movedRange = new MyRange(current.Start, smallerEnd);
        var movedRange2 = new MyRange(smallerEnd + 1, biggerEnd);

        var overlapStart = Math.Max(current.Start, next.Start);
        var overlapEnd = Math.Min(current.End, next.End);
        overlaps.Add(new MyRange(overlapStart, overlapEnd));

        Console.WriteLine();
        Console.WriteLine(current + "\toverlaps\t" + next + $"| overlap: {overlapStart:n0}-{overlapEnd:n0}");
        Console.WriteLine(current + "\t-->\t" + movedRange);
        Console.WriteLine(next + "\t-->\t" + movedRange2 + "\n");

        // updated ranges are not sorted, duplicates will occur when  not skipping an element but skipping will result in false entries due to not sorted
        ranges[i] = movedRange;
        ranges[i + 1] = movedRange2;
        //i++;
        nonOverlappingRanges.Add(movedRange);
        nonOverlappingRanges.Add(movedRange2);
        //ranges[i] = new MyRange(current.Start, next.Start - 1);
    }
    else
    {
        Console.WriteLine(current + "\tis valid");
        nonOverlappingRanges.Add(current);
    }
}

// remove dupes
nonOverlappingRanges = nonOverlappingRanges.DistinctBy(r => r.ToString()).ToList();

// calcultate elements
ulong totalFreshIds = 0;
foreach (var range in ranges)
{
    ulong count = range.GetCount();
    totalFreshIds += count;
}
foreach (var range in overlaps)
{
    totalFreshIds -= range.GetCount();
}

Console.WriteLine("\n\nTotal ids: " + totalFreshIds); // Not right: 325.300.804.412.718 , 325.300.804.412.721, 304.921.375.431.488, 2x 336.371.612.680.340, 333.583.707.800.519, 343.269.827.150.447
// 348.548.952.146.324 too high

#endregion

public struct MyRange
{
    public ulong Start, End;
    public MyRange(ulong start, ulong end)
    {
        this.Start = start;
        this.End = end;
    }

    public bool isEmpty()
    {
        return Start == 0 && End == 0;
    }

    public ulong GetCount()
    {
        return End + 1 - Start;
    }

    public override string ToString()
    {
        return $"{Start.ToString("n0")}-{End.ToString("n0")} | {this.GetCount()}";
    }
}

public struct RangeNumber
{
    public ulong Number;
    public bool isEnd;

    public RangeNumber(ulong number, bool isEnd)
    {
        this.Number = number;
        this.isEnd = isEnd;
    }
}