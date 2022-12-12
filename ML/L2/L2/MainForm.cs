using CsvHelper;
using CsvHelper.Configuration;
using L2.Utils;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Diagnostics;
using System.Globalization;
using static L2.KohonenMap;

namespace L2
{
    public partial class MainForm : Form
    {
        public int SOMWidth { get; set; } = 10;

        public int SOMHeight { get; set; } = 10;

        public KohonenMapTrainParameters TrainParameters { get; init; } = new KohonenMapTrainParameters();

        public KohonenMap Map { get; init; } = new KohonenMap();

        SortedSet<string> MapClasses { get; init; } = new SortedSet<string>();

        static MarkerType[] MapClassMarkes { get; } = new MarkerType[]
        {
            MarkerType.Circle,
            MarkerType.Square,
            MarkerType.Diamond,
            MarkerType.Triangle,
            MarkerType.Star,
        };

        DataSet<SampleWithClass> TrainDataSet { get; set; } = new DataSet<SampleWithClass>();

        DataSet<SampleWithClassEstimation> TestDataSet { get; set; } = new DataSet<SampleWithClassEstimation>();

        List<OxyPlot.WindowsForms.PlotView> PlotViews { get; init; } = new List<OxyPlot.WindowsForms.PlotView>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnTrain(object sender, EventArgs e)
        {
            // Load datat set
            var dataSet = new DataSet<SampleWithClass>();
            {
                TrainDataSet.Clear();
                TestDataSet.Clear();

                var dataSetFile = "Data\\cars1.csv";

                var config = new CsvConfiguration(CultureInfo.InvariantCulture);

                using (var reader = new StreamReader(dataSetFile))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Data.CarData>().ToList();

                    MapClasses.Clear();
                    foreach (var record in records)
                    {
                        MapClasses.Add(record.Type);
                    }

                    foreach (var record in records)
                    {
                        dataSet.Add(new double[]{ record.EngineSize,
                                                  record.Cylinders,
                                                  record.Horsepower,
                                                  record.MPG_City,
                                                  record.MPG_Highway,
                                                  record.Weight,
                                                  record.Wheelbase,
                                                  record.Length },
                                    MapClasses.FindIndex(record.Type));
                    }

                    dataSet.Normalize();
                    dataSet.Shuffle();

                    var trainCount = (dataSet.Set.Count() * 4) / 5;
                    TrainDataSet.AddRange(dataSet.Set.Take(trainCount));

                    foreach (var set in dataSet.Set.Skip(trainCount))
                    {
                        TestDataSet.Add(set.X, set.D);
                    }
                 }
            }

            // Train
            Map.Init(SOMWidth, SOMHeight, TrainDataSet.Depth);
            Map.Train(TrainDataSet.Set, TrainParameters);
            //Map.Train(dataSet.Set, TrainParameters);

            // Test
            var testResult = Map.Test(TrainDataSet.Set, TestDataSet.Set);

            // Draw result
            {
                var tableW = 3;
                var tableH = (int)Math.Ceiling((Map.Depth + 1) / (float)tableW);

                m_tableLayoutPanel.ColumnCount = tableW;
                m_tableLayoutPanel.RowCount = tableH;
                m_tableLayoutPanel.ColumnStyles.Clear();
                m_tableLayoutPanel.RowStyles.Clear();
                m_tableLayoutPanel.Controls.Clear();

                PlotViews.ForEach(view => view.Dispose());
                PlotViews.Clear();

                for (var wh = 0; wh < tableW; wh++)
                {
                    m_tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f/tableW));
                }

