using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4
{
    internal struct ExperimentCase
    {
        internal double[] X0 { get; set; } = Array.Empty<double>();

        internal NLPSolver.Result Result { get; set; } = NLPSolver.Result.Empty;
    }
}
