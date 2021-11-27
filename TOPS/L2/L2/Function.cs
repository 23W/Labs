using System;
using System.Diagnostics;

namespace L2
{
    class Function : IFunctionWithHessian
    {
        #region Properties

        public int N { get; init; } = 0;

        public double[] X0 => CalcInitialGuess();

        #endregion

        #region Construction

        public Function(int n)
        {
            N = n;
        }

        #endregion

        #region Interface Implementation

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

        public double[,] CalcHessian(double[] x)
        {
            Debug.Assert(x.Length == N);

            var h = new double[N, N];
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)
                {
                    h[i, j] = 0;
                }
            }

            for (var i = 0; i < N; i++)
            {
                h[i, i] = CalcSecondPartialDeriviate(i + 1, x[i]);
            }

            return h;
        }

        #endregion

        #region Helper Methods

        double CalcElement(int i, double x)
        {
            var r = Math.Pow((i * x - N), 2 * i) / (10 * i * i * i);
            return r;
        }

        double CalcPartialDeriviate(int i, double x)
        {
            var d = (Math.Pow((i * x) - N, (2 * i) - 1)) / (5.0 * i);

            if (!double.IsFinite(d))
            {
                Debug.WriteLine("Issue: non finite partial deriviate!");
            }

            return d;
        }

        double CalcSecondPartialDeriviate(int i, double x)
        {
            var d = ((2 * i - 1) * Math.Pow((i * x) - N, (2 * i) - 2)) / 5;

            if (!double.IsFinite(d))
            {
                Debug.WriteLine("Issue: non finite second partial deriviate!");
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

        #endregion
    }
}
