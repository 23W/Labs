using MathNet.Numerics.Optimization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    class Function : IFunctionWithGradient
    {
        public int N { get; init; } = 0;

        public double[] X0 => CalcInitialGuess();

        public Function(int n)
        {
            N = n;
        }

        public double CalcValue(double[] x)
        {
            Debug.Assert(x.Length == N);

            double s = 0;
            for (var i = 0; i < N; i++)
            {
                s += CalcElement(i + 1, x[i]);
            }

            return s;
        }

        public double[] CalcGradient(double[] x)
        {
            Debug.Assert(x.Length == N);

            var g = new double[x.Length];
            for (var i = 0; i < N; i++)
            {
                g[i] = CalcPartialDeriviate(i + 1, x[i]);
            }

            return g;
        }

        double CalcElement(int i, double x)
        {
            //var r = Math.Pow((i * x - N), 2 * i) / (10.0 * Math.Pow(i, 3));
            var r = Math.Pow((i * x - N), 2 * i) / (10 * i * i * i);
            return r;
        }

        double CalcPartialDeriviate(int i, double x)
        {
            //var d1 = (2 * i * i * Math.Pow((i * x) - N, (2 * i) - 1)) / (10.0 * Math.Pow(i, 3));
            var d = (Math.Pow((i * x) - N, (2 * i) - 1)) / (5.0 * i);

            if (!double.IsFinite(d))
            {
                Debug.WriteLine("Issue: non finite partial deriviate!");
            }

            return d;
        }

        double[] CalcInitialGuess()
        {
            var x0 = new double[N];
            for (var i = 0; i < N; i++)
            {
                x0[i] = (i + 1) / 10.0;
            }

            return x0;
        }
    }
}
