using _6;
using common;
using System.Text.RegularExpressions;



var reader = new DataReader<string>((data) => data);
var rows = reader.getData();

#region Part 1
//List<Entry> entries = new List<Entry>();
//List<string> operators = new List<string>();

//foreach (var row in rows)
//{
//    var parts = Regex.Split(row, @"\s+").Where(s => s != string.Empty).ToArray();
//    for (int i = 0; i < parts.Length; i++)
//    {
//        if (parts[i].Length == 0) continue;

//        if (rows.IndexOf(row) == rows.Count - 1)
//        {
//            operators.Add(parts[i]);
//            continue;
//        }

//        entries.Add(new Entry(Convert.ToUInt64(parts[i]), i));
//    }
//}

//var tmp = entries.GroupBy((e) => e.rowIndex).ToList();


//ulong totalSum = 0;
//for (int i = 0; i < tmp.Count; i++)
//{
//    string op = operators[i];
//    ulong sum = 0;
//    if(op == "*") sum = 1;
//    foreach (var e in tmp[i])
//    {
//        switch (op)
//        {
//            case "*":
//                sum *= e.number;
//                break;
//            case "+":
//                sum += e.number;
//                break;
//            default:
//                break;
//        }
//    }
//    Console.WriteLine(sum);
//    totalSum += sum;
//}

//Console.WriteLine(totalSum.ToString());

//public struct Entry
//{
//    public ulong number;
//    public int rowIndex;

//    public Entry(ulong number, int rowIndex)
//    {
//        this.number = number;
//        this.rowIndex = rowIndex;
//    }
//}
#endregion

#region Part 2

var m = new Matrix(rows.ToList());
m.RotateMatrixCounterClockwise();

// convert 2d array into bins with numbers and their operator
List<Col> cols = new List<Col>();
List<string> numbers = new List<string>();
char op = '\0';
for (int i = 0; i < m.data.GetLength(0); i++)         // rows
{
    string number = String.Empty;
    for (int j = 0; j < m.data.GetLength(1); j++)     // columns
    {
        char c = m.data[i, j];
        if( Char.IsDigit(c) )
        {
            number += c;
        }

        if (c == '+' || c == '*')
        {
            op = c;
        }
    }
    if(number.Length != 0)
    {
        numbers.Add(number);
        number = String.Empty;
    }
    if (op != '\0')
    {
        cols.Add(new Col(numbers, op));
        numbers.Clear();
        op = '\0';
    }
}

// calculate results per row
ulong sum = 0;
foreach (var col in cols)
{
    sum += col.Calc();
}

Console.WriteLine(sum.ToString());

#endregion

public struct Col
{
    List<ulong> Numbers;
    char op;

    public Col(List<string> numbers, char op)
    {
        List<ulong> parsedNumbers = new List<ulong>();
        foreach (var item in numbers)
        {
            parsedNumbers.Add(Convert.ToUInt64(item));
        }
        this.Numbers = parsedNumbers;
        this.op = op;
    }

    public ulong Calc()
    {
        ulong res;
        if(op == '+')
        {
            res = 0;
        }
        else
        {
            // * 
            res = 1;
        }

        foreach (var item in Numbers)
        {
            if(op == '+')
            {
                res += item;
            }
            else
            {
                res *= item;
            }
        }

        return res;
    }
}