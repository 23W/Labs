using CenterSpace.NMath.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class PolynomialInterpolation : IPolynomialFunction
    {
        internal double[] X { get; init; }
        internal double[] Y { get; init; }
        public int Rank => X.Length;
        public double[] A => CalcA();

        internal PolynomialInterpolation(ITableFunction tf)
            : this(tf.X, tf.Y)
        {
        }

        internal PolynomialInterpolation(double[] x, double[] y)
        {
            if (x.Length != y.Length || 
                x.Length <= 1)
            {
                throw new ArgumentException($"invalid arguments {nameof(x)} and {nameof(y)}");
            }

            X = x;
            Y = y;
        }

        public double CalcValue(double x)
        {
            var a = A;
            var y = 0.0;

            for (var r = 0; r < Rank; r++)
            {
                y += a[r] * Math.Pow(x, r);
            }

            return y;
        }

        double[] CalcA()
        {
            bool isCalculated = m_a?.Any() ?? false;
            if (!isCalculated)
            {
                var t = new DoubleMatrix(Rank, Rank);
                for (var r = 0; r < Rank; r++)
                {
                    for (var c = 0; c < Rank; c++)
                    {
                        t[r, c] = Math.Pow(X[r], c);
                    }
                }

                var y = new DoubleMatrix(Rank, 1);
                for (var r = 0; r < Rank; r++)
                {
                    y[r, 0] = Y[r];
                }
                
                var a = NMathFunctions.Product(NMathFunctions.Inverse(t), y);
                m_a = a.Col(0).ToArray();
            }

            return m_a ?? Array.Empty<double>();
        }

        double[] m_a = Array.Empty<double>();
    }
}
