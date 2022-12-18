namespace LB2.Utilities
{
    internal static class ArrayUtilities
    {
        internal static void Output<T>(T[,] array, string linePrefix = "")
        {
            for (var i = 0; i < array.GetLength(1); i++)
            {
                Console.Write(linePrefix);

                for (var j = 0; j < array.GetLength(0); j++)
                {
                    Console.Write($"{array[j, i]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
