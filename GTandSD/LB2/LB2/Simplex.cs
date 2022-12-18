using CenterSpace.NMath.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB2
{
    // Solves LP transport task by Simplex algorithm (NMath implementation)
    internal static class Simplex
    {
        internal static int[,] Solve(int[,] effort, int[] sourceConstrain, int[] destinationConstraing)
        {
            var n = effort.GetLength(1); // columns
            var m = effort.GetLength(0); // rows

            Debug.Assert(m == sourceConstrain.Length);
            Debug.Assert(n == destinationConstraing.Length);
            Debug.Assert(sourceConstrain.Sum() == destinationConstraing.Sum());

            var c = new DoubleVector(m * n);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    c[j * n + i] = effort[j, i];
                }
            }

            var lp = new MixedIntegerLinearProgrammingProblem(c);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    lp.AddLowerBound(j * n + i, 0);
                    lp.SetIntegrality(j * n + i, true);
                }
            }

            for (var j = 0; j < m; j++)
            {
                var sc = new DoubleVector(m * n);
                sc.Clear();

                for (var i = 0; i < n; i++)
                {
                    sc[j * n + i] = 1;
                }

                lp.AddUpperBoundConstraint(sc, sourceConstrain[j]);
            }

            for (var i = 0; i < n; i++)
            {
                var dc = new DoubleVector(m * n);
                dc.Clear();

                for (var j = 0; j < m; j++)
                {
                    dc[j * n + i] = 1;
                }

                lp.AddEqualityConstraint(dc, destinationConstraing[i]);
            }

            var solver = new DualSimplexSolverORTools();
            solver.Solve(lp, true);

            Debug.Assert(solver.Result == ConstrainedOptimizerORTools.SolveResult.Optimal);
            Debug.Assert(solver.OptimalX.Length == m * n);

            var optimalX = solver.OptimalX.ToArray();
            var res = new int[m, n];

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    res[j, i] = (int)optimalX[j * n + i];
                }
            }

            return res;
        }
    }
}
