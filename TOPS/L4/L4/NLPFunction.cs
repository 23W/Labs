﻿namespace L4
{
    internal abstract class NLPConstraint : IConstarint
    {
        public abstract double RightHandValue { get; }

        public abstract NLPSolver.ConstraintType ConstrainType { get; }

        public abstract IFunction BoundingFunction { get; }

        public abstract string Name { get; }

        public abstract double CalcValue(double[] x);
    }

    internal class NLPFunction : IFunction
    {
        #region Constraints

        internal class Constraint1 : NLPConstraint
        {
            public override double RightHandValue => 0;

            public override NLPSolver.ConstraintType ConstrainType => NLPSolver.ConstraintType.Lower;

            public override IFunction BoundingFunction => IFunctionWrapper.Make(new Func<double[], double>(x => -20 + 250 / x[0]));

            public override string Name => "y - 250/x + 20 ≥ 0";

            public override double CalcValue(double[] x)
            {
                if (x?.Length != 2)
                {
                    throw new ArgumentException("X sould be two order", nameof(x));
                }

                var f = x[1] - (250.0 / x[0]) + 20.0;
                return f;
            }
        }

        internal class Constraint2 : NLPConstraint
        {
            public override double RightHandValue => 0;

            public override NLPSolver.ConstraintType ConstrainType => NLPSolver.ConstraintType.Lower;

            public override IFunction BoundingFunction => IFunctionWrapper.Make(new Func<double[], double>(x => (7.0 / 8.0) * (x[0] - 10.0)));

            public override string Name => "y - 7/8 * (x - 10) ≥ 0";

            public override double CalcValue(double[] x)
            {
                if (x?.Length != 2)
                {
                    throw new ArgumentException("X sould be two order", nameof(x));
                }

                var f = x[1] - (7.0 / 8.0) * (x[0] - 10.0);
                return f;
            }
        }

        #endregion

        public double ScaleFactor = 1;

        public double[] LowerBound => new double[] { 1, 1 };

        public double[] UpperBound => new double[] { 80, 80 };

        public IEnumerable<NLPConstraint> Constratins => new NLPConstraint[] {new Constraint1(), new Constraint2() }; 

        public double CalcValue(double[] x)
        {
            if (x?.Length != 2)
            {
                throw new ArgumentException("X sould be two order", nameof(x));
            }

            var f = ScaleFactor * x[0] * (Math.Sin(0.3 * x[1] - 5) - Math.Cos(0.3 * x[0] + 5));
            return f;
        }
    }
}
