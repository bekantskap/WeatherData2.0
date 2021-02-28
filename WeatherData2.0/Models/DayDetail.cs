using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherData2._0.Models
{
    public class DayDetail
    {
        public DateTime Date { get; set; }
        public float? InsideTemperature { get; set; }
        public float? OutsideTemperature { get; set; }
        public int? InsideHumidity { get; set; }
        public int? OutsideHumidity { get; set; }
    }
}
