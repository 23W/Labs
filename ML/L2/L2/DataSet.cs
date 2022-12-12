using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2
{
    public class Sample
    {
        public double[] X { get; set; } = new double[0];

        public int Length => X.Length;
    }

    public class SampleWithClass : Sample
    {
        public int D { get; set; } = 1;
    }

    public class SampleWithClassEstimation : SampleWithClass
    {
        public int E { get; set; } = 1;
    }

    public class DataSet<T> where T : SampleWithClass, new()
    {
        public IEnumerable<T> Set => m_set;

        public int Depth => Set.Any() ? Set.First().Length : 0;

        #region Methods

        public void Add(T sample)
        {
            if (Set.Any() && Set.First().Length != sample.X.Length)
            {
                throw new ArgumentException("All samples must be equal size");
            }

            m_set.Add(sample);
        }

        public void Add(Sample sample, int dClass)
        {
            Add(new T() { X = sample.X, D = dClass });
        }

        public void Add(double[] sample, int dClass)
        {
            Add(new T() { X = sample, D = dClass });
        }

        public void AddRange(IEnumerable<T> samples)
        {
            foreach(var sample in samples)
            {
                Add(sample);
            }
        }

        public void Clear()
        {
            m_set.Clear();
        }

        public void Shuffle()
        {
            var random = new Random();

            for (int i = m_set.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                var temp = m_set[j];
                m_set[j] = m_set[i];
                m_set[i] = temp;
            }
        }

        public void Normalize()
        {
            for (var z = 0; z < Depth; z++)
            {
                var min = double.MaxValue;
                var max = double.MinValue;

                foreach (var sample in Set)
                {
                    min = Math.Min(min, sample.X[z]);
                    max = Math.Max(max, sample.X[z]);
                }

                var mean = (max + min) / 2;
                var length = (max - min);

                foreach (var sample in Set)
                {
                    var x = sample.X[z];
                    sample.X[z] = (x - mean) / length;
                }
            }
        }

        #endregion

        #region Members

        List<T> m_set = new List<T>();

        #endregion
    }
}
