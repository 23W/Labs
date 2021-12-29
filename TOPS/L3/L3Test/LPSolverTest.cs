using Microsoft.VisualStudio.TestTools.UnitTesting;
using L3;

namespace L3Test
{
    [TestClass]
    public class LPSolverTest
    {
        [TestMethod]
        public void SimpleSolution()
        {
            var lp = new LPSolver(new double[] { -1.0, 1.0 });
            lp.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 1.0, 2.0 }, 10.0);
            lp.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 2.0, 1.0 }, 10.0);
            lp.AddBound(LPSolver.BoundType.Lower, 0, 0.0);
            lp.AddBound(LPSolver.BoundType.Lower, 1, 0.0);
            var res = lp.Solve();

            Assert.IsTrue(res.Succeeded);
            Assert.AreEqual(5.0, res.Value, 1e-6);
            Assert.AreEqual(0.0, res.X[0], 1e-6);
            Assert.AreEqual(5.0, res.X[1], 1e-6);
        }

        [TestMethod]
        public void DocExamplTest()
        {
            // Currently, wheat is $3.38/bushel. The farmer can expect a yield of 55 bushels/acre.
            double wheatPrice = 3.38;
            double wheatYield = 55.0;
            double wheatRevenuePerAcre = wheatPrice * wheatYield;

            // Currently, barley is $1.98/bushel. The farmer can expect a yield of 75 bushels/acre.
            double barleyPrice = 1.98;
            double barleyYield = 75.0;
            double barleyRevenuePerAcre = barleyPrice * barleyYield;

            // Currently, corn is $1.70/bushel. The farmer can expect a yield of 110 bushels/acre.
            double cornPrice = 1.70;
            double cornYield = 110.0;
            double cornRevenuePerAcre = cornPrice * cornYield;

            var p = new double[] { wheatRevenuePerAcre, barleyRevenuePerAcre, cornRevenuePerAcre };
            var lpProblem = new LPSolver(p);

            // The farmer has 8,000 lbs of nitrogen fertilizer. It's known that wheat requires
            // 12 lb/acre, barley 5 lb/acre and corn 22 lb/acre.
            lpProblem.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 12.0, 5.0, 22.0 }, 8000.0);

            // The farmer has 22,000 lbs of phosphate fertilizer. It's known that wheat requires
            // 30 lb/acre, barley 8 lb/acre and corn 50 lb/acre.
            lpProblem.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 30.0, 8.0, 50.0 }, 22000.0);

            // The farmer has a permit for 1,000 acre-feet of water. Wheat requires 1.5 ft of water, 
            // barley requires 0.7 and corn 2.2.
            lpProblem.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 1.5, 0.7, 2.2 }, 1200.0);

            // The farmer has a maximum of 640 acres for planting.
            lpProblem.AddConstraint(LPSolver.ConstrainType.Upper, new double[] { 1.0, 1.0, 1.0 }, 640.0);


            lpProblem.AddBound(LPSolver.BoundType.Lower, 0, 0.0);
            lpProblem.AddBound(LPSolver.BoundType.Lower, 1, 0.0);
            lpProblem.AddBound(LPSolver.BoundType.Lower, 2, 0.0);

            // Solve
            var res = lpProblem.Solve();
            Assert.IsTrue(res.Succeeded);
            Assert.AreEqual(608.0, res.X[0], 1e-6);
            Assert.AreEqual(0.0, res.X[1], 1e-6);
            Assert.AreEqual(32.0, res.X[2], 1e-6);
        }
    }
}