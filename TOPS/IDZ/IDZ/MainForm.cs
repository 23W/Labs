using OxyPlot;
using OxyPlot.Annotations;
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
        PolyExperiment Experiment = new PolyExperiment();

        public MainForm()
        {
            InitializeComponent();
        }

        void Run()
        {
            Experiment.Run();
        }

        void UpdateControls()
        {
            // plot view
            {
                var plotModel = new PlotModel()
                {
                    Background = OxyColors.White,
                    PlotAreaBorderColor = OxyColors.Black
                };

                // axes
                {
                    var margin = (Experiment.X1 - Experiment.X0) * 0.01;

                    var xAxis = new LinearAxis()
                    {
                        Title = "X",
                        Minimum = Experiment.X0 - margin,
                        Maximum = Experiment.X1 + margin,
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
                    // function
                    const int fnSteps = 500;
                    double dx = (Experiment.X1 - Experiment.X0) / fnSteps;

                    var fns = new FunctionSeries(x => Experiment.F.CalcValue(x), Experiment.X0, Experiment.X1, dx, "f(x)");
                    plotModel.Series.Add(fns);

                    // polynom
                    if (Experiment.G != default)
                    {
                        var polyFn = Experiment.G;

                        var pfns = new FunctionSeries(x => polyFn.CalcValue(x), Experiment.X0, Experiment.X1, dx, "g(x)")
                        {
                            Color = OxyColor.FromAColor(150, OxyColors.Purple),
                            LineStyle = LineStyle.Dash,

                        };
                        plotModel.Series.Add(pfns);
                    }
                }

                // points
                if (Experiment.TableFunction != default)
                {
                    for (var p = 0; p < Experiment.TableRank; p++)
                    {
                        var point = new PointAnnotation()
                        {
                            X = Experiment.TableFunction.X[p],
                            Y = Experiment.TableFunction.Y[p],
                            Shape = MarkerType.Circle,
                            StrokeThickness = 1,
                            Stroke = OxyColors.Black,
                            Fill = OxyColors.LightGray,
                        };

                        plotModel.Annotations.Add(point);
                    }
                }

                // minimum
                if (Experiment.Optimum.Succeded)
                {
                    var line = new LineAnnotation()
                    {
                        Type = LineAnnotationType.Vertical,
                        X = Experiment.Optimum.OptX,
                        Layer = AnnotationLayer.BelowSeries,
                    };

                    var point = new PointAnnotation()
                    {
                        X = Experiment.Optimum.OptX,
                        Y = Experiment.Optimum.OptF,
                        Shape = MarkerType.Triangle,
                        StrokeThickness = 1,
                        Stroke = OxyColors.Black,
                        Fill = OxyColors.White,
                    };

                    plotModel.Annotations.Add(line);
                    plotModel.Annotations.Add(point);
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
