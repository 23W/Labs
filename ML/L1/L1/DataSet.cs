using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    internal class Sample
    {
        internal float[] X { get; set; } = new float[0];

        internal int Length => X.Length;
    }

    internal class SampleWithClass : Sample
    {
        internal float D = 1;
    }

    internal class DataSet
    {
        internal IEnumerable<SampleWithClass> Set => m_set;

        internal int Length => Set.Any() ? Set.First().Length : 0;

        internal void Add(SampleWithClass sample)
        {
            if (Set.Any() && Set.First().Length != sample.X.Length)
            {
                throw new ArgumentException("All samples must be equal size");
            }

            m_set.Add(sample);
        }

        internal void Add(Sample sample, float dClass)
        {
            Add(new SampleWithClass() { X = sample.X, D = dClass });
        }

        internal void Add(float[] sample, float dClass)
        {
            Add(new SampleWithClass() { X = sample, D = dClass });
        }

        #region Members

        List<SampleWithClass> m_set = new List<SampleWithClass>();

        #endregion
    }
}
