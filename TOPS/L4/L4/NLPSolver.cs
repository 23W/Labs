using CenterSpace.NMath.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4
{
    public class NLPSolver
    {
        public enum ConstrainType
        {
            Upper,  // <= constrain
            Lower,  // >= constrain
            Equal   // == constrain
        }

        public enum BoundType
        {
            Upper,
            Lower,
        }

        public enum Method
        {
            StochasticHillClimbing,
            SQP,
        }

        public struct Result
        {
            public bool Succeeded;
            public double[] X;
            public double Value;

            public static Result Empty => new Result() { Succeeded = false, X = new double[] { }, Value = 0 };
        }

        NonlinearProgrammingProblem NLPProblem { get; init; }
        DoubleVector X0 { get; init; }

        public NLPSolver(Func<double[], double> func, double[] x0)
        {
            if (x0?.Length == 0)
            {
                throw new InvalidArgumentException(nameof(x0));
            }

            X0 = new DoubleVector(x0);
            NLPProblem = new NonlinearProgrammingProblem(X0.Length, new Func<DoubleVector, double>(x => func(x.ToArray())));
        }

        public void AddBound(BoundType type, int xIndex, double bound)
        {
            if (xIndex < 0 || xIndex >= NLPProblem.NumVariables)
            {
                throw new InvalidArgumentException(nameof(xIndex));
            }

            switch (type)
            {
                case BoundType.Upper:
                    NLPProblem.AddUpperBound(xIndex, bound);
                    break;
                case BoundType.Lower:
                    NLPProblem.AddLowerBound(xIndex, bound);
                    break;
                default:
                    throw new InvalidArgumentException(nameof(type));
            }
        }

        public void AddConstraint(ConstrainType type, Func<double[], double> constraint, double rightHandValue = 0)
        {
            if (constraint == null)
            {
                throw new InvalidArgumentException(nameof(constraint));
            }

            switch (type)
            {
                case ConstrainType.Upper:
                    NLPProblem.AddUpperBoundConstraint(X0.Length, new Func<DoubleVector, double>(x => constraint(x.ToArray())), rightHandValue);
                    break;
                case ConstrainType.Lower:
                    NLPProblem.AddLowerBoundConstraint(X0.Length, new Func<DoubleVector, double>(x => constraint(x.ToArray())), rightHandValue);
                    break;
                case ConstrainType.Equal:
                    NLPProblem.AddEqualityConstraint(X0.Length, new Func<DoubleVector, double>(x => constraint(x.ToArray())), rightHandValue);
                    break;
                default:
                    throw new InvalidArgumentException(nameof(type));
            }
        }

        public Result Solve(Method method = Method.StochasticHillClimbing)
        {
            var res = Result.Empty;

            switch (method)
            {
                case Method.StochasticHillClimbing:
                    {
                        var solver = new StochasticHillClimbingSolver();
                        solver.Solve(NLPProblem, X0);

                        res.Succeeded = solver.Result == ConstrainedOptimizer.SolveResult.Optimal;
                        if (res.Succeeded)
                        {
                            res.X = solver.OptimalX.ToArray();
                            res.Value = solver.OptimalObjectiveFunctionValue;
                        }
                    }
                    break;

                case Method.SQP:
                    {
                        var solver = new ActiveSetLineSearchSQP();
                        res.Succeeded = solver.Solve(NLPProblem, X0);
                        if (res.Succeeded)
                        {
                            res.X = solver.OptimalX.ToArray();
                            res.Value = solver.OptimalObjectiveFunctionValue;
                        }
                    }
                    break;

                default:
                    throw new InvalidArgumentException(nameof(method));
            }

            return res;
        }
    }
}
