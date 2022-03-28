using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class WeatherService
    {
        private HttpClient httpClient;

        private const string apiKey = "ADD-YOUR-API-KEY-HERE";
        private const string baseApiUrl = "https://api.ambeedata.com/weather";

        public WeatherService()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<WeatherResponse> GetLatestWeatherByPosition(double latitude, double longitude)
        {
            var uri = new Uri($"{baseApiUrl}/latest/by-lat-lng?lat={latitude}&lng={longitude}");

            var response = await this.httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            var data = jObject["data"];

            var weatherResponse = new WeatherResponse
            {
                Time = new DateTime(GetValueFromJson<int>(data, "time")),
                Summary = GetValueFromJson<string>(data, "summary"),
                Icon = GetValueFromJson<string>(data, "icon"),
                Temperature = GetValueFromJson<double>(data, "temperature"),
                ApparentTemperature = GetValueFromJson<double>(data, "apparentTemperature"),
                DewPoint = GetValueFromJson<double>(data, "dewPoint"),
                Humidity = GetValueFromJson<double>(data, "humidity"),
                Pressure = GetValueFromJson<double>(data, "pressure"),
                WindSpeed = GetValueFromJson<double>(data, "windSpeed"),
                WindGust = GetValueFromJson<double>(data, "windGust"),
                WindBearing = GetValueFromJson<double>(data, "windBearing"),
                CloudCover = GetValueFromJson<double>(data, "cloudCover"),
                UVIndex = GetValueFromJson<double>(data, "uvIndex"),
                Visibility = GetValueFromJson<double>(data, "visibility")
            };

            return weatherResponse;
        }

        public async Task<List<WeatherResponse>> GetLatestWeatherForecastByPosition(double latitude, double longitude, DateTime from, DateTime to)
        {
            var uri = new Uri($"{baseApiUrl}/history/by-lat-lng?lat={latitude}&lng={longitude}&from={from:yyyy-MM-dd HH:mm:ss}&to={to:yyyy-MM-dd HH:mm:ss}");

            var response = await this.httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            var data = jObject["history"];

            var forecast = new List<WeatherResponse>();

            foreach (var entry in data)
            {
                forecast.Add(new WeatherResponse
                {
                    Time = new DateTime(GetValueFromJson<int>(entry, "time")),
                    Summary = GetValueFromJson<string>(entry, "summary"),
                    Icon = GetValueFromJson<string>(entry, "icon"),
                    Temperature = GetValueFromJson<double>(entry, "temperature"),
                    ApparentTemperature = GetValueFromJson<double>(entry, "apparentTemperature"),
                    DewPoint = GetValueFromJson<double>(entry, "dewPoint"),
                    Humidity = GetValueFromJson<double>(entry, "humidity"),
                    Pressure = GetValueFromJson<double>(entry, "pressure"),
                    WindSpeed = GetValueFromJson<double>(entry, "windSpeed"),
                    WindGust = GetValueFromJson<double>(entry, "windGust"),
                    WindBearing = GetValueFromJson<double>(entry, "windBearing"),
                    CloudCover = GetValueFromJson<double>(entry, "cloudCover"),
                    UVIndex = GetValueFromJson<double>(entry, "uvIndex"),
                    Visibility = GetValueFromJson<double>(entry, "visibility")
                });
            }

            return forecast;
        }

        private T GetValueFromJson<T>(JToken data, string field)
        {
            return (T)Convert.ChangeType((string)data[field], typeof(T));
        }
    }
}
