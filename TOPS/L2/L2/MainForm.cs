using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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

        void ColdStart()
        {
            // do dummy calculation at first call (avoid cold start problem)
            var f = new RosenbrockFunction();
            var x0 = new double[] { -1.2, 1.0 };

            var testPlan = new TestPlan()
            {
                Experiments = new Test[]
                {
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Simplex }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Gradient }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.Newton }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.LevenbergMarquardt }),
                    new Test(f, x0, new FunctionOptimization(f) { Method = FunctionOptimization.MinMethod.BFGS }),
                }
            };

            testPlan.Run();
        }

        void Run()
        {
            TestPlan.Run();
        }

        void UpdateControls()
        {
            // List View
            {
                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var e in TestPlan.Experiments)
                {
                    var minX = string.Join("; ", e.BestResult.MinX.Select(x => $"{x:F3}"));

                    m_listView.Items.Add(new ListViewItem(new string[] { GetMethodTitle(e.Minimizer.Method),
                                                                         $"{e.SuccededAttempts}",
                                                                         $"{e.AvgTime_ms:F3}",
                                                                         $"{e.BestResult.Steps}",
                                                                         $"{e.BestResult.MinF:G4}",
                                                                         minX }));
                }

                m_methodColumn.Width = -2;
                m_attemptsColumn.Width = -2;
                m_avgTimeColumn.Width = -2;
                m_stepsColumnt.Width = -2;
                m_minFColumn.Width = -2;
                m_minXColumn.Width = -2;

                m_listView.EndUpdate();
            }

            // Time Plot View
            {
                var plotModel = new PlotModel() { Background = OxyColors.White };

                var timeCategoryAxis = new CategoryAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
                var timeAxis = new LinearAxis { Title = "Time.avg (ms)", Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false };

                plotModel.Axes.Add(timeCategoryAxis);
                plotModel.Axes.Add(timeAxis);

                var timeSeries = new BarSeries() { FillColor = OxyColors.BlueViolet, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

                foreach (var e in TestPlan.Experiments.Reverse())
                {
                    timeSeries.Items.Add(new BarItem() { Value = e.AvgTime_ms });
                    timeCategoryAxis.Labels.Add(GetMethodTitle(e.Minimizer.Method));
                }

                plotModel.Series.Add(timeSeries);
                
                m_timePlotView.Model = plotModel;
            }

            // Steps Plot View
            {
                var plotModel = new PlotModel() { Background = OxyColors.White };

                var stepsCategoryAxis = new CategoryAxis { Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
                var stepsAxis = new LinearAxis { Title = "Steps", Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false };

                plotModel.Axes.Add(stepsCategoryAxis);
                plotModel.Axes.Add(stepsAxis);

                var stepsSeries = new BarSeries() { FillColor = OxyColors.Red, StrokeColor = OxyColors.Black, StrokeThickness = 1 };

                foreach (var e in TestPlan.Experiments.Reverse())
                {
                    stepsSeries.Items.Add(new BarItem() { Value = e.BestResult.Steps });
                    stepsCategoryAxis.Labels.Add(GetMethodTitle(e.Minimizer.Method));
                }

                plotModel.Series.Add(stepsSeries);

                m_stespPlotView.Model = plotModel;
            }
        }

        string GetMethodTitle(FunctionOptimization.MinMethod method)
        {
            var res = string.Empty;

            switch (method)
            {
                case FunctionOptimization.MinMethod.Gradient:
                    res = "Gradient";
                    break;
                case FunctionOptimization.MinMethod.BFGS:
                case FunctionOptimization.MinMethod.BFGS_B:
                    res = "BFGS";
                    break;
                case FunctionOptimization.MinMethod.Simplex:
                    res = "Simplex";
                    break;
                case FunctionOptimization.MinMethod.LevenbergMarquardt:
                    res = "Leven.-Marq.";
                    break;
                case FunctionOptimization.MinMethod.Newton:
                    res = "Newton";
                    break;
            }

            return res;
        }

        void OnRun(object sender, EventArgs e)
        {
            Run();
            UpdateControls();
        }

        void OnLoad(object sender, EventArgs e)
        {
            ColdStart();
        }
    }
}
