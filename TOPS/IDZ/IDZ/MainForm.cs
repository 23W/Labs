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
            // poly table list view
            
            {
                m_tableListView.BeginUpdate();
                m_tableListView.Items.Clear();

                if (PolyExperiment.TableFunction != default)
                {
                    for (var p = 0; p < PolyExperiment.TableRank; p++)
                    {
                        m_tableListView.Items.Add(new ListViewItem(new string[] { $"{PolyExperiment.TableFunction.X[p]:F2}",
                                                                                  $"{PolyExperiment.TableFunction.Y[p]:F2}"}));
                    }

                    ResizeListViewColumns(m_tableListView);
                }

                m_tableListView.EndUpdate();
            }

            // lp manufacture table list view
            {
                m_manufactureListView.BeginUpdate();
                m_manufactureListView.Items.Clear();

                if (ManufactureExpariment.HasResult)
                {
                    for (var i = 0; i < ManufactureExpariment.ResultQuantity.Length; i++)
                    {
                        m_manufactureListView.Items.Add(new ListViewItem(new string[] { $"Manufacture Item #{i+1:D}", 
                                                                                        $"{ManufactureExpariment.ResultQuantity[i]:G} pcs" }));
                    }

                    m_manufactureListView.Items.Add(new ListViewItem(new string[] { $"Total income", 
                                                                                    $"{ManufactureExpariment.ResultIncome:G} golds" }));

                    ResizeListViewColumns(m_manufactureListView);
                }
                
                m_manufactureListView.EndUpdate();
            }

            // lp transport table list view
            {
                m_transportPlanListView.BeginUpdate();
                m_transportDetailsListView.BeginUpdate();

                m_transportPlanListView.Items.Clear();
                m_transportDetailsListView.Items.Clear();

                if (TransportExperiment.HasResult)
                {
                    m_transportPlanListView.Columns.Clear();
                    m_transportPlanListView.Columns.Add(new ColumnHeader() { Text = $"Depots" });

                    for (var i = 0; i < TransportExperiment.TargetCenter.Length; i++)
                    {
                        m_transportPlanListView.Columns.Add(new ColumnHeader() { Text = $"Target Center #{i + 1}" });
                    }

                    for (var d = 0; d < TransportExperiment.ResultPlan.GetLength(0); d++)
                    {
                        var dItem = new ListViewItem($"Depot #{d + 1}");
                        dItem.SubItems.AddRange(Enumerable.Range(0, TransportExperiment.ResultPlan.GetLength(1))
                                                          .Select(tc => TransportExperiment.ResultPlan[d, tc]>0 ? $"{TransportExperiment.ResultPlan[d, tc]:F0}" : "-")
                                                          .ToArray());

                        m_transportPlanListView.Items.Add(dItem);
                    }

                    m_transportDetailsListView.Items.Add(new ListViewItem(new string[] { $"Total Depot Stock", 
                                                                                         $"{TransportExperiment.TotalDepotStock:F0} pcs" }));
                    m_transportDetailsListView.Items.Add(new ListViewItem(new string[] { $"Total Target Centers Capacity",
                                                                                         $"{TransportExperiment.TotalTargetCenterStock:F0} pcs" }));
                    m_transportDetailsListView.Items.Add(new ListViewItem(new string[] { $"Transport Fare",
                                                                                         $"{TransportExperiment.ResultFare:F2} golds" }));

                    ResizeListViewColumns(m_transportPlanListView);
                    ResizeListViewColumns(m_transportDetailsListView);
                }

                m_transportPlanListView.EndUpdate();
                m_transportDetailsListView.EndUpdate();
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
                
                ResizeListViewColumns(m_polyListView);

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

                ResizeListViewColumns(m_optimizationListView);

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

        static void ResizeListViewColumns(ListView listView)
        {
            foreach (var c in listView.Columns.Cast<ColumnHeader>())
            {
                c.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                c.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void OnRun(object sender, EventArgs e)
        {
            Run();
            UpdateControls();
        }
    }
}
