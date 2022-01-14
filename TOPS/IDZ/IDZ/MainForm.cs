using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
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

namespace IDZ
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void Run()
        {

        }

        void UpdateControls()
        {
            // plot view
            {
                var fn = new Function();
                double x0 = fn.LowerBound;
                double x1 = fn.UpperBound;

                var plotModel = new PlotModel()
                {
                    Background = OxyColors.White,
                    PlotAreaBorderColor = OxyColors.Black
                };

                // axes
                {
                    var xAxis = new LinearAxis()
                    {
                        Title = "X",
                        Minimum = x0,
                        Maximum = x1,
                        Position = AxisPosition.Bottom,
                        IsPanEnabled = false,
                        IsZoomEnabled = false,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    };

                    var yAxis = new LinearAxis()
                    {
                        Title = "Y",
                        Position = AxisPosition.Left,
                        IsPanEnabled = false,
                        IsZoomEnabled = false,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    };

                    plotModel.Axes.Add(xAxis);
                    plotModel.Axes.Add(yAxis);
                }

                // legend
                {
                    var l = new Legend()
                    {
                        LegendPlacement = LegendPlacement.Outside
                    };

                    plotModel.Legends.Add(l);
                }

                // functions
                {
                    const int fnSteps = 1000;

                    var fns = new FunctionSeries(x => fn.CalcValue(x), x0, x1, (x1 - x0) / fnSteps, "f(x)");
                    plotModel.Series.Add(fns);
                }

                m_plotView.Model = plotModel;
            }
        }

        private void OnRun(object sender, EventArgs e)
        {
            Run();
            UpdateControls();
        }
    }
}
