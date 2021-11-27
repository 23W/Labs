using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace L2
{
    class Test
    {
        #region Properties

        internal IFunction F { get; init; }
        internal double[] X0 { get; init; }

        internal FunctionOptimization Minimizer { get; init; }

        internal int Times { get; set; } = 10;
        internal int SuccededAttempts { get; private set; }
        internal FunctionOptimization.MinResult BestResult { get; private set; }
        internal double AvgTime_ms { get; private set; }

        #endregion

        #region Construction

        internal Test(IFunction f, double[] x0, FunctionOptimization minimizer)
        {
            F = f;
            X0 = x0;
            Minimizer = minimizer;
        }

        #endregion

        #region Methods

        internal void Run()
        {
            var clocks = new List<double>();
            var results = new List<FunctionOptimization.MinResult>();

            for (var t = 0; t < Times; t++)
            {
                var sw = new Stopwatch();
                sw.Start();

                var currentRes = Minimizer.FindMin(X0);

                sw.Stop();

                if (currentRes.Succeded)
                {
                    results.Add(currentRes);
                    clocks.Add(sw.Elapsed.TotalMilliseconds);
                }
            }

            if (results.Any())
            {
                SuccededAttempts = results.Count;
                AvgTime_ms = clocks.Average();
                BestResult = results.Aggregate((min, next) => min.MinF < next.MinF ? min : next);
            }
        }

        #endregion
    }

    class TestPlan
    {
        #region Properties

        internal IEnumerable<Test> Experiments { get; init; }

        #endregion

        #region Methods

        internal void Run()
        {
            foreach(var e in Experiments)
            {
                e.Run();
            }
        }

        #endregion
    }
}
