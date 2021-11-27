using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.Optimization.ObjectiveFunctions;

namespace L2
{
    class FunctionOptimization
    {
        public struct MinResult
        {
            public double MinF;
            public double[] MinX;

            public int Steps;
            public bool Succeded;
        }

        public enum MinMethod
        {
            Gradient,
            BFGS,
            BFGS_B,
            Simplex,
            LevenbergMarquardt
        }

        public IFunction F { get; init; } = default(IFunction);
        public double Tolerance { get; set; } = 1e-8;
        public int MaxIterations { get; set; } = 1000000;
        public bool UseFGradient { get; set; } = true;
        public MinMethod Method { get; set; } = MinMethod.Gradient;

        public FunctionOptimization(IFunction f)
        {
            F = f;
        }

        public MinResult FindMin(double[] x0, double[] lowerBound = default(double[]), double[] upperBound = default(double[]))
        {
            MinResult min;

            var objective = BuildObjectiveFunction(F, x0.Length, UseFGradient, lowerBound, upperBound);

            try
            {
                switch (Method)
                {
                    case MinMethod.Gradient:
                        {
                            var alg = new ConjugateGradientMinimizer(Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective, CreateVector.DenseOfArray(x0));

                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.BFGS:
                        {
                            var alg = new BfgsMinimizer(Tolerance, Tolerance, Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective,
                                                         CreateVector.DenseOfArray(x0));


                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.BFGS_B:
                        {
                            var alg = new BfgsBMinimizer(Tolerance, Tolerance, Tolerance, MaxIterations);
                            var result = alg.FindMinimum(objective,
                                                         SafeBounds(lowerBound, x0.Length, -1),
                                                         SafeBounds(upperBound, x0.Length, 1),
                                                         CreateVector.DenseOfArray(x0));


                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.Simplex:
                        {
                            var result = NelderMeadSimplex.Minimum(objective, CreateVector.DenseOfArray(x0), Tolerance, MaxIterations);

                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    case MinMethod.LevenbergMarquardt:
                        {
                            if (!(F is IFunctionWithGradient))
                            {
                                throw new ArgumentException("Function must support gradient!", nameof(F));
                            }

                            var modelF = new LevenbergMarquardtModel(F as IFunctionWithGradient, x0.Length);
                            var objectiveModel = ObjectiveFunction.NonlinearModel(modelF.Model, modelF.DerivativesDerivatives, modelF.ModelX, modelF.ModelY);

                            var result = LevenbergMarquardtMinimizer.Minimum(objectiveModel, CreateVector.DenseOfArray(x0),
                                                                                             OptionalBounds(lowerBound),
                                                                                             OptionalBounds(upperBound),
                                                                                             gradientTolerance: Tolerance,
                                                                                             stepTolerance: Tolerance,
                                                                                             functionTolerance: Tolerance,
                                                                                             maximumIterations: MaxIterations);

                            min.MinX = result.MinimizingPoint.ToArray();
                            min.MinF = F.CalcValue(min.MinX);
                            min.Steps = result.Iterations;
                            min.Succeded = result.ReasonForExit != ExitCondition.None &&
                                           result.ReasonForExit != ExitCondition.InvalidValues &&
                                           result.ReasonForExit != ExitCondition.ExceedIterations;
                            break;
                        }
                    default:
                        min = default(MinResult);
                        min.Succeded = false;
                        break;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Exception: {e.Message}");

                min = default(MinResult);
                min.Succeded = false;
            }

            return min;
        }

        static Vector<double> OptionalBounds(double[] bound)
        {
            return (bound != default(double[])) ? CreateVector.DenseOfArray(bound) : null;
        }

        static Vector<double> SafeBounds(double[] bound, int n, float scale = 1)
        {
            if (bound == default(double[]))
            {
                bound = new double[n];
                for (var i = 0; i < bound.Length; i++)
                {
                    bound[i] = (double.MaxValue - 1) * scale;
                }
            }

            return CreateVector.DenseOfArray(bound);
        }

        static IObjectiveFunction BuildObjectiveFunction(IFunction f, int n, bool ensureGradient = false, double[] lowerBound = default(double[]), double[] upperBound = default(double[]))
        {
            var objective = default(IObjectiveFunction);

            if ((f is IFunctionWithGradient) && ensureGradient)
            {
                var gf = f as IFunctionWithGradient;

                objective = ObjectiveFunction.Gradient(x => gf.CalcValue(x.ToArray()),
                                                       x => CreateVector.DenseOfArray(gf.CalcGradient(x.ToArray())));
            }
            else
            {
                objective = ObjectiveFunction.Value(x => f.CalcValue(x.ToArray()));
                objective = new ForwardDifferenceGradientObjectiveFunction(objective,
                                                                           SafeBounds(lowerBound, n, -1),
                                                                           SafeBounds(upperBound, n, 1));
            }

            return objective;
        }
    }

    class LevenbergMarquardtModel
    {
        internal IFunctionWithGradient F { get; init; }
        internal int N { get; init; }

        internal Vector<double> ModelX => Vector<double>.Build.Dense(N);
        internal Vector<double> ModelY => Vector<double>.Build.Dense(N);

        internal LevenbergMarquardtModel(IFunctionWithGradient f, int n)
        {
            Debug.Assert(n > 0);

            F = f;
            N = n;
        }

        internal Vector<double> Model(Vector<double> p, Vector<double> x)
        {
            var y = CreateVector.Dense<double>(x.Count);
            var pa = p.ToArray();

            for (int i = 0; i < x.Count; i++)
            {
                y[i] = F.CalcValue(pa);
            }
            
            return y;
        }

        internal Matrix<double> DerivativesDerivatives(Vector<double> p, Vector<double> x)
        {
            var derivatives = Matrix<double>.Build.Dense(x.Count, p.Count);
            var pa = p.ToArray();

            for (int i = 0; i < x.Count; i++)
            {
                var g = F.CalcGradient(pa);
                for (int gi = 0; gi < g.Length; gi++)
                {
                    derivatives[i, gi] = g[gi];
                }
            }
            return derivatives;
        }


    }
}
