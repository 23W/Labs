using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class Function : IFunction
    {
        double A0 => 1;
        double A1 => 3;
        double A2 => -2;
        double A3 => 1;
        double A4 => -5;

        public double UpperBound => 1.5;
        public double LowerBound => -1;

        public double CalcValue(double x)
        {
            var y = A4 * Math.Pow(x, 4) +
                    A3 * Math.Pow(x, 3) +
                    A2 * Math.Pow(x, 2) +
                    A1 * x +
                    A0;
            return y;
        }
    }
}
