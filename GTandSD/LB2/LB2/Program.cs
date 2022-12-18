using LB2;

Console.WriteLine("Greedy Algorithm");

// Task data
var efforts = new int[,] { { 4000, 7200 }, { 2800, 6400 }, { 4800, 8000 } };
var sourceConstrains = new int[] { 1000, 1500, 1200 };
var destinationConstrains = new int[] { 2300, 1400 };

var greedyRes = Greedy.Solve((int[,])efforts.Clone(),
                             (int[])sourceConstrains.Clone(),
                             (int[])destinationConstrains.Clone());

var simplexRes = Simplex.Solve((int[,])efforts.Clone(),
                               (int[])sourceConstrains.Clone(),
                               (int[])destinationConstrains.Clone());
