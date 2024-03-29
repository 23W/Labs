﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal interface IFunction
    {
        double CalcValue(double x);
    }

    internal interface IBoundedFunction : IFunction
    {
        double UpperBound { get; }
        double LowerBound { get; }
    }

    internal interface IPolynomialFunction : IFunction
    {
        int Rank { get; }
        double[] A { get; }
    }

    internal interface ITableFunction
    {
        double[] X { get; }
        double[] Y { get; }
    }

    internal struct OptimizationResult
    {
        internal bool Succeded;
        internal double OptX;
        internal double OptF;
    }

    internal interface IFunctionOptimizer
    {
        OptimizationResult CalcMinimum();
        OptimizationResult CalcMaximum();
    }
}
