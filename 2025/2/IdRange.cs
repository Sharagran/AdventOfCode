using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2
{
    internal class IdRange
    {
        // both inclusive
        public long start;
        public long end;
        public IdRange(string data)
        {
            string[] parts = data.Split('-');
            start = long.Parse(parts[0]);
            end = long.Parse(parts[1]);
        }

        public List<long> GetInvalidIds()
        {
            var ids = new List<long>();
            for (long i = start; i <= end; i++)
            {
                if(isInvalid2(i))
                {
                    ids.Add(i);
                }
            }
            return ids;
        }

        public override string ToString()
        {
            return $"{start:n0}-{end:n0}";
        }

        #region Static Methods
        public static bool isInvalid(long id)
        {
            var numbers = splitNumber(id);
            if (numbers.Length == 0) return false;

            return numbers[0] == numbers[1];
        }

        public static bool isInvalid2(long number)
        {
            string sNum = number.ToString();
            for (int chunkSize = 1; chunkSize <= sNum.Length / 2; chunkSize++)
            {
                if (sNum.Length % chunkSize != 0) continue;

                string chunk = sNum.Substring(0, chunkSize);

                bool invalid = true;
                for (int i = 0; i < sNum.Length; i++)
                {
                    if (sNum[i] == chunk[i % chunkSize]) continue;
                    else
                    {
                        invalid = false;
                        break;
                    }
                }
                if (invalid) return true;
            }

            return false;
        }

        public static double[] splitNumber(long number)
        {
            var digits = calculateDigits(number);
            if (digits % 2 == 0)
            {
                var splittedNumber = number / Math.Pow(10, digits / 2);
                var firstHalf = Math.Truncate(splittedNumber); // cut float calc diff
                var lastHalf = Math.Round(splittedNumber % 1 * Math.Pow(10, digits / 2));

                return [firstHalf, lastHalf];
            }
            else
            {
                return [];
            }
        }

        private static double calculateDigits(long number)
        {
            return Math.Ceiling(Math.Log(number + 1) / Math.Log(10));
        }

        #endregion
    }
}
