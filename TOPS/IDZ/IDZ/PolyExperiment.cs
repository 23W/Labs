using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class PolyExperiment
    {
        internal IBoundedFunction F => new Function();
        internal double X0 => F.LowerBound;
        internal double X1 => F.UpperBound;
        internal int TableRank { get; set; } = 4;
        internal ITableFunction? TableFunction { get; private set; } = default(ITableFunction);
        internal IFunction? G { get; private set; } = default(IFunction);
        internal double StdDev { get; private set; }
        internal int StdDevQuality => 1000;
        internal OptimizationResult Optimum { get; private set; } = default(OptimizationResult);

        internal void Run()
        {
            // polynom interpolation
            TableFunction = new TableFunction(F, X0, X1, TableRank);
            G = new PolynomialInterpolation(TableFunction);

            // standard deviation of intrepolation
            var stdTableFunction = new TableFunction(F, X0, X1, StdDevQuality);
            var cumulativeError = 0.0;
            for (var r = 0; r < StdDevQuality; r++)
            {
                cumulativeError += Math.Pow(stdTableFunction.Y[r] - G.CalcValue(stdTableFunction.X[r]), 2);
            }
            StdDev = Math.Sqrt(cumulativeError / StdDevQuality);

            // maximum value
            var minimizer = new GoldenRatioMinimizer() { F = F, X0 = X0, X1 = X1, Tolerance = 1e-8 };
            Optimum = minimizer.CalcMaximum();
        }
    }
}
