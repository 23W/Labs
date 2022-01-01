namespace L4
{
    internal class NLPFunction : IFunction
    {
        #region Constraints

        internal class Constraint1 : IFunction
        {
            public double RightHandValue => 0;

            public NLPSolver.ConstrainType ConstrainType => NLPSolver.ConstrainType.Lower;

            public double CalcValue(double[] x)
            {
                if (x?.Length != 2)
                {
                    throw new ArgumentException("X sould be two order", nameof(x));
                }

                var f = x[1] - (250.0 / x[0]) + 20.0;
                return f;
            }
        }

        internal class Constraint2 : IFunction
        {
            public double RightHandValue => 0;

            public NLPSolver.ConstrainType ConstrainType => NLPSolver.ConstrainType.Lower;

            public double CalcValue(double[] x)
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

        public double[] LowerBound => new double[] { 1, 1 };

        public double[] UpperBound => new double[] { 80, 80 };

        public IEnumerable<IFunction> Constratins => new IFunction[] {new Constraint1(), new Constraint2() }; 

        public double CalcValue(double[] x)
        {
            if (x?.Length != 2)
            {
                throw new ArgumentException("X sould be two order", nameof(x));
            }

            var f = (-1) * x[0] * (Math.Sin(0.3 * x[1] - 5) - Math.Cos(0.3 * x[0] + 5));
            return f;
        }
    }
}
