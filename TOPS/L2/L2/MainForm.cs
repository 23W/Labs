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
        TestPlan TestPlan { get; init; }

        public MainForm()
        {
            var f = new Function(5);
            var x0 = f.X0;

            TestPlan = new TestPlan() { Experiments = new Test[] 
                {
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Simplex }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Gradient }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Newton }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.LevenbergMarquardt, Tolerance = 1e-15 }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.BFGS }),
                }};

            InitializeComponent();
        }

        void Run()
        {
            TestPlan.Run();
        }

        void UpdateControls()
        {
        }

        void OnRun(object sender, EventArgs e)
        {
            Run();
            UpdateControls();
        }
    }
}
