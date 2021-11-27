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
            var f = new Function(5);
            var x0 = f.X0;

            var lm_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.LevenbergMarquardt, Tolerance = 1e-15 };
            var lm_min = lm_opt.FindMin(x0);

            var n_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Newton };
            var n_min = n_opt.FindMin(x0);

            var g_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Gradient };
            var g_min = g_opt.FindMin(x0);

            var s_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Simplex };
            var s_min = s_opt.FindMin(x0);

            var b_opt = new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.BFGS };
            var b_min = s_opt.FindMin(x0);
        }
    }
}
