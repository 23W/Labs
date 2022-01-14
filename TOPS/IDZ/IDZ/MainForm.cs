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
        PolyExperiment PolyExperiment = new PolyExperiment();
        LPManufactureExpariment ManufactureExpariment = new LPManufactureExpariment();
        LPTransportExperiment TransportExperiment = new LPTransportExperiment();

        public MainForm()
        {
            InitializeComponent();
        }

        void Run()
        {
            PolyExperiment.Run();
            ManufactureExpariment.Run();
            TransportExperiment.Run();
        }

        void UpdateControls()
        {
            // table list view
            if (PolyExperiment.TableFunction != default)
            {
                m_tableListView.BeginUpdate();
                m_tableListView.Items.Clear();

                for (var p = 0; p < PolyExperiment.TableRank; p++)
                {
                    m_tableListView.Items.Add(new ListViewItem(new string[] { $"{PolyExperiment.TableFunction.X[p]:F2}",
                                                                              $"{PolyExperiment.TableFunction.Y[p]:F2}"}));
                }

                m_tableXColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_tableXColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_tableYColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_tableYColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_tableListView.EndUpdate();
            }

            // polinomial list view
            if (PolyExperiment.G != default)
            {
                m_polyListView.BeginUpdate();
                m_polyListView.Items.Clear();

                for (var p = 0; p < PolyExperiment.G.Rank; p++)
                {
                    m_polyListView.Items.Add(new ListViewItem(new string[] { $"A{p}",
                                                                             $"{PolyExperiment.G.A[p]:F3}"}));
                }

                m_polyListView.Items.Add(new ListViewItem(new string[] { $"σ",
                                                                         $"{PolyExperiment.StdDev:F3}"}));
                
                m_polyDataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_polyDataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_polyValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_polyValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_polyListView.EndUpdate();
            }

            // optimization list view
            {
                m_optimizationListView.BeginUpdate();
                m_optimizationListView.Items.Clear();

                m_optimizationListView.Items.Add(new ListViewItem(new string[] { $"OptX",
                                                                                 $"{PolyExperiment.Optimum.OptX:F2}"}));
                m_optimizationListView.Items.Add(new ListViewItem(new string[] { $"OptF",
                                                                                 $"{PolyExperiment.Optimum.OptF:F2}"}));
                m_optimizationListView.Items.Add(new ListViewItem(new string[] { $"ε",
                                                                                 $"{PolyExperiment.OptimumQuality:G}"}));

                m_optimizationDataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_optimizationDataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                m_optimizationValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_optimizationValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_optimizationListView.EndUpdate();
            }

            // plot view
            {
                var plotModel = new PlotModel()
                {
                    Background = OxyColors.White,
                    PlotAreaBorderColor = OxyColors.Black
                };

                // axes
                {
                    var margin = (PolyExperiment.X1 - PolyExperiment.X0) * 0.01;

                    var xAxis = new LinearAxis()
                    {
                        Title = "X",
                        Minimum = PolyExperiment.X0 - margin,
                        Maximum = PolyExperiment.X1 + margin,
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
                    double dx = (PolyExperiment.X1 - PolyExperiment.X0) / fnSteps;

                    var fns = new FunctionSeries(x => PolyExperiment.F.CalcValue(x), PolyExperiment.X0, PolyExperiment.X1, dx, "f(x)");
                    plotModel.Series.Add(fns);

                    // polynom
                    if (PolyExperiment.G != default)
                    {
                        var polyFn = PolyExperiment.G;

                        var pfns = new FunctionSeries(x => polyFn.CalcValue(x), PolyExperiment.X0, PolyExperiment.X1, dx, "g(x)")
                        {
                            Color = OxyColor.FromAColor(150, OxyColors.Purple),
                            LineStyle = LineStyle.Dash,

                        };
                        plotModel.Series.Add(pfns);
                    }
                }

                // points
                if (PolyExperiment.TableFunction != default)
                {
                    for (var p = 0; p < PolyExperiment.TableRank; p++)
                    {
                        var point = new PointAnnotation()
                        {
                            X = PolyExperiment.TableFunction.X[p],
                            Y = PolyExperiment.TableFunction.Y[p],
                            Shape = MarkerType.Circle,
                            StrokeThickness = 1,
                            Stroke = OxyColors.Black,
                            Fill = OxyColors.LightGray,
                        };

                        plotModel.Annotations.Add(point);
                    }
                }

                // minimum
                if (PolyExperiment.Optimum.Succeded)
                {
                    var line = new LineAnnotation()
                    {
                        Type = LineAnnotationType.Vertical,
                        X = PolyExperiment.Optimum.OptX,
                        Layer = AnnotationLayer.BelowSeries,
                    };

                    var point = new PointAnnotation()
                    {
                        X = PolyExperiment.Optimum.OptX,
                        Y = PolyExperiment.Optimum.OptF,
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
