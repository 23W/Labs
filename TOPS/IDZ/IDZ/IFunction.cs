using System;
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

    internal interface ITableFunction
    {
        double[] X { get; }
        double[] Y { get; }
    }
}
