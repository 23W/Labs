using System.Diagnostics;

namespace L1
{
    internal class Perceptron
    {
        internal float[] W { get; set; } = new float[1];

        internal int Length => W.Length - 1;

        internal float Estimate(Sample sample)
        {
            if (sample.X.Length != Length)
            {
                throw new ArgumentException("Wrong Sample length");
            }

            float wx = sample.X.Zip(W.Skip(1), (x, w) => w * x).Sum() + W[0];
            float e = F(wx);
            return e;
        }

        internal IEnumerable<float> Train(DataSet ds, float iterations = 100, float learningRate = 0.1f)
        {
            W = new float[ds.Length + 1];

            // initial W
            {
                var rnd = new Random();
                for (int i = 0; i < W.Length; i++)
                {
                    W[i] = (float)rnd.NextDouble();
                }
            }

            // train
            var errors = new List<float>();
            for (int i = 0; i < iterations; i++)
            {
                float err = 0;

                foreach(var s in ds.Set)
                {
                    var y = Estimate(s);
                    var d = s.D;
                    var delta = d - y;
                    var deltaW = delta * learningRate;

                    UpdateW(deltaW, s);

                    err += MathF.Pow(delta, 2);
                }

                errors.Add(err);
            }

            return errors;
        }

        #region Helper Methods

        float F(float wx)
        {
            return wx >= 0 ? 1 : -1;
        }

        void UpdateW(float deltaW, Sample sample)
        {
            Debug.Assert(sample.Length == Length);

            W[0] = deltaW;
            for (var i = 1; i < W.Length; i++)
            {
                W[i] += deltaW * sample.X[i];
            }
        }

        #endregion
    }
}
