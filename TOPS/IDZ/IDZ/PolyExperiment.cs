﻿using System;
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
        internal OptimizationResult Optimum { get; private set; } = default(OptimizationResult);

        internal void Run()
        {
            TableFunction = new TableFunction(F, X0, X1, TableRank);
            G = new PolynomialInterpolation(TableFunction);

            var minimizer = new GoldenRatioMinimizer() { F = F, X0 = X0, X1 = X1, Tolerance = 1e-8 };
            Optimum = minimizer.CalcMaximum();
        }
    }
}
