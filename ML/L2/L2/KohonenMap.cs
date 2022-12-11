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

        public float LearningRate { get; set; } = 0.1f;

        public Random Random { get; set; } = new Random();
    }

    public class KohonenMapTrainResult
    {
        public IEnumerable<float> Errors { get; set; } = Enumerable.Empty<float>();
    }

    public class KohonenMap
    {
        #region Nested class

        public class Node
        {
            #region Properties

            public float[] W { get; init; } = new float[1];

            public int Length => W.Length - 1;

            public int X { get; init; } = 0;

            public int Y { get; init; } = 0;

            public Dictionary<int, int> Classes { get; init; } = new Dictionary<int, int>();

            public int MostUsedClass => GetMostUsedClass().Item1;

            public int MostUsedClassCount => GetMostUsedClass().Item2;

            public int TotalClassCount => GetTotalClassCount();

            public float Variance => GetClassVariance();

            #endregion

            #region Construction

            public Node(int x, int y, int depth)
            {
                W = new float[depth];
                Array.Fill(W, 0);

                X = x;
                Y = y;
            }

            #endregion

            #region Methods

            public float DistanceTo(Sample sample)
            {
                Debug.Assert(sample.Length == Length);

                float total = 0;

                for (int i = 0; i < Length; i++)
                {
                    var difference = W[i] - sample.X[i];
                    total += difference * difference;
                }

                return total;
            }

            public float CoordinateDistance(Node node)
            {
                int differenceInX = X - node.X;
                int differenceInY = Y - node.Y;

                return (differenceInX * differenceInX) + (differenceInY * differenceInY);
            }

            internal void UpdateWeights(Sample sample, float learningRate, float distanceFalloff)
            {
                Debug.Assert(sample.Length == Length);

                float learningRateTimesDistanceFalloff = learningRate * distanceFalloff;

                for (int index = 0; index < Length; index++)
                {
                    W[index] += (learningRateTimesDistanceFalloff * (sample.X[index] - W[index]));
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

            float GetClassVariance()
            {
                var res = 0.0f;

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

        public Node[,] Grid { get; init; } = new Node[0, 0];

        public int Width => Grid.GetLength(0);

        public int Height => Grid.GetLength(1);

        public int Depth => ((Width > 0) && (Height > 0)) ? Grid[0, 0].Length : 0;

        public Node this[int x, int y] => Grid[x, y];

        #endregion

        #region Construction

        public KohonenMap(int width, int height, int depth)
        {
            Grid = new Node[width, height];

            for (var j = 0; j < height; j++)
            {
                for (var i = 0; i < width; i++)
                {
                    Grid[i, j] = new Node(i, j, depth);
                }
            }
        }

        #endregion

        #region Methods
        public Node Estimate(Sample sample)
        {
            if (sample.Length != Depth)
            {
                throw new ArgumentException("Wrong Sample length");
            }

            var node = FindBestMatchingNode(sample);
            return node;
        }

        public KohonenMapTrainResult Train(IEnumerable<SampleWithClass> ds, KohonenMapTrainParameters parameters)
        {
            if (ds == null)
            {
                throw new ArgumentNullException(nameof(ds));
            }
            else if (!ds.Any())
            {
                throw new ArgumentException(nameof(ds));
            }

            var res = new KohonenMapTrainResult();

            InitializeWeights(ds, parameters.Random);
            TrainWeights(ds, parameters.Iterations, parameters.LearningRate);
            ClassifyNodes(ds);

            return res;
        }

        #endregion

        #region Helper Methods

        void InitializeWeights(IEnumerable<SampleWithClass> ds, Random? random = default(Random))
        {
            random ??= new Random();

            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (var sample in ds)
            {
                foreach (var x in sample.X)
                {
                    min = Math.Min(min, x);
                    max = Math.Max(max, x);
                }
            }

            float spread = max - min;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int z = 0; z < Depth; z++)
                    {
                        var w = (float)((random!.NextDouble() * spread) + min);

                        var node = Grid[i, j];

                        node.W[z] = w;
                        node.Classes.Clear();
                    }
                }
            }
        }

        void TrainWeights(IEnumerable<SampleWithClass> ds, int trainingIterations, float startLearningRate)
        {
            var latticeRadius = MathF.Max(Width, Height) / 2;
            var timeConstant = trainingIterations / MathF.Log(latticeRadius);
            var learningRate = startLearningRate;

            for (var iteration = 0; iteration < trainingIterations; iteration++)
            {
                var neighborhoodRadius = latticeRadius * MathF.Exp(-iteration / timeConstant);
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

                learningRate = startLearningRate * MathF.Exp(-(float)iteration / trainingIterations);
            }
        }

        void ClassifyNodes(IEnumerable<SampleWithClass> ds)
        {
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

        void UpdateBestMatchingNodes(Sample trainingDataToMatch, float neighborhoodDiameter, float neighborhoodRadius,
                                                                 float neighborhoodRadiusSquared, float learningRate)
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

                    // Perform a more fine-grained filter to only get the nodes within the neighborhood (circle).
                    if (distanceSquared <= neighborhoodRadiusSquared)
                    {
                        var distanceFalloff = MathF.Exp(-distanceSquared / (2 * neighborhoodRadiusSquared));
                        nodeToAdjust.UpdateWeights(trainingDataToMatch, learningRate, distanceFalloff);
                    }
                }
            }
        }

        Node FindBestMatchingNode(Sample dataToMatch)
        {
            var bestMatchingNode = Grid[0, 0];
            var bestDistance = float.MaxValue;

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
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

        #endregion
    }
}
