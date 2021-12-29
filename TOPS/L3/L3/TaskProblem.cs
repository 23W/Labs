namespace L3
{
    interface ITaskProblem
    {
        LPSolver Build();
    }

    struct PointResult
    {
        internal double Point;
        internal LPSolver.Result Result = LPSolver.Result.Empty;
    }

    struct ZoneResult
    {
        internal double FromPoint;
        internal double ToPoint;
        internal LPSolver.Result Result = LPSolver.Result.Empty;
    }
}
