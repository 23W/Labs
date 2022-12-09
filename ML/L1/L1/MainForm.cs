using CsvHelper;
using CsvHelper.Configuration;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;

namespace L1
{
    public partial class MainForm : Form
    {
        public Perceptron Perceptron { get; init; } = new Perceptron();
        public PerceptronTrainParameters TrainParameters { get; init; } = new PerceptronTrainParameters();
        DataSet<SampleWithClass> TrainDataSet { get; init; } = new DataSet<SampleWithClass>();
        DataSet<SampleWithClassEstimation> TestDataSet { get; init; } = new DataSet<SampleWithClassEstimation>();
        float DecisionBoundarySlope { get; set; } = 0;
        float DecisionBoundaryIntercept { get; set; } = 0;

        Random Rand { get; init; } = new Random();

        public MainForm()
        {
            InitializeComponent();

        }

        private double NextRandom(double mean = 0, double stdDev = 1)
        {
            double u1 = 1.0 - Rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }

        private void GenerateData(string filePath)
        {
            var dataSet = new DataSet<SampleWithClass>();

            for (var i = 0; i < 200; i++)
            {
                var x1 = (float)(NextRandom(20, 2));
                var x2 = (float)(NextRandom(20, 4));

                dataSet.Add(new float[] { x1, x2 }, 0);
            }

            for (var i = 0; i < 200; i++)
            {
                var x1 = (float)(NextRandom(0, 3));
                var x2 = (float)(NextRandom(5, 3));

                dataSet.Add(new float[] { x1, x2 }, 1);
            }

            dataSet.Shuffle();

            // Write DataSet
            {
                var records = dataSet.Set
                                     .Select(s => new Data.PerceptronData() { DClass = s.D, X1 = s.X[0], X2 = s.X[1] });

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            m_iterationsTextBox.DataBindings.Add("Text", this, "TrainParameters.Iterations");
            m_learningRateTextBox.DataBindings.Add("Text", this, "TrainParameters.LearningRate");
        }

        private void OnTrain(object sender, EventArgs e)
        {
            var dataSetFile = m_randomRadioButton.Checked ? "Data\\data_random.csv" :
                                                            "Data\\data_static.csv";
            if (m_randomRadioButton.Checked)
            {
                GenerateData(dataSetFile);
            }

            // Load datat set
            {
                TrainDataSet.Clear();
                TestDataSet.Clear();

                var config = new CsvConfiguration(CultureInfo.InvariantCulture);

                using (var reader = new StreamReader(dataSetFile))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Data.PerceptronData>().ToList();
                    var trainRecords = records.Take((records.Count() * 4) / 5);
                    var testRecords = records.Skip(trainRecords.Count());

                    foreach (var record in trainRecords)
                    {
                        TrainDataSet.Add(new float[] { record.X1, record.X2 }, record.DClass);
                    }

                    foreach (var record in testRecords)
                    {
                        TestDataSet.Add(new float[] { record.X1, record.X2 }, record.DClass);
                    }
                }
            }

            // Train
            var trainErrors = Perceptron.Train(TrainDataSet.Set, TrainParameters);
            DecisionBoundarySlope = -(Perceptron.W[0] / Perceptron.W[2]) / (Perceptron.W[0] / Perceptron.W[1]);
            DecisionBoundaryIntercept = -(Perceptron.W[0] / Perceptron.W[2]);

            // Test
            {
                var testSet = TestDataSet.Set.ToList();
                TestDataSet.Clear();

                foreach (var test in testSet)
                {
                    var estimation = new SampleWithClassEstimation()
                    {
                        X = test.X,
                        D = test.D,
                        E = Perceptron.Estimate(test)
                    };

                    TestDataSet.Add(estimation);
                }
            }

            // draw dataSet
            {
                var plotModel = new PlotModel() { Background = OxyColors.White };

                var axisX1 = new LinearAxis { Title = "X1", Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false };
                var axisX2 = new LinearAxis { Title = "X2", Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };
                var trainColorAxis = new LinearColorAxis { Position = AxisPosition.Right, Key = "Train", Palette = OxyPalettes.Cool(2), Minimum = 0, Maximum = 1, IsAxisVisible = false };
                var testColorAxis = new LinearColorAxis { Position = AxisPosition.Right, Key = "Test", Minimum = -1, Maximum = 1, IsAxisVisible = false };
                testColorAxis.Palette.Colors.Clear();
                testColorAxis.Palette.Colors.Add(OxyColor.FromRgb(0, 0, 0));
                testColorAxis.Palette.Colors.Add(trainColorAxis.Palette.Colors[0].ChangeSaturation(0.2));
                testColorAxis.Palette.Colors.Add(trainColorAxis.Palette.Colors[1].ChangeSaturation(0.2));

                plotModel.Axes.Add(axisX1);
                plotModel.Axes.Add(axisX2);
                plotModel.Axes.Add(trainColorAxis);
                plotModel.Axes.Add(testColorAxis);

                var traingScatterSeries = new ScatterSeries() 
                {
                    MarkerType = MarkerType.Triangle,
                    MarkerStroke = OxyColors.Black,
                    MarkerStrokeThickness = 1.0,
                    ColorAxisKey = trainColorAxis.Key
                };
                traingScatterSeries.Points.AddRange(TrainDataSet.Set.Select(s => new ScatterPoint(s.X[0], s.X[1])
                {
                    Value = s.D
                }));

                var testScatterSeries = new ScatterSeries()
                {
                    MarkerType = MarkerType.Circle,
                    MarkerStroke = OxyColors.Black,
                    MarkerStrokeThickness = 1.0,
                    ColorAxisKey = testColorAxis.Key,
                };
                testScatterSeries.Points.AddRange(TestDataSet.Set.Select(s => new ScatterPoint(s.X[0], s.X[1])
                {
                    Value = s.E == s.D ? s.E : -1 // 0,1 for classes and -1 for wrong detection (error in estimation)
                }));

                plotModel.Series.Add(traingScatterSeries);
                plotModel.Series.Add(testScatterSeries);

                var lineAnnotation = new LineAnnotation()
                {
                    Slope = DecisionBoundarySlope,
                    Intercept = DecisionBoundaryIntercept,
                    Color = OxyColors.Red,
                };
                plotModel.Annotations.Add(lineAnnotation);

                m_trainPlotView.Model = plotModel;
            }

            // draw error
            {
                var plotModel = new PlotModel() { Background = OxyColors.White };

                var axisIter = new LinearAxis { Title = "Iteration", Position = AxisPosition.Bottom, IsPanEnabled = false, IsZoomEnabled = false };
                var axisErr = new LinearAxis { Title = "Train Errors", Position = AxisPosition.Left, IsPanEnabled = false, IsZoomEnabled = false };

                plotModel.Axes.Add(axisIter);
                plotModel.Axes.Add(axisErr);

                var lineSeries = new LineSeries();
                var index = 0;
                foreach (var err in trainErrors)
                {
                    var pt = new DataPoint(index++, err);
                    lineSeries.Points.Add(pt);
                }

                plotModel.Series.Add(lineSeries);

                m_errorPlotView.Model = plotModel;
            }
        }

      
    }
}