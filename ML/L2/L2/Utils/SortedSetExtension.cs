using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2.Utils
{
    public static class SortedSetExtension
    {
        public static int FindIndex(this SortedSet<string> set, string value)
        {
            var index = 0;

            foreach (var item in set)
            {
                if (item == value)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
