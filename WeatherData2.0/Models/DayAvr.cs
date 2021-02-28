using System;

namespace WeatherData2._0.Models
{
    public class DayAvr
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float IndoorTemperature { get; set; }
        public float OutdoorTemperature { get; set; }
        public float IndoorHumidity { get; set; }
        public float OutdoorHumidity { get; set; }
        public int IndoorMold { get; set; }
        public int OutdoorMold { get; set; }
    }
}



