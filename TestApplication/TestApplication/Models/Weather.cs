using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication.Models
{
    public class Weather
    {
        public string Temp { get; set; }
        public string Date { get; set; }
        public string Icon { get; set; }

        public static List<Weather> GetDummyData()
        {
            var tempList = new List<Weather>();
            tempList.Add(new Weather { Temp = "22", Date = "Sunday 16", Icon = "weather.png" });
            tempList.Add(new Weather { Temp = "21", Date = "Monday 17", Icon = "weather.png" });
            tempList.Add(new Weather { Temp = "20", Date = "Tuesday 18", Icon = "weather.png" });
            tempList.Add(new Weather { Temp = "12", Date = "Wednesday 19", Icon = "weather.png" });
            tempList.Add(new Weather { Temp = "17", Date = "Thursday 20", Icon = "weather.png" });
            tempList.Add(new Weather { Temp = "20", Date = "Friday 21", Icon = "weather.png" });

            return tempList;
        }
    }

    public class WeatherResponse
    {
        public DateTime Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public double Temperature { get; set; }
        public double ApparentTemperature { get; set; }
        public double DewPoint { get; set; }
        public double Humidity { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
        public double WindBearing { get; set; }
        public double CloudCover { get; set; }
        public double UVIndex { get; set; }
        public double Visibility { get; set; }

        public string GetIconPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Icon))
                    return string.Empty;

                return $"https://assetambee.s3-us-west-2.amazonaws.com/weatherIcons/PNG/{Icon}.png";
            }
        }
    }
}
