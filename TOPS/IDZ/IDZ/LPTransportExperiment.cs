using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class LPTransportExperiment
    {
        internal int Variant => 23;

        internal double[] Depot => new double[] { 20 + 10 * Variant, 
                                                  30,
                                                  30 + 10 * Variant, 
                                                  40,
                                                  25,
                                                  15 };

        internal double[] TargetCenter => new double[] { 60 + 10 * Variant, 
                                                         50 + 10 * Variant, 
                                                         50 };

        internal double[,] Price => new double[,] { { 10, 8, 5 },
                                                    {  5, 6, 5 },
                                                    {  4, 8, 7 },
                                                    { 11, 4, 5 },
                                                    {  2, 6, 10 },
                                                    {  4, 3, 8 } };

        internal bool HasResult { get; private set; } = false;
        internal double[,] ResultPlan { get; private set; } = new double[0, 0];
        internal double ResultFare { get; private set; } = 0;

        internal void Run()
        {
            Debug.Assert(Depot.Length == Price.GetLength(0));
            Debug.Assert(TargetCenter.Length == Price.GetLength(1));
            Debug.Assert(Depot.Aggregate((p, c) => p + c) == TargetCenter.Aggregate((p, c) => p + c));

            var flatPrice = Price.Cast<double>().ToArray();
            var solver = new LPSolver(flatPrice, LPSolver.LPType.Integer);

            for (var d = 0; d < Depot.Length; d++)
            {
                var a = new double[Price.GetLength(0), Price.GetLength(1)];

                for (var tc = 0; tc < TargetCenter.Length; tc++)
                {
                    a[d, tc] = 1;
                }
                
                var flatA = a.Cast<double>().ToArray();
                solver.AddConstraint(LPSolver.ConstrainType.Equal, flatA, Depot[d]);
            }


            for (var tc = 0; tc < TargetCenter.Length; tc++)
            {
                var a = new double[Price.GetLength(0), Price.GetLength(1)];

                for (var d = 0; d < Depot.Length; d++)
                {
                    a[d, tc] = 1;
                }
                
                var flatA = a.Cast<double>().ToArray();
                solver.AddConstraint(LPSolver.ConstrainType.Equal, flatA, TargetCenter[tc]);
            }

            for (var i = 0; i < flatPrice.Length; i++)
            {
                solver.AddBound(LPSolver.BoundType.Lower, i, 0);
                solver.AddIntegralConstraint(i);
            }

            var result = solver.Solve(true);
            HasResult = result.Succeeded;

            if (HasResult)
            {
                var plan = new double[Price.GetLength(0), Price.GetLength(1)];
                for(var i = 0; i < result.X.Length; i++)
                {
                    var d = i / TargetCenter.Length;
                    var tc = i % TargetCenter.Length;

                    plan[d, tc] = result.X[i];
                }

                ResultPlan = plan;
                ResultFare = result.Value;
            }
        }
    }
}
