using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    public class KohonenMapTrainParameters
    {
        public int Iterations { get; set; } = 100;

        public double LearningRate { get; set; } = 0.02;

        public Random Random { get; set; } = new Random();
    }

    public class KohonenMapTestResult
    {
        public double QualityFactor { get; set; } = 0;

        public double ErrorFactor { get; set; } = 0;

        public int PositiveCount { get; set; } = 0;

        public int NegativeCount { get; set; } = 0;
    }

    public class KohonenMap
    {
        #region Nested class

        public class Node
        {
            #region Properties

            public double[] W { get; init; } = new double[1];

            public int Length => W.Length;

            public int X { get; init; } = 0;

            public int Y { get; init; } = 0;

            public Dictionary<int, int> Classes { get; init; } = new Dictionary<int, int>();

            public int MostUsedClass => GetMostUsedClass().Item1;

            public int MostUsedClassCount => GetMostUsedClass().Item2;

            public int TotalClassCount => GetTotalClassCount();

            public double Variance => GetClassVariance();

            #endregion

            #region Construction

            public Node(int x, int y, int depth)
            {
                W = new double[depth];
                Array.Fill(W, 0);

                X = x;
                Y = y;
            }

            #endregion

            #region Methods

            public double DistanceTo(Sample sample)
            {
                Debug.Assert(sample.Length == Length);

                double total = 0;

                for (int i = 0; i < Length; i++)
                {
                    var difference = W[i] - sample.X[i];
                    total += difference * difference;
                }

                return total;
            }

            public double DistanceTo(Node node)
            {
                Debug.Assert(node.Length == Length);

                double total = 0;

                for (int i = 0; i < Length; i++)
                {
                    var difference = W[i] - node.W[i];
                    total += difference * difference;
                }

                return total;
            }

            public double CoordinateDistance(Node node)
            {
                int differenceInX = X - node.X;
                int differenceInY = Y - node.Y;

                return (differenceInX * differenceInX) + (differenceInY * differenceInY);
            }

            internal void UpdateWeights(Sample sample, double learningRate, double distanceFalloff)
            {
                Debug.Assert(sample.Length == Length);

                double learningRateTimesDistanceFalloff = learningRate * distanceFalloff;

                for (int index = 0; index < Length; index++)
                {
                    var w = W[index];
                    W[index] = w + (learningRateTimesDistanceFalloff * (sample.X[index] - w));
                }
            }

            #endregion

            #region Helper Methods

            Tuple<int, int> GetMostUsedClass()
            {
                var res = Tuple.Create(-1, 0);

                foreach (var c in Classes)
                {
                    if (c.Value > res.Item2)
                    {
                        res = Tuple.Create(c.Key, c.Value);
                    }
                }

                return res;
            }

            int GetTotalClassCount()
            {
                var total = 0;

                foreach(var c in Classes)
                {
                    total += c.Value;
                }

                return total;
            }

            double GetClassVariance()
            {
                var res = 0.0;

                if (Classes.Count > 1)
                {
                    var mean = GetMostUsedClass();

                    foreach (var c in Classes)
                    {
                        res += ((c.Key != mean.Item1) ? 1.0f : 0.0f) * c.Value / TotalClassCount;
                    }
                }

                return res;
            }

            #endregion
        }

        #endregion

        #region Properties

        public Node[,] Grid { get; private set; } = new Node[0, 0];

        public int Width => Grid.GetLength(0);

        public int Height => Grid.GetLength(1);

        public int Depth => ((Width > 0) && (Height > 0)) ? Grid[0, 0].Length : 0;
        public double MaxW {get; private set; }
        public double MinW {get; private set; }

        public Node this[int x, int y] => Grid[x, y];

        #endregion

        #region Methods

        public void Init(int width, int height, int depth)
        {
            Grid = new Node[width, height];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    Grid[i, j] = new Node(i, j, depth);
                }
            }
        }

        public Node Estimate(Sample sample)
        {
            if (sample.Length != Depth)
            {
                throw new ArgumentException("Wrong Sample length");
            }

            var node = FindBestMatchingNode(sample);
            return node;
        }

        public Sample EstimateSample(IEnumerable<Sample> samples, int x, int y)
        {
            var node = Grid[x, y];
            var sample = FindBestMatchingSample(samples, node);
            return sample;
        }

        public void Train(IEnumerable<SampleWithClass> ds, KohonenMapTrainParameters parameters)
        {
            if (!ds.Any())
            {
                throw new ArgumentException(nameof(ds));
            }

            InitializeWeights(ds, parameters.Random);
            TrainWeights(ds, parameters.Iterations, parameters.LearningRate);
            ClassifyNodes(ds);
        }

        public KohonenMapTestResult Test(IEnumerable<SampleWithClass> trainSet, IEnumerable<SampleWithClassEstimation> testSet)
        {
            var res = new KohonenMapTestResult();

            foreach (var test in testSet)
            {
                var node = FindBestMatchingNode(test);
                var estimation = node.MostUsedClass;

                if (!node.Classes.Any())
                {
                    var trainSample = FindBestMatchingSample(trainSet, node) as SampleWithClass;
                    estimation = trainSample!.D;
                }

                test.E = estimation;
                if (test.E != test.D)
                {
                    res.NegativeCount++;

                    var errorValue = 1.0;
                    if (node.Classes.ContainsKey(test.D) && node.Classes.ContainsKey(test.E))
                    {
                        var freq = node.Classes[test.D];
                        var total = node.Classes[test.E];

                        errorValue = 1 - ((double)freq / total);
                    }

                    res.ErrorFactor += errorValue;
                }
                else
                {
                    res.PositiveCount++;
                }
            }

            res.QualityFactor = 1 - res.ErrorFactor / (res.PositiveCount + res.NegativeCount);
            return res;
        }

        #endregion

        #region Helper Methods

        void InitializeWeights(IEnumerable<SampleWithClass> ds, Random? random = default(Random))
        {
            random ??= new Random();

            double min = double.MaxValue;
            double max = double.MinValue;

            foreach (var sample in ds)
            {
                foreach (var x in sample.X)
                {
                    min = Math.Min(min, x);
                    max = Math.Max(max, x);
                }
            }

            double spread = max - min;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var node = Grid[i, j];

                    for (int z = 0; z < Depth; z++)
                    {
                        var w = (double)((random!.NextDouble() * spread) + min);
                        node.W[z] = w;
                    }
                }
            }

            MaxW = max;
            MinW = min;
        }

        IEnumerable<double> TrainWeights(IEnumerable<SampleWithClass> ds, int trainingIterations, double startLearningRate)
        {
            var errors = new List<double>();

            var latticeRadius = Math.Max(Width, Height) / 2;
            var timeConstant = trainingIterations / Math.Log(latticeRadius);
            var learningRate = startLearningRate;

            for (var iteration = 0; iteration < trainingIterations; iteration++)
            {
                learningRate = startLearningRate * Math.Exp(-(double)iteration / trainingIterations);

                var neighborhoodRadius = latticeRadius * Math.Exp(-iteration / timeConstant);
                var neighborhoodDiameter = neighborhoodRadius * 2;
                var neighborhoodRadiusSquared = neighborhoodRadius * neighborhoodRadius;

                foreach (var sample in ds)
                {
                    UpdateBestMatchingNodes(sample,
                                            neighborhoodDiameter,
                                            neighborhoodRadius,
                                            neighborhoodRadiusSquared,
                                            learningRate);
                }
            }

            return errors;
        }

        void ClassifyNodes(IEnumerable<SampleWithClass> ds)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var node = Grid[i, j];
                    node.Classes.Clear();
                }
            }

            foreach (var sample in ds)
            {
                var node = Estimate(sample);

                if (node.Classes.ContainsKey(sample.D))
                {
                    node.Classes[sample.D] += 1;
                }
                else
                {
                    node.Classes.Add(sample.D, 1);
                }
            }
        }

        void UpdateBestMatchingNodes(Sample trainingDataToMatch, double neighborhoodDiameter, double neighborhoodRadius,
                                                                 double neighborhoodRadiusSquared, double learningRate)
        {
            Debug.Assert(trainingDataToMatch.Length == Depth);

            var bestMatchingNode = FindBestMatchingNode(trainingDataToMatch);

            // Calculate the bounds of the neighborhood.
            int startX = (int)Math.Max(0, bestMatchingNode.X - neighborhoodRadius - 1);
            int startY = (int)Math.Max(0, bestMatchingNode.Y - neighborhoodRadius - 1);
            int endX = (int)Math.Min(Width, startX + neighborhoodDiameter + 1);
            int endY = (int)Math.Min(Height, startY + neighborhoodDiameter + 1);

            for (int i = startX; i < endX; i++)
            {
                for (int j = startY; j < endY; j++)
                {
                    // Get the node to adjust.
                    var nodeToAdjust = Grid[i, j];
                    var distanceSquared = bestMatchingNode.CoordinateDistance(nodeToAdjust);

                    // Check neighborhood circle distance.
                    if (distanceSquared <= neighborhoodRadiusSquared)
                    {
                        var distanceFalloff = Math.Exp(-distanceSquared / (2 * neighborhoodRadiusSquared));
                        nodeToAdjust.UpdateWeights(trainingDataToMatch, learningRate, distanceFalloff);
                    }
                }
            }
        }

        Node FindBestMatchingNode(Sample dataToMatch)
        {
            var bestMatchingNode = Grid[0, 0];
            var bestDistance = double.MaxValue;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var currentNode = Grid[i, j];
                    var currentDistance = currentNode.DistanceTo(dataToMatch);

                    if (currentDistance < bestDistance)
                    {
                        bestMatchingNode = currentNode;
                        bestDistance = currentDistance;
                    }
                }
            }

            return bestMatchingNode;
        }

        Sample FindBestMatchingSample(IEnumerable<Sample> ds, Node node)
        {
            var bestMatchingSample = ds.First();
            var bestDistance = double.MaxValue;

            foreach(var sample in ds)
            {
                var currentDistance = node.DistanceTo(sample);
                if (currentDistance < bestDistance)
                {
                    bestMatchingSample = sample;
                    bestDistance = currentDistance;
                }
            }

            return bestMatchingSample;
        }

        #endregion
    }
}
