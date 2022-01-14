using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class GoldenRatioMinimizer : IFunctionOptimizer
    {
        internal struct Point
        {
            internal double X;
            internal double Y;
        }

        internal IFunction? F { get; set; }
        internal double X0 { get; set;}
        internal double X1 { get; set; }
        internal double Tolerance { get; set; } = 1e-7;
        internal double GoldenRatio => (1 + Math.Sqrt(5)) / 2;

        public OptimizationResult CalcMinimum()
        {
            if (F == default)
            {
                throw new ArgumentException(nameof(F));
            }

            if (X0 >= X1)
            {
                throw new ArgumentException($"invalid properties {nameof(X0)} and {nameof(X1)}");
            }

            return Calc(true);
        }

        public OptimizationResult CalcMaximum()
        {
            if (F == default)
            {
                throw new ArgumentException(nameof(F));
            }

            if (X0 >= X1)
            {
                throw new ArgumentException($"invalid properties {nameof(X0)} and {nameof(X1)}");
            }

            return Calc(false);
        }

        OptimizationResult Calc(bool minimize)
        {
            Debug.Assert(F != default);

            var scale = minimize ? 1 : -1;
         
            var lowwer = new Point() { X = X0, Y = scale * F.CalcValue(X0) };
            var upper = new Point() { X = X1, Y = scale * F.CalcValue(X1) };
            var x_l = default(Point?);
            var x_u = default(Point?);

            while ((upper.X - lowwer.X) > Tolerance)
            {
                if (!x_l.HasValue)
                {
                    var x = upper.X - (upper.X - lowwer.X) / GoldenRatio;
                    x_l = new Point() { X = x, Y = scale * F.CalcValue(x) };
                }

                if (!x_u.HasValue)
                {
                    var x = lowwer.X + (upper.X - lowwer.X) / GoldenRatio;
                    x_u = new Point() { X = x, Y = scale * F.CalcValue(x) };
                }

                if (x_l.Value.Y >= x_u.Value.Y)
                {
                    lowwer = x_l.Value;
                    x_l = x_u;
                    x_u = default;
                }
                else
                {
                    upper = x_u.Value;
                    x_u = x_l;
                    x_l = default;
                }
            }

            var minX = (upper.X + lowwer.X) / 2;
            var res = new OptimizationResult()
            {
                Succeded = true,
                OptX = minX,
                OptF = F.CalcValue(minX)
            };

            return res;
        }
    }
}
