using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    internal class Bank
    {
        List<int> batteries;
        public Bank(string data)
        {
            batteries = new List<int>();
            foreach (char item in data)
            {
                batteries.Add(item - '0');
            }
        }

        #region Part 1
        public int getLargestJoltage()
        {
            int a = -1, b = -1;

            // find largest element in list which is not in the last index
            int ai = -1;
            for (int i = 0; i < batteries.Count; i++)
            {
                int battery = batteries[i];
                if (battery > a && i != batteries.Count - 1)
                {
                    a = battery;
                    ai = i;
                }
            }

            for (int i = ai + 1; i < batteries.Count; i++)
            {
                int battery = batteries[i];
                if (battery > b)
                {
                    b = battery;
                }
            }

            return a * 10 + b;
        }
        #endregion

        #region Part 2
        public long getLargestJoltage(int numberOfBatteries)
        {
            List<int> highestBatteries = new List<int>();

            var (max, maxIndex) = highestJoltageAfterI(-1, numberOfBatteries);
            highestBatteries.Add(max);

            for (int i = 1; i < numberOfBatteries; i++)
            {
                (max, maxIndex) = highestJoltageAfterI(maxIndex, numberOfBatteries - i);
                highestBatteries.Add(max);
            }

            long totalJoltage = 0;
            for (int i = 0; i < highestBatteries.Count; i++)
            {
                totalJoltage += highestBatteries[i] * (long)Math.Pow(10, numberOfBatteries - i - 1);
            }

            return totalJoltage;
        }

        private (int, int) highestJoltageAfterI(int index, int banksRemaining)
        {
            int max = -1, maxi = -1;
            for (int i = index+1; i < batteries.Count - banksRemaining + 1; i++)
            {
                int battery = batteries[i];
                if(battery > max)
                {
                    max = battery;
                    maxi = i;
                }
            }

            return (max, maxi);
        }
        #endregion
    }
}
