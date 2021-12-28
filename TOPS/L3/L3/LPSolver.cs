using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterSpace.NMath.Core;


namespace L3
{
    public class LPSolver
    {
        public enum ConstrainType
        {
            Upper,
            Lower,
            Equal
        }

        public enum BoundType
        {
            Upper,
            Lower,
        }

        public struct Result
        {
            public bool Succeeded;
            public double[] X;
            public double Value;

            public static Result Empty => new Result() { Succeeded = false, X = new double[] { }, Value =0 };
        }

        LinearProgrammingProblem LPProblem { get; init; }

        public LPSolver(double[] c)
        {
            if (c?.Length == 0)
            {
                throw new ArgumentNullException("Invalid vector C", nameof(c));
            }

            LPProblem = new LinearProgrammingProblem(new DoubleVector(c));
        }

        public void AddConstraint(ConstrainType type, double[] a, double b)
        {
            if (a?.Length == 0)
            {
                throw new ArgumentNullException("Invalid vector A", nameof(a));
            }

            switch (type)
            {
                case ConstrainType.Upper:
                    LPProblem.AddUpperBoundConstraint(new DoubleVector(a), b);
                    break;
                case ConstrainType.Lower:
                    LPProblem.AddLowerBoundConstraint(new DoubleVector(a), b);
                    break;
                case ConstrainType.Equal:
                    LPProblem.AddEqualityConstraint(new DoubleVector(a), b);
                    break;
                default:
                    throw new ArgumentException("invalid type", nameof(type));
            }
        }

        public void AddBound(BoundType type, int xIndex, double bound)
        {
            if (xIndex < 0 || xIndex >= LPProblem.NumVariables)
            {
                throw new ArgumentNullException("Invalid value of xIndex", nameof(xIndex));
            }

            switch (type)
            {
                case BoundType.Upper:
                    LPProblem.AddUpperBound(xIndex, bound);
                    break;
                case BoundType.Lower:
                    LPProblem.AddLowerBound(xIndex, bound);
                    break;
                default:
                    throw new ArgumentException("invalide type", nameof(type));
            }
        }

        public Result Solve(bool minimaze = false)
        {
            var res = Result.Empty;

            var solver = new PrimalSimplexSolverORTools();

            // Solve
            solver.Solve(LPProblem, minimaze);

            res.Succeeded = solver.Result == ConstrainedOptimizerORTools.SolveResult.Optimal;
            if (res.Succeeded)
            {
                res.X = solver.OptimalX.ToArray();
                res.Value = solver.OptimalObjectiveFunctionValue;
            }
            
            return res;
        }
    }
}
