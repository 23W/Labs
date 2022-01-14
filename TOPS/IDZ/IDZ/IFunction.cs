using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal interface IFunction
    {
        double UpperBound { get; }
        double LowerBound { get; }

        double CalcValue(double x);

    }
}
