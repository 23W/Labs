using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB2
{
    // Solves LP transport task by Greedy algorithm
    internal static class Greedy
    {
        internal static int[,] Solve(int[,] effort, int[] sourceConstrain, int[] destinationConstraing)
        {
            var n = effort.GetLength(1); // columns
            var m = effort.GetLength(0); // rows

            Debug.Assert(m == sourceConstrain.Length);
            Debug.Assert(n == destinationConstraing.Length);
            Debug.Assert(sourceConstrain.Sum() == destinationConstraing.Sum());

            const int unused = int.MinValue;

            var res = new int[m, n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    res[j, i] = unused;
                }
            }

            bool stop = false;
            do
            {
                int minE = int.MaxValue;
                int minN = unused, minM = unused;

                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < m; j++)
                    {
                        if (res[j, i] == unused && 
                            effort[j, i] < minE)
                        {
                            minE = effort[j, i];
                            minN = i;
                            minM = j;
                        }
                    }
                }

                stop = minN == unused ||
                       minM == unused;
                if (!stop)
                {
                    if (sourceConstrain[minM] < destinationConstraing[minN])
                    {
                        res[minM, minN] = sourceConstrain[minM];
                    }
                    else
                    {
                        res[minM, minN] = destinationConstraing[minN];
                    }

                    sourceConstrain[minM] -= res[minM, minN];
                    destinationConstraing[minN] -= res[minM, minN];
                }
            }
            while (!stop);

            return res;
        }
    }
}
