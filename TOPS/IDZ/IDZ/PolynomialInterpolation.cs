using CenterSpace.NMath.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class PolynomialInterpolation : IFunction
    {
        internal double[] X { get; init; }
        internal double[] Y { get; init; }
        internal int Range => X.Length;
        internal double[] A => CalcA();

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

            for (var r = 0; r < Range; r++)
            {
                y += a[r] * Math.Pow(x, r);
            }

            return y;
        }

        double[] CalcA()
        {
            if (!m_a?.Any() ?? true)
            {
                var t = new DoubleMatrix(Range, Range);
                for (var r = 0; r < Range; r++)
                {
                    for (var c = 0; c < Range; c++)
                    {
                        t[r, c] = Math.Pow(X[r], c);
                    }
                }

                var y = new DoubleMatrix(Range, 1);
                for (var r = 0; r < Range; r++)
                {
                    y[r, 0] = Y[r];
                }
                
                var a = NMathFunctions.Product(NMathFunctions.Inverse(t), y);
                m_a = a.Col(0).ToArray();
            }

            return m_a;
        }

        double[] m_a = Array.Empty<double>();
    }
}
