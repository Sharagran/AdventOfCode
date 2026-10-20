using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    internal class NumberRange
    {
        private string prefix;
        public NumberRange(ulong start, ulong end)
        {
            // find identical prefix
            string sStart = start.ToString();
            string sEnd = end.ToString();

            string prefix = String.Empty;
            for (int i = 0; i < sEnd.Length; i++)
            {
                if (sStart[i] != sEnd[i])
                {
                    prefix = sStart.Substring(0, i);
                    //Console.WriteLine(prefix);
                    break;
                }
            }

            this.prefix = prefix;
        }
    }
}
