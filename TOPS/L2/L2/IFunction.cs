namespace L2
{
    interface IFunction
    {
        double CalcValue(double[] x);
    }

    interface IFunctionWithGradient : IFunction
    {
        double[] CalcGradient(double[] x);
    }

    interface IFunctionWithHessian : IFunctionWithGradient
    {
        double[,] CalcHessian(double[] x);
    }
}
