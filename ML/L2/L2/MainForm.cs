using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using L2.Utils;
using System.Diagnostics;
using System.Data;
using System.Drawing.Imaging;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Windows.Forms;
using OxyPlot.WindowsForms;

namespace L2
{
    public partial class MainForm : Form
    {
        DataSet<SampleWithClass> TrainDataSet { get; set; } = new DataSet<SampleWithClass>();

        DataSet<SampleWithClassEstimation> TestDataSet { get; set; } = new DataSet<SampleWithClassEstimation>();

        KohonenMapTrainParameters TrainParameters { get; init; } = new KohonenMapTrainParameters();

        KohonenMap Map { get; init; } = new KohonenMap();

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

                    var typeSet = new SortedSet<string>();
                    foreach (var record in records)
                    {
                        typeSet.Add(record.Type);
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
                                    typeSet.FindIndex(record.Type));
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
            const int width = 10;
            const int height = 10;

            Map.Init(width, height, TrainDataSet.Depth);
            Map.Train(TrainDataSet.Set, TrainParameters);
            //Map.Train(dataSet.Set, TrainParameters);

            // Test
            var errors = Map.Test(TrainDataSet.Set, TestDataSet.Set);
            Debug.WriteLine(errors);

            // Draw result
            {
                var tableW = 3;
                var tableH = (int)Math.Ceiling(Map.Depth / (float)tableW);

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
                    var model = new PlotModel() /*{ Title = $"W[{d+1}]" }*/;

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
                        X0 = 0.5,
                        X1 = Map.Width + 0.5,
                        Y0 = 0.5,
                        Y1 = Map.Height + 0.5,
                        Data = data,
                        Interpolate = false,
                        LabelFontSize = 0.2,
                    };
                    model.Series.Add(series);

                    var plotView = new PlotView();
                    m_tableLayoutPanel.Controls.Add(plotView, d % tableW, d / tableW);

                    plotView.Dock = DockStyle.Fill;
                    plotView.Model = model;
                }
            }
        }
    }
}