                for (var wh = 0; wh < tableH; wh++)
                {
                    m_tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / tableH));
                }

                for (var d = 0; d < Map.Depth; d++)
                {
                    var model = new PlotModel();

                    var colorAxis = new LinearColorAxis()
                    {
                        Position = AxisPosition.Right,
                        Palette = OxyPalettes.Jet(500),
                        Maximum = Map.MaxW,
                        Minimum = Map.MinW,
                        IsAxisVisible = false,
                    };

                    model.Axes.Add(colorAxis);

                    var data = new double[Map.Width, Map.Height];
                    for (var x = 0; x < Map.Width; x++)
                    {
                        for (var y = 0; y < Map.Height; y++)
                        {
                            data[x, y] = Map[x, y].W[d];
                        }
                    }

                    var series = new HeatMapSeries()
                    {
                        CoordinateDefinition = HeatMapCoordinateDefinition.Center,
                        X0 = 1.0,
                        X1 = Map.Width + 0.0,
                        Y0 = 1.0,
                        Y1 = Map.Height + 0.0,
                        Data = data,
                        Interpolate = false,
                    };
                    model.Series.Add(series);

                    // annotations
                    {
                        for (var x = 0; x < Map.Width; x++)
                        {
                            var annotation = new LineAnnotation()
                            {
                                Type = LineAnnotationType.Vertical,
                                X = x + 0.5,
                                Layer = AnnotationLayer.AboveSeries,
                            };
                            model.Annotations.Add(annotation);
                        }

                        for (var y = 0; y < Map.Height; y++)
                        {
                            var annotation = new LineAnnotation()
                            {
                                Type = LineAnnotationType.Horizontal,
                                Y = y + 0.5,
                                Layer = AnnotationLayer.AboveSeries,
                            };
                            model.Annotations.Add(annotation);
                        }

                        for (var x = 0; x<Map.Width; x++)
                        {
                            for(var y = 0; y<Map.Height; y++)
                            {
                                var node = Map[x, y];
                                if (node.Classes.Any())
                                {
                                    var ptAnnotation = new PointAnnotation()
                                    {
                                        X = x + 1,
                                        Y = y + 1,
                                        Shape = MapClassMarkes[node.MostUsedClass % MapClassMarkes.Length],
                                        Fill = OxyColors.WhiteSmoke,
                                        Stroke = OxyColors.Black,
                                        StrokeThickness = 1,
                                        //Text = $"{node.W[d]:0.##}",
                                        //FontSize = 6,
                                        //FontWeight = FontWeights.Bold,
                                        //TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                                        //TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                                    };
                                    model.Annotations.Add(ptAnnotation);
                                }
                            }
                        }
                    }

                    var plotView = new PlotView();
                    m_tableLayoutPanel.Controls.Add(plotView, d % tableW, d / tableW);

                    plotView.Dock = DockStyle.Fill;
                    plotView.Model = model;
                }

                {
                    var model = new PlotModel();

                    model.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Bottom,
                        Maximum = 120,
                        IsAxisVisible = false,
                        IsPanEnabled = false,
                    });
                    model.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Left,
                        IsAxisVisible = false,
                        IsPanEnabled = false,
                    });

                    var classIndex = 0;
                    foreach (var className in MapClasses)
                    {
                        var ptAnnotation = new PointAnnotation()
                        {
                            X = 10,
                            Y = 10 + (classIndex * 10),
                            Shape = MapClassMarkes[classIndex % MapClassMarkes.Length],
                            Fill = OxyColors.WhiteSmoke,
                            Stroke = OxyColors.Black,
                            StrokeThickness = 1,
                            Text = $"{className}",
                            FontSize = 10,
                            FontWeight = FontWeights.Bold,
                            TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                            TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                        };
                        model.Annotations.Add(ptAnnotation);

                        classIndex++;
                    }

                    var qualityAnnotation = new PointAnnotation()
                    {
                        X = 40,
                        Y = 10,
                        Shape = MarkerType.None,
                        Text = $"Quality: {(int)(testResult.QualityFactor * 100)} %",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                    };
                    model.Annotations.Add(qualityAnnotation);

                    var pnAnnotation = new PointAnnotation()
                    {
                        X = 40,
                        Y = 20,
                        Shape = MarkerType.None,
                        Text = $"Positive per Negative: {testResult.PositiveCount}/{testResult.NegativeCount}",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                    };
                    model.Annotations.Add(pnAnnotation);

                    var testAnnotation = new PointAnnotation()
                    {
                        X = 40,
                        Y = 30,
                        Shape = MarkerType.None,
                        Text = $"Test Dataset length: {TestDataSet.Set.Count()}",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                    };
                    model.Annotations.Add(testAnnotation);

                    var trainAnnotation = new PointAnnotation()
                    {
                        X = 40,
                        Y = 40,
                        Shape = MarkerType.None,
                        Text = $"Train Dataset length: {TrainDataSet.Set.Count()}",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        TextVerticalAlignment = OxyPlot.VerticalAlignment.Middle,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left,
                    };
                    model.Annotations.Add(trainAnnotation);

                    var plotView = new PlotView();
                    var graphIndex = Map.Depth;
                    m_tableLayoutPanel.Controls.Add(plotView, graphIndex % tableW, graphIndex / tableW);

                    plotView.Dock = DockStyle.Fill;
                    plotView.Model = model;
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            m_iterationsTextBox.DataBindings.Add("Text", this, $"{nameof(TrainParameters)}.{nameof(TrainParameters.Iterations)}");
            m_learningRateTextBox.DataBindings.Add("Text", this, $"{nameof(TrainParameters)}.{nameof(TrainParameters.LearningRate)}");
            m_gridWidthTextBox.DataBindings.Add("Text", this, $"{nameof(SOMWidth)}");
            m_gridHeightTextBox.DataBindings.Add("Text", this, $"{nameof(SOMHeight)}");
        }
    }
}