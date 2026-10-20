using _3;
using common;

var reader = new DataReader<Bank>((data) => new Bank(data));
var banks = reader.getData();

long sum = 0;
foreach (var bank in banks)
{
    long tmp = bank.getLargestJoltage(12);
    sum += tmp;
    Console.WriteLine(tmp);
}

Console.WriteLine(sum);