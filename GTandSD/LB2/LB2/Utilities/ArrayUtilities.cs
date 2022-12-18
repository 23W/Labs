namespace LB2.Utilities
{
    internal static class ArrayUtilities
    {
        internal static void Output<T>(T[,] array, string linePrefix = "")
        {
            for (var j = 0; j < array.GetLength(0); j++)
            {
                Console.Write(linePrefix);

                for (var i = 0; i < array.GetLength(1); i++)
                {
                    Console.Write($"{array[j, i]} ");
                }

                Console.WriteLine();
            }
        }

        internal static T DotProcution<T>(T[,] a, T[,] b, Func<T, T, T> multiplier, Func<T, T, T> additor) where T: struct
        {
            T res = default(T);

            for (var j = 0; j < a.GetLength(0); j++)
            {
                for (var i = 0; i < a.GetLength(1); i++)
                {
                    res = additor(res, multiplier(a[j, i], b[j, i]));
                }
            }

            return res;
        }

        internal static int DotProcution(int[,] a, int[,] b)
        {
            int res = DotProcution(a, b,
                                  (a, b) => a * b,
                                  (a, b) => a + b);
            return res;
        }
    }
}
