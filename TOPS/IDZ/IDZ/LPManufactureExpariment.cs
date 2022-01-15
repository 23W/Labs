using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    internal class LPManufactureExpariment
    {
        internal int Variant => 8;
        internal double A => 0.1 * Variant + 2;

        internal double Cardboard1 => 2 + A;
        internal double Cardboard2 => 8 - A;
        internal double CardboardAmount => 360;

        internal double LaborCost1 => 6 - A;
        internal double LaborCost2 => 4;
        internal double LaborCostAmount => 192;

        internal double Energy1 => 2;
        internal double Energy2 => 3 + A;
        internal double EnergyAmount => 180;

        internal double Income1 => 12;
        internal double Income2 => 16;

        internal bool HasResult { get; private set; } = false;
        internal double[] ResultQuantity { get; private set; } = Array.Empty<double>();
        internal double ResultIncome { get; private set; } = 0;

        internal void Run()
        {
            var solver = new LPSolver(new double[] { Income1, Income2 }, LPSolver.LPType.Integer);

            solver.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { Cardboard1, Cardboard2 }, CardboardAmount);
            solver.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { LaborCost1, LaborCost2 }, LaborCostAmount);
            solver.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { Energy1, Energy2}, EnergyAmount);

            solver.AddBound(LPSolver.BoundType.Lower, 0, 0);
            solver.AddBound(LPSolver.BoundType.Lower, 1, 0);

            solver.AddIntegralConstraint(0);
            solver.AddIntegralConstraint(1);

            var result = solver.Solve();
            HasResult = result.Succeeded;

            if (result.Succeeded)
            {
                ResultQuantity = result.X;
                ResultIncome = result.Value;
            }
        }
    }
}
