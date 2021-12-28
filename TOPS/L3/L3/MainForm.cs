using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Diagnostics;

namespace L3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region Methods

        void Run()
        {
            // Start solution
            {
                var task = new StarShipTaskProblem();
                var taskSolver = task.Build();

                m_startOptimal = taskSolver.Solve();
            }

            // 1
            //{
            //    var task = new StarShipTaskProblem();
            //    var testFeature = StarShipTaskProblem.Feature.Missiles;
            //    for (double i = 0; i < 400; i++)
            //    {
            //        task.Scores[(int)testFeature] = i;

            //        var variantSolver = task.Build();
            //        var variantRes = variantSolver.Solve();
            //        if (variantRes.Succeeded)
            //        {
            //            Debug.WriteLine($"Target: {variantRes.Value}, X: {string.Join(",", variantRes.X)}");
            //        }
            //    }
            //}

            // Price analysis
            {
                var task = new StarShipTaskProblem();
                var testFeature = StarShipTaskProblem.Feature.Missiles;

                m_startPrice = task.Prices[(int)testFeature];
                m_pricePointAnalysis.Clear();
                m_priceZoneAnalysis.Clear();

                var variantZone = default(ZoneResult);

                for (double i = 1; i <= StarShipTaskProblem.MoneyAmount; i++)
                {
                    task.Prices[(int)testFeature] = i;

                    var variantSolver = task.Build();
                    var variantRes = variantSolver.Solve();
                    if (variantRes.Succeeded)
                    {
                        if (variantZone.Equals(default(ZoneResult)))
                        {
                            variantZone = new ZoneResult() { FromPoint = i, ToPoint = i, Result = variantRes };
                        }
                        else if (!variantZone.Result.X?.SequenceEqual(variantRes.X) ?? false)
                        {
                            variantZone.ToPoint = i;
                            m_priceZoneAnalysis.Add(variantZone);

                            variantZone = new ZoneResult() { FromPoint = i, ToPoint = i, Result = variantRes };
                        }

                        m_pricePointAnalysis.Add(new PointResult() { Point = i, Result = variantRes });
                    }
                }

                variantZone.ToPoint = StarShipTaskProblem.MoneyAmount;
                m_priceZoneAnalysis.Add(variantZone);
            }
        }

        void UpdateControls()
        {
            // ListView
            {
                m_listView.BeginUpdate();

                m_listView.Items.Clear();

                for (var i = 0; i < m_startOptimal.X.Length; i++)
                {
                    m_listView.Items.Add(new ListViewItem(new string[] { $"X[{i}]", $"{m_startOptimal.X[i]:F0}" }));
                }
                m_listView.Items.Add(new ListViewItem(new string[] { $"Max(F)", $"{m_startOptimal.Value:F0}" }));


                m_dataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_dataColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_valueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_valueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_listView.EndUpdate();
            }

            // PricePlotView
            {
                var plotModel = new PlotModel() 
                { 
                    Background = OxyColors.White, 
                    PlotAreaBorderColor = OxyColors.Black 
                };

                // axes
                {
                    var xAxis = new LinearAxis()
                    { 
                        Title = "Price", 
                        Position = AxisPosition.Bottom, 
                        IsPanEnabled = false, 
                        IsZoomEnabled = false, 
                        MajorGridlineStyle = LineStyle.Solid, 
                        MinorGridlineStyle = LineStyle.Dot 
                    };

                    var yAxis = new LinearAxis()
                    {
                        Minimum = 20, 
                        Title = "Max(F)",
                        Position = AxisPosition.Left,
                        IsPanEnabled = false,
                        IsZoomEnabled = false,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    };

                    plotModel.Axes.Add(xAxis);
                    plotModel.Axes.Add(yAxis);
                }

                // line
                {
                    var lineSeries = new LineSeries() { Title = "aaa", Color = OxyColors.DarkMagenta };
                    lineSeries.Points.AddRange(m_pricePointAnalysis.Select(p => new DataPoint(p.Point, p.Result.Value)));
                
                    plotModel.Series.Add(lineSeries);
                }

                // zones
                {
                    for (var i = 0; i < m_priceZoneAnalysis.Count; i++)
                    {
                        var z = m_priceZoneAnalysis[i];
                        var zAnnotation = new RectangleAnnotation() { Layer = AnnotationLayer.BelowSeries, MinimumX = z.FromPoint, MaximumX = z.ToPoint, Fill = c_zoneColorsStrip[i % c_zoneColorsStrip.Length] };
                        plotModel.Annotations.Add(zAnnotation);
                    }

                    var optAnnotation = new LineAnnotation() { Layer = AnnotationLayer.BelowSeries, Type = LineAnnotationType.Vertical, X = m_startPrice, Color = OxyColors.Green };
                    var maxFAnnotation = new LineAnnotation() { Layer = AnnotationLayer.BelowSeries, Type = LineAnnotationType.Horizontal, Y = m_startOptimal.Value, Color = OxyColors.DarkBlue };
                    plotModel.Annotations.Add(optAnnotation);
                    plotModel.Annotations.Add(maxFAnnotation);
                 }

                m_pricePlotView.Model = plotModel;
            }
        }

        #endregion

        #region Events

        private void OnRun(object sender, EventArgs e)
        {
            Run();
            UpdateControls();
        }

        #endregion

        #region Variables

        LPSolver.Result m_startOptimal = LPSolver.Result.Empty;

        double m_startPrice = 0;
        IList<PointResult> m_pricePointAnalysis = new List<PointResult>();
        IList<ZoneResult> m_priceZoneAnalysis = new List<ZoneResult>();

        static readonly OxyColor[] c_zoneColorsStrip = new OxyColor[] 
        {
            OxyColor.FromAColor(69, OxyColors.Red),
            OxyColor.FromAColor(69, OxyColors.Orange),
            OxyColor.FromAColor(69, OxyColors.Yellow),
            OxyColor.FromAColor(69, OxyColors.Green)
        };

        #endregion
    }
}