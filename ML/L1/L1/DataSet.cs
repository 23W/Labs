using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    public class Sample
    {
        public float[] X { get; set; } = new float[0];

        public int Length => X.Length;
    }

    public class SampleWithClass : Sample
    {
        public float D { get; set; } = 1;
    }

    public class SampleWithClassEstimation : SampleWithClass
    {
        public float E { get; set; } = 1;
    }

    public class DataSet<T> where T : SampleWithClass, new()
    {
        public IEnumerable<T> Set => m_set;

        public int Length => Set.Any() ? Set.First().Length : 0;

        public void Add(T sample)
        {
            if (Set.Any() && Set.First().Length != sample.X.Length)
            {
                throw new ArgumentException("All samples must be equal size");
            }

            m_set.Add(sample);
        }

        public void Add(Sample sample, float dClass)
        {
            Add(new T() { X = sample.X, D = dClass });
        }

        public void Add(float[] sample, float dClass)
        {
            Add(new T() { X = sample, D = dClass });
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

        #region Members

        List<T> m_set = new List<T>();

        #endregion
    }
}
