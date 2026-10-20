using _10;
using common;
using System.Collections;
using System.Numerics;

var reader = new DataReader<IndicatorLights>((data) => new IndicatorLights(data));
var rows = reader.getTestData();

//Console.WriteLine(rows[0].CalculateFewestButtonPresses());
//rows[1].CalculateFewestButtonPresses();
//rows[2].CalculateFewestButtonPresses();

IEnumerable<IEnumerable<int>> result = Helper.GetPermutations(Enumerable.Range(1, 3), 2);

int sum = 0;
foreach (var item in rows)
{
    //sum += item.CheckPerms();
}

Console.WriteLine(sum); // 500

Console.WriteLine(rows[0].GetMinPressesToSatisfyJoltage());