using common;
using _2;

DataReader<IdRange> reader = new DataReader<IdRange>((data) => new IdRange(data), ',');
var data = reader.getData();

long sum = 0;
foreach (var idrange in data)
{
    var invalidIds = idrange.GetInvalidIds();
    if (invalidIds.Count == 0) continue;
    sum += invalidIds.Aggregate((sum, next) => sum + next);
}

Console.WriteLine(sum); 
