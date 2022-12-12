using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2.Data
{
    public class CarData
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;

        public double EngineSize { get; set; } = 0;
        public double Cylinders { get; set; } = 0;
        public double Horsepower { get; set; } = 0;
        public double MPG_City { get; set; } = 0;
        public double MPG_Highway { get; set; } = 0;
        public double Weight { get; set; } = 0;
        public double Wheelbase { get; set; } = 0;
        public double Length { get; set; } = 0;
    }
}
