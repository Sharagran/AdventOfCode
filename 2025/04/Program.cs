using _4;
using common;

var reader = new DataReader<Row>((data) => new Row(data));
var rows = reader.getData();
Map m = new Map();
m.AddRange(rows);
for (int i = 0; i < m.Count; i++)
{
    m[i].setYLocation(i);
    m[i].setMap(m);
}

//Console.WriteLine(m.CountAccessiblePaperRolls());


// part 2
int sum = 0;
int res;

do
{
    res = m.CountAccessiblePaperRolls();
    sum += res;
}
while (res > 0);
Console.WriteLine(sum);