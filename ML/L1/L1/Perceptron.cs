using System.Diagnostics;

namespace L1
{
    public class PerceptronTrainParameters
    {
        public int Iterations { get; set; } = 100;

        public float LearningRate { get; set; } = 0.1f;
    }

    public class Perceptron
    {
        public float[] W { get; set; } = new float[1];

        public int Length => W.Length - 1;

        public float Estimate(Sample sample)
        {
            if (sample.Length != Length)
            {
                throw new ArgumentException("Wrong Sample length");
            }

            float wx = W[0];
            for (var i = 0; i < Length; i++)
            {
                wx += sample.X[i] * W[i + 1];
            }
            float e = F(wx);
            return e;
        }

        public IEnumerable<float> Train(IEnumerable<SampleWithClass> ds, PerceptronTrainParameters parameters)
        {
            W = new float[ds.First().Length + 1];

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
            for (int i = 0; i < parameters.Iterations; i++)
            {
                float err = 0;

                foreach(var s in ds)
                {
                    var y = Estimate(s);
                    var d = s.D;
                    var delta = d - y;
                    var deltaW = delta * parameters.LearningRate;

                    UpdateW(deltaW, s);

                    err += MathF.Pow(delta, 2) / 2;
                }

                errors.Add(err);
            }

            return errors;
        }

        #region Helper Methods

        float F(float wx)
        {
            return wx > 0 ? 1 : 0;
        }

        void UpdateW(float deltaW, Sample sample)
        {
            Debug.Assert(sample.Length == Length);

            W[0] += deltaW;
            for (var i = 0; i < Length; i++)
            {
                W[i + 1] += deltaW * sample.X[i];
            }
        }

        #endregion
    }
}
