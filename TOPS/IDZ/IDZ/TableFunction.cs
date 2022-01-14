using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class TableFunction : ITableFunction
    {
        public double[] X { get; private set; } = Array.Empty<double>();
        public double[] Y { get; private set; } = Array.Empty<double>();

        internal TableFunction(IFunction fn, double x0, double x1, double dx)
        {
            var list = new List<Tuple<double, double>>();

            var step = 0;
            for (var x = x0; x < x1; x = x0 + (dx * ++step))
            {
                var y = fn.CalcValue(x);
                list.Add(new Tuple<double, double>(x, y));
            }

            X = list.Select(t => t.Item1).ToArray();
            Y = list.Select(t => t.Item2).ToArray();
        }

        internal TableFunction(IFunction fn, double x0, double x1, int count)
        {
            X = new double[count];
            Y = new double[count];

            for (var i = 0; i < count; i++)
            {
                var x = x0 + ((x1 - x0) * i) / (count-1);
                var y = fn.CalcValue(x);

                X[i] = x;
                Y[i] = y;
            }
        }
    }
}
