using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace L4
{
    public partial class MainForm : Form
    {
        NLPSolver.Method CurrentMethod => m_stochasticHillClimbingButton.Checked ? NLPSolver.Method.StochasticHillClimbing : 
                                         (m_sqpMethodButton.Checked ? NLPSolver.Method.SQP : NLPSolver.Method.StochasticHillClimbing);

        public MainForm()
        {
            InitializeComponent();
        }

        #region Methods

        void Run(bool generateNewGuesses)
        {
            var rnd = new Random();

            var fn = new NLPFunction() { ScaleFactor = -1 };
            double x0 = fn.LowerBound[0], x1 = fn.UpperBound[0];
            double y0 = fn.LowerBound[1], y1 = fn.UpperBound[1];

            if (generateNewGuesses)
            {
                m_experiments.Clear();
            }

            for (var expNumber = 0; expNumber < c_experimentsCount; expNumber++)
            {
                var initialGuess = default(double[]);

                if (generateNewGuesses)
                {
                    initialGuess = new double[]
                    {
                        x0 + (x1 - x0) * rnd.NextDouble(),
                        y0 + (y1 - y0) * rnd.NextDouble()
                    };
                }
                else
                {
                    initialGuess = m_experiments[expNumber].X0;
                }

                var solver = new NLPSolver(fn, initialGuess);
                solver.AddBound(NLPSolver.BoundType.Lower, 0, x0);
                solver.AddBound(NLPSolver.BoundType.Upper, 0, x1);
                solver.AddBound(NLPSolver.BoundType.Lower, 1, y0);
                solver.AddBound(NLPSolver.BoundType.Upper, 1, y1);

                foreach(var constraint in fn.Constratins)
                {
                    solver.AddConstraint(constraint);
                }

                var expCase = new ExperimentCase()
                {
                    X0 = initialGuess,
                    Result = solver.Solve(method: CurrentMethod)
                };

                if (expCase.Result.Succeeded)
                {
                    var unscaledResult = expCase.Result;
                    unscaledResult.Value /= fn.ScaleFactor;

                    expCase.Result = unscaledResult;

                    if (generateNewGuesses)
                    {
                        m_experiments.Add(expCase);
                    }
                    else
                    {
                        m_experiments[expNumber] = expCase;
                    }
                }
            }
        }

        void UpdateControls()
        {
            // list view
            {
                m_listView.BeginUpdate();
                m_listView.Items.Clear();

                foreach (var experiment in m_experiments)
                {
                    m_listView.Items.Add(new ListViewItem(new string[] {
                                                            $"[{string.Join(", ", experiment.X0.Select(x => $"{x:G3}"))}]",
                                                            $"[{string.Join(", ", experiment.Result.X.Select(x => $"{x:G3}"))}]",
                                                            $"{experiment.Result.Value:F2}" }));
                }

                m_x0ColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_x0ColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_resultColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_resultColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_resultValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                m_resultValueColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                m_listView.EndUpdate();
            }

            // plot view
            {
                var fn = new NLPFunction();
                double x0 = fn.LowerBound[0], x1 = fn.UpperBound[0];
                double y0 = fn.LowerBound[1], y1 = fn.UpperBound[1];

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
                        Minimum = fn.LowerBound[0],
                        Maximum = fn.UpperBound[0],
                        Position = AxisPosition.Bottom,
                        IsPanEnabled = false,
                        IsZoomEnabled = false,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    };

                    var yAxis = new LinearAxis()
                    {
                        Title = "Y",
                        Minimum = fn.LowerBound[1],
                        Maximum = fn.UpperBound[1],
                        Position = AxisPosition.Left,
                        IsPanEnabled = false,
                        IsZoomEnabled = false,
                        MajorGridlineStyle = LineStyle.Solid,
                        MinorGridlineStyle = LineStyle.Dot
                    };

                    plotModel.Axes.Add(xAxis);
                    plotModel.Axes.Add(yAxis);
                }

                // constraints
                {
                    var colors = new OxyColor[] 
                    {
                        OxyColor.FromAColor(120, OxyColors.Blue),
                        OxyColor.FromAColor(120, OxyColors.Red)
                    };

                    var constraintIndex = 0;
                    foreach (var constraint in fn.Constratins)
                    {
                        var annotation = new FunctionAnnotation()
                        {
                            Equation = new Func<double, double>(x => constraint.BoundingFunction.CalcValue(new double[] { x })),
                            StrokeThickness = 3,
                            Color = colors[constraintIndex % colors.Length],
                        };

                        plotModel.Annotations.Add(annotation);

                        constraintIndex++;
                    }
                }

                // data serieses
                {
                    const int gridCount = 200;
                    const int levelCount = 15;
                    const int labelStep = 2;

                    var xCoordinates = ArrayBuilder.CreateVector(x0, x1, (x1 - x0) / gridCount);
                    var yCoordinates = ArrayBuilder.CreateVector(y0, y1, (y1 - y0) / gridCount);

                    var dataF = ArrayBuilder.Evaluate(new Func<double, double, double>((x, y) => fn.CalcValue(new double[] { x, y })),
                                                      xCoordinates,
                                                      yCoordinates);
                    var maxF = dataF.Max2D();
                    var minF = dataF.Min2D();

                    // heat map series
                    {
                        var hms = new HeatMapSeries()
                        {
                            X0 = x0,
                            X1 = x1,
                            Y0 = y0,
                            Y1 = y1,
                            Data = dataF
                        };
                        
                        var colorAxis = new LinearColorAxis()
                        {
                            Position = AxisPosition.Right,
                            Palette = OxyPalettes.Jet(500),
                        };

                        plotModel.Axes.Add(colorAxis);
                        plotModel.Series.Add(hms);
                    }

                    // contour series
                    {
                        var cs = new ContourSeries
                        {
                            ColumnCoordinates = xCoordinates,
                            RowCoordinates = yCoordinates,
                            Data = dataF,
                            ContourLevelStep = (maxF - minF) / levelCount,
                            LabelBackground = OxyColor.FromAColor(90, OxyColors.WhiteSmoke),
                            LabelStep = labelStep,
                            LabelFormatString = "F2",
                            MultiLabel = true,
                            LabelSpacing = 600,
                        };

                        plotModel.Series.Add(cs);
                    }
                }

                // results
                {
                    foreach (var experiment in m_experiments)
                    {
                        var startPoint = new PointAnnotation()
                        {
                            X = experiment.X0[0],
                            Y = experiment.X0[1],
                            Shape = MarkerType.Circle,
                            StrokeThickness = 1,
                            Stroke = OxyColors.Black,
                            Fill = OxyColors.LightGray,
                        };

                        var endPoint = new PointAnnotation()
                        {
                            X = experiment.Result.X[0],
                            Y = experiment.Result.X[1],
                            Shape = MarkerType.Triangle,
                            StrokeThickness = 1,
                            Stroke = OxyColors.Black,
                            Fill = OxyColors.White,
                            Size = 8,
                        };

                        var line = new PolylineAnnotation()
                        {
                            LineStyle = LineStyle.Dash,
                            Color = OxyColor.FromAColor(150, OxyColors.Gray),
                            StrokeThickness = 2,
                        };

                        line.Points.Add(new DataPoint(startPoint.X, startPoint.Y));
                        line.Points.Add(new DataPoint(endPoint.X, endPoint.Y));

                        plotModel.Annotations.Add(line);
                        plotModel.Annotations.Add(startPoint);
                        plotModel.Annotations.Add(endPoint);
                    }
                }

                m_plotView.Model = plotModel;
            }
        }

        #endregion

        #region Event Handlers

        private void OnRun(object sender, EventArgs e)
        {
            Run(true);
            UpdateControls();
        }

        private void OnMethodChanged(object sender, EventArgs e)
        {
            Run(false);
            UpdateControls();
        }
        
        #endregion

        #region Variables

        const int c_experimentsCount = 10;
        IList<ExperimentCase> m_experiments = new List<ExperimentCase>();

        #endregion
    }
}