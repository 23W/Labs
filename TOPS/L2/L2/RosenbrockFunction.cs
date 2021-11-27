using System;
using System.Diagnostics;

namespace L2
{
    public class RosenbrockFunction : IFunctionWithGradient
    {
        public int N => 2;

        public double CalcValue(double[] x)
        {
            Debug.Assert(x.Length == N);

            var y = Math.Pow(1.0 - x[0], 2) + 100.0 * Math.Pow(x[1] - x[0] * x[0], 2);
            return y;
        }

        public double[] CalcGradient(double[] x)
        {
            Debug.Assert(x.Length == N);

            var prime = new double[N];
            for (int i = 0; i < N; i++)
            {
                prime[0] = 400.0 * x[0] * x[0] * x[0] - 400.0 * x[0] * x[1] + 2.0 * x[0] - 2.0;
                prime[1] = 200.0 * (x[1] - x[0] * x[0]);
            }

            return prime;
        }
    }
}
