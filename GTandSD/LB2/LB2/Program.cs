using LB2;
using LB2.Utilities;


// Benchmark attempts
const int attempts = 100;

// Task data
var efforts = new int[,] { { 4000, 5900 }, 
                           {  900, 5400 }, 
                           { 3200, 6000 } };
var sourceConstrains = new int[] { 100, 150, 550 };
var destinationConstrains = new int[] { 600, 200 };


Console.WriteLine("Greedy Algorithm:");

var greedyRes = BenchmarkUtilities.Measure(() => Greedy.Solve((int[,])efforts.Clone(),
                                                              (int[])sourceConstrains.Clone(),
                                                              (int[])destinationConstrains.Clone()), attempts);

Console.WriteLine($"\tDuration: {greedyRes.Item2.TotalMilliseconds} ms");
Console.WriteLine("\tResult:");
ArrayUtilities.Output(greedyRes.Item1, "\t\t");


Console.WriteLine("Simplex Algorithm:");

var simplexRes = BenchmarkUtilities.Measure(() => Simplex.Solve((int[,])efforts.Clone(),
                                                                (int[])sourceConstrains.Clone(),
                                                                (int[])destinationConstrains.Clone()), attempts);
Console.WriteLine($"\tDuration: {simplexRes.Item2.TotalMilliseconds} ms");
Console.WriteLine("\tResult:");
ArrayUtilities.Output(simplexRes.Item1, "\t\t");
