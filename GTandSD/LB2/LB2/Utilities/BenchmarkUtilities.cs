using System.Diagnostics;

namespace LB2.Utilities
{
    internal static class BenchmarkUtilities
    {
        internal static Tuple<T, TimeSpan> Measure<T>(Func<T> alorithm, int attempts = 10)
        {
            if (attempts <= 0)
            {
                throw new ArgumentException(nameof(attempts));
            }

            T? res = default;
            var duration = default(TimeSpan);

            for (var attempt = 0; attempt < attempts; attempt++)
            {
                var st = new Stopwatch();
                st.Start();

                res = alorithm();

                st.Stop();

                var attemptDuration = st.Elapsed;
                if (duration == default || attemptDuration < duration)
                {
                    duration = attemptDuration;
                }
            }

            return Tuple.Create(res!, duration);
        }
    }
}
