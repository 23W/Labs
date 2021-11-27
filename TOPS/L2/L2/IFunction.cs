namespace L2
{
    public interface IFunction
    {
        double CalcValue(double[] x);
    }

    public interface IFunctionWithGradient : IFunction
    {
        double[] CalcGradient(double[] x);
    }

    public interface IFunctionWithHessian : IFunctionWithGradient
    {
        double[,] CalcHessian(double[] x);
    }
}
