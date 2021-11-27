using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnRun(object sender, EventArgs e)
        {
            var f = new RosenbrockFunction();
            var x0 = new double[] { -1.2, 1.0 };

            var lm_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.LevenbergMarquardt, Tolerance = 1e-15, MaxIterations = 10000 };
            var lm_min = lm_opt.FindMin(x0);

            var g_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Gradient };
            var g_min = g_opt.FindMin(x0);

            var s_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Simplex };
            var s_min = s_opt.FindMin(x0);
        }
    }
}
