using common;

int passingZero = 0;
int dial = 50;

int mod(int x, int m, bool first = true)
{
    if (x >= m)
    {
        passingZero++;
        return mod(x - m, m, false);
    }
    else if (x < 0)
    {
        passingZero++;
        return mod(x + m, m, false);
    }
    else
    {
        if (x == 0 && first)
        {
            passingZero++;
        }
        return x;
    }
}




DataReader<string> fr = new DataReader<string>((data) => data);
var data = fr.getData();
//data = fr.getTestData();

DataReader<Test> fr2 = new DataReader<Test>((string data) => new Test(data));
var testData = fr2.getTestData();



foreach (string value in data)
{
    char LR = value[0];
    int offset = Convert.ToInt32(value.Substring(1, value.Length - 1));

    try2(offset, LR == 'R');
    //if (LR == 'L')
    //{
    //    if (dial == 0)
    //    {
    //        passingZero--;
    //    }
    //    dial = mod(dial - offset, 100);
    //}
    //else if (LR == 'R')
    //{
    //    dial = mod(dial + offset, 100);
    //}

}

// 1150
Console.WriteLine(passingZero); // 6738

void try2(int offset, bool positive)
{
    while (offset > 0)
    {
        dial += positive ? 1 : -1;
        offset--;

        if(dial == 100)
        {
            dial = 0;
        }
        if(dial == -1)
        {
            dial = 99;
        }

        if (dial == 0)
        {
            passingZero++;
        }
    }
}