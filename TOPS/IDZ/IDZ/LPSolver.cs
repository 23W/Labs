using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CenterSpace.NMath.Core;

namespace IDZ
{
    internal class LPSolver
    {
        public enum LPType
        {
            Double,
            Integer,
        }

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

            public static Result Empty => new Result() { Succeeded = false, X = new double[] { }, Value = 0 };
        }

        LinearProgrammingProblem LPProblem { get; init; }
        LPType LPProblemType { get; init; }

        public LPSolver(double[] c, LPType type = LPType.Double)
        {
            if (c?.Length == 0)
            {
                throw new InvalidArgumentException(nameof(c));
            }

            switch (type)
            {
                case LPType.Double:
                    LPProblem = new LinearProgrammingProblem(new DoubleVector(c));
                    LPProblemType = type;
                    break;
                case LPType.Integer:
                    LPProblem = new MixedIntegerLinearProgrammingProblem(new DoubleVector(c));
                    LPProblemType = type;
                    break;
                default:
                    throw new InvalidArgumentException(nameof(type));
            }
        }

        public void AddConstraint(ConstrainType type, double[] a, double b)
        {
            if (a?.Length == 0)
            {
                throw new InvalidArgumentException(nameof(a));
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
                    throw new InvalidArgumentException(nameof(type));
            }
        }

        public void AddBound(BoundType type, int xIndex, double bound)
        {
            if (xIndex < 0 || xIndex >= LPProblem.NumVariables)
            {
                throw new InvalidArgumentException(nameof(xIndex));
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
                    throw new InvalidArgumentException(nameof(type));
            }
        }

        public void AddIntegralConstraint(int xIndex)
        {
            if (xIndex < 0 || xIndex >= LPProblem.NumVariables)
            {
                throw new InvalidArgumentException(nameof(xIndex));
            }

            if (LPProblemType == LPType.Integer)
            {
                (LPProblem as MixedIntegerLinearProgrammingProblem)?.SetIntegrality(xIndex, true);
            }
        }

        public Result Solve(bool minimaze = false)
        {
            var res = Result.Empty;

            switch (LPProblemType)
            {
                case LPType.Double:
                    {
                        var solver = new PrimalSimplexSolverORTools();
                        solver.Solve(LPProblem, minimaze);

                        res.Succeeded = solver.Result == ConstrainedOptimizerORTools.SolveResult.Optimal;
                        if (res.Succeeded)
                        {
                            res.X = solver.OptimalX.ToArray();
                            res.Value = solver.OptimalObjectiveFunctionValue;
                        }
                    }
                    break;

                case LPType.Integer:
                    {
                        var solver = new DualSimplexSolverORTools();
                        solver.Solve(LPProblem as MixedIntegerLinearProgrammingProblem, minimaze);

                        res.Succeeded = solver.Result == ConstrainedOptimizerORTools.SolveResult.Optimal;
                        if (res.Succeeded)
                        {
                            res.X = solver.OptimalX.ToArray();
                            res.Value = solver.OptimalObjectiveFunctionValue;
                        }
                    }
                    break;
            }

            return res;
        }
    }
}